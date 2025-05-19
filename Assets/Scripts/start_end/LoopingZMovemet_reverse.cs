using UnityEngine;
using UnityEngine.AI;

public class LoopingZMovemet_reverse : MonoBehaviour
{
    public float startZ = 0f;         // ���� z ��ġ
    public float endZ = -3f;         // �� z ��ġ (�ǵ��ư� ����)
    public static float speed = 0.3f;          // �̵� �ӵ�
    private float previousZ;
    public static float totalZDistance = 0f;

    public bool issecond;
    public bool isitem;
    public bool isObstacle;



    public Vector3 moveDirection = -Vector3.forward;

    private Vector3 startPosition;

    public static bool isShieldActive = false; // ���� ������ 

    void Start()
    {
        //totalZDistance = 0f;
        if (issecond || isitem)
        {
            startPosition = transform.position;
            startPosition.z = startZ;
            transform.position = startPosition;

            previousZ = startZ;
        }
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (issecond)
        {
            CheckDistance();
        }

        
        // ������ �̵� �Ͽ� �ݺ�
        if (transform.position.z <= endZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
            if (isObstacle)
            {
                GameManager.Instance.isSpawn = true;
            }

        }

        // ����
        if (speed < 0.5f)
        {
            speed += 0.02f * Time.deltaTime;
        }

        GameManager.Instance.runDistance = totalZDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (!isShieldActive) // ���� ���� 
            {
                if (speed > 0.2f) { speed -= 0.3f; }
                GameManager.Instance.isSpawn = true;
            }
            else
            {
                Debug.Log("Shield On");
            }

        }
    }

    public void CheckDistance()
    {
        float currentZ = transform.position.z;
        float deltaZ = currentZ - previousZ;

        if (deltaZ < 0f)
        {
            totalZDistance -= deltaZ;
        }

        previousZ = currentZ;
    }


}
