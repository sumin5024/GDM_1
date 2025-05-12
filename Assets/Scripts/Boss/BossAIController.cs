using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;


/*
 * ���� �ൿ����
 * 1. SpawnCorn; ������ 3�� ���� ��ȯ
 * 2. PalmBlast; ��ǳ
 * 3. SwingSafetybaton; ������ �ֵθ���
 * 4. Dash; �����ġ��
 * WhistleSignal ȣ����
 */
public class BossAIController : MonoBehaviour
{
    public float lifeTime = 10f;
    public bool isRandom = false;
    int act = -1;

    [Header("Whistle Action Pattern")]
    public bool isSpawnCorn;
    public bool isPalmBlast;
    public bool isSwingSafetybaton;
    public bool isDash;
    
    [Header("Action")]
    public GameObject bossModel;
    public Animator animator;
    public GameObject apprearEffect_prefab;
    float LockTime; //gamemanager ispawn true�� �������� �ʰ�
    bool withObstacleSpwaner;
    GameObject player;

    [Header("SpawnCorn")]
    public GameObject corn_prefab;
    public float cornDelayTime;

    [Header("PalmBlast")]
    public GameObject blast_prefab;
    public float Blast_gap = 0.025f;

    [Header("SwingSafetybaton")]
    public GameObject safetybaton_prefab;
    public float Safetybaton_gap = 0.025f;

    [Header("Dash")]
    public float dashSpeed = 0.01f;
    public GameObject dashCollider_prefab;
    public ParticleSystem dashEffect;

    [Header("WhistleSignal")]
    public GameObject whistle;
    public float whistleDuration;
    private GameObject tmp_whistle;

    private Coroutine actionCoroutine;
    private void Start()
    {
        withObstacleSpwaner = (bool) GameObject.FindAnyObjectByType<Spawner>(); //�������� ����

        player = FindAnyObjectByType<LoopingZMovement>().gameObject;

        //ȣ���� UI ����
        tmp_whistle = Instantiate(whistle, transform);
        tmp_whistle.transform.eulerAngles = new Vector3(0, -90, 0);
        tmp_whistle.SetActive(false);

        //lifeTime�� �ڿ� ����
        Destroy(this.gameObject, lifeTime);

    }
    private void OnEnable()
    {
        //OnAppearDisappearEffect();

    }
    private void OnDisable()
    {
        StopAllCoroutines();
        OnAppearDisappearEffect();

    }

