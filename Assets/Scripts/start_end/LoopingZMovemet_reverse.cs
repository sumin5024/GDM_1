using UnityEngine;
using UnityEngine.AI;

public class LoopingZMovemet_reverse : MonoBehaviour
{
    public float startZ = 0f;         // 시작 z 위치
    public float endZ = -3f;         // 끝 z 위치 (되돌아갈 지점)
    public static float speed = 0.3f;          // 이동 속도
    private float previousZ;
    public static float totalZDistance = 0f;

    public bool issecond;
    public bool isitem;
    public bool isObstacle;



    public Vector3 moveDirection = -Vector3.forward;

    private Vector3 startPosition;

    public static bool isShieldActive = false; // 쉴드 아이템 

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

        
        // 앞으로 이동 하여 반복
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

        // 가속
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
            if (!isShieldActive) // 쉴드 판정 
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
