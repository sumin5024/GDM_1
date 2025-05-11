using UnityEngine;
using UnityEngine.AI;

public class LoopingZMovement : MonoBehaviour
{
    public float startZ = -0.96f;         // 시작 z 위치
    public float endZ = 0.54f;         // 끝 z 위치 (되돌아갈 지점)
    public static float speed = 0.5f;          // 이동 속도
    private float previousZ;
    public float totalZDistance = 0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        startPosition.z = startZ;
        transform.position = startPosition;

        previousZ = startZ;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        
        CheckDistance();
        // 앞으로 이동 하여 반복
        if (transform.position.z >= endZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
            Player.Instance.isGrounded = true;
            GameManager.Instance.isSpawn = true;

        }

        // 가속
        if(speed < 0.7f)
        {
            speed += 0.02f * Time.deltaTime;
        }

        GameManager.Instance.runDistance = totalZDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if (speed > 0.2f) { speed -= 0.2f; }
        }
    }

    public void CheckDistance()
    {
        float currentZ = transform.position.z;
        float deltaZ = currentZ - previousZ;

        if (deltaZ > 0f)
        {
            totalZDistance += deltaZ;
        }

        previousZ = currentZ;
    }
}