    void DeleteAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }
    private void LateUpdate()
    {
        if (GameManager.Instance.isSpawn && LockTime <=  0)
        {
            LockTime = 2;
            if (!withObstacleSpwaner)
            {
                GameManager.Instance.isSpawn = false;
                DeleteAllObstacles();
            }
            StartCoroutine(ActionPattern(0f));
        }
        LockTime -= Time.deltaTime;
    }
    private void InitPattern()
    {
        bossModel.SetActive(true);
        tmp_whistle.SetActive(false);
        animator.SetInteger("ThrowBaton", -1);
        animator.SetTrigger("Idle");
        animator.SetFloat("speed", LoopingZMovement.speed + 1.5f);//�ִϸ��̼� ��� �ӵ�
    }

    IEnumerator ActionPattern(float delaytime)
    {
        InitPattern();
        yield return new WaitForSeconds(delaytime);
                 
        if (actionCoroutine != null) StopCoroutine(actionCoroutine);
        if(isRandom)  act = Random.Range(0,4);
        else act = ++act% 4;
        switch (act)
        {
            case 0:     //������
                SpawnCorns();
                break;
            case 1:     //��ǳ
                PalmBlast();
                break;
            case 2:     //������
                SwingSafetybaton();
                break;
            case 3:     //���� ��ġ��
                Dash();
                break;

        }

    }

    private void SpawnCorns() 
    {
        actionCoroutine =  StartCoroutine(SpawnCornsLoop());
    }
    IEnumerator SpawnCornsLoop()
    {
        if (isSpawnCorn) WhistleSignal();//ȣ����

        Debug.Log("SpawnCorn");

        yield return new WaitForSeconds(whistleDuration);//ȣ����� ���� ���� �� �ð� ��
        tmp_whistle.SetActive(false);

        //������ ����
        GameObject tmp;
        for(int i=0; i<3; i++)  
        {
            animator.SetTrigger("Corn");
            yield return new WaitForSeconds(cornDelayTime);

            Vector3 pos = transform.position;
            tmp = Instantiate(corn_prefab, pos, Quaternion.identity);
            Destroy(tmp, 2f);
        }

    }
    private void PalmBlast() 
    {
        actionCoroutine = StartCoroutine(MakePalmBlast());

    }

    IEnumerator MakePalmBlast()
    {
        if (isPalmBlast) WhistleSignal();//ȣ����

        Debug.Log("MakePalmBlast");

        yield return new WaitForSeconds(whistleDuration );//ȣ����� ���� ���� �� �ð� ��
        tmp_whistle.SetActive(false);

        animator.SetTrigger("Jangpoong");
        yield return new WaitForSeconds(0.7f);   //�ִϸ��̼� ������

        //��ǳ ����
        GameObject tmp; 
        Vector3 pos = transform.position;
        tmp = Instantiate(blast_prefab, pos + Vector3.up * Blast_gap, Quaternion.identity);
        Destroy(tmp, 2f);
    }

    private void SwingSafetybaton() 
    {
        actionCoroutine = StartCoroutine(SwingRandomSafetybaton());

    }

    IEnumerator SwingRandomSafetybaton()
    {

        if (isSwingSafetybaton) WhistleSignal();    //ȣ����

        Debug.Log("SwingSafetybaton");

        yield return new WaitForSeconds(whistleDuration);//ȣ����� ���� ���� �� �ð� ��
        tmp_whistle.SetActive(false);

        int h = Random.Range(0, 3);
        animator.SetInteger("ThrowBaton", h);

        //������ ������
        GameObject tmp;
        Vector3 pos = transform.position;
        tmp = Instantiate(safetybaton_prefab, pos + Vector3.up*h* Safetybaton_gap, Quaternion.Euler(0,0,0));
        Destroy(tmp, 2f);
    }

    private void Dash() 
    {
        actionCoroutine = StartCoroutine(StartDash());
    }

    IEnumerator StartDash()
    {
        if (isDash) WhistleSignal();   //ȣ���� ���

        Debug.Log("StartDash");

        yield return new WaitForSeconds(whistleDuration);   //ȣ����� ���� ���� �� �ð� ��

        animator.SetTrigger("Idle");

        yield return new WaitForSeconds(whistleDuration);   //ȣ����� ���� ���� �� �ð� ��

        tmp_whistle.SetActive(false);

        bossModel.SetActive(false); //�ִϸ��̼� ������Ʈ  ��Ȱ��ȭ

        //�뽬 ������Ʈ ����
        Vector3 pos = transform.position;  
        GameObject tmp = Instantiate(dashCollider_prefab, pos, Quaternion.identity);
        Destroy(tmp, 2);
        //�̵�
        float speed = 0;
        float Zspeed = LoopingZMovement.speed;
        while (tmp) 
        {
            speed += dashSpeed;
            tmp.transform.Translate(Vector3.back * (-Zspeed + speed) * Time.deltaTime);
            yield return null;

        }
    }


    private void WhistleSignal() 
    {
        Debug.Log("Whistle");

        animator.SetTrigger("Whistle"); // ȣ���� �ִϸ��̼�

        tmp_whistle.SetActive(true);    //ȣ���� UI Ȱ��ȭ

    }
    private void OnAppearDisappearEffect()  //����, ���� ����Ʈ
    {
        Vector3 pos = transform.position;
        GameObject effect = Instantiate(apprearEffect_prefab, pos , Quaternion.identity);
        Destroy(effect, 1);
    } 

}
