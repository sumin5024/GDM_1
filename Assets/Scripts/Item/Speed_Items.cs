using UnityEngine;
using System;
using System.Collections;

public class Speed_Item : MonoBehaviour
{
    public float speedAmount = 0.7f;  // 스피드 변화량
    public float effectDuration = 0.5f; // 효과 지속 시간
    public GameObject SpeedActivePrefab;
    private MeshRenderer meshRenderer;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<MeshRenderer>(); ;
        anim.enabled = true;
    }
    
    private void OnEnable()
    {
        if (anim != null)
        {
            anim.enabled = true;  // 오브젝트가 다시 활성화될 때 애니메이터 켜기
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            if (gameObject.name.Contains("Faster"))
            {
                SoundManager.instance.getPItemSound.Play();
                speedAmount = Mathf.Abs(speedAmount);
                Debug.Log("속도 증가 아이템: " + speedAmount);

            }
            else if (gameObject.name.Contains("Slower"))
            {
                SoundManager.instance.getNItemSound.Play();
                speedAmount = -Mathf.Abs(speedAmount);
                Debug.Log("속도 감소 아이템: " + speedAmount);

            }

            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(); // enable 
            foreach (var mr in meshRenderers)
            {
                mr.enabled = false;
                Debug.Log("MeshRenderer 끔: " + mr.gameObject.name);
            }
            anim.enabled = false;


            StartCoroutine(ApplyTemporarySpeed(other.gameObject));


        }
    }

    private IEnumerator ApplyTemporarySpeed(GameObject player)
    {


        Transform speedApos = player.transform.Find("ItemActP");
        GameObject ActiveS = Instantiate(SpeedActivePrefab);
        ActiveS.transform.SetParent(speedApos);
        ActiveS.transform.localPosition = Vector3.zero;
        Destroy(ActiveS, 0.5f);


        float originalSpeed = LoopingZMovemet_reverse.speed;// LoopingZMovement_reverse.speed;
        Debug.Log("원래 속도: " + originalSpeed);
        float newSpeed = Mathf.Clamp(originalSpeed + speedAmount, 0.1f, 1.5f);

        LoopingZMovemet_reverse.speed = newSpeed;
        Debug.Log("속도 변경 적용: " + newSpeed);

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovemet_reverse.speed = originalSpeed;
        Debug.Log("속도 복귀: " + originalSpeed);

        FindObjectOfType<Item_Spawner>()?.OnSpeedItemCollected(); // 스포너 호출

        yield return new WaitForSeconds(0.1f);
        anim.enabled = true; // enable 추가 
        Destroy(gameObject);

    }
}
