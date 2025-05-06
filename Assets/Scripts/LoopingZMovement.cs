using UnityEngine;

public class LoopingZMovement : MonoBehaviour
{
    public float startZ = 0f;         // 시작 z 위치
    public float endZ = -50f;         // 끝 z 위치 (되돌아갈 지점)
    public static float speed = 0.5f;          // 이동 속도

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        startPosition.z = startZ;
        transform.position = startPosition;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // 앞으로 이동 하여 반복
        if (transform.position.z >= endZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
            Player.Instance.isGrounded = true;
        }

        // 가속
        if(speed < 0.7f)
        {
            speed += 0.02f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 장애물 충돌 시 감속
        if (collision.gameObject.name != "Floor")
        {
            if (speed > 0.2f) { speed -= 0.2f; }
        }
    }
}
