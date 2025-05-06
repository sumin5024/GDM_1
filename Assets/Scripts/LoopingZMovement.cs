using UnityEngine;

public class LoopingZMovement : MonoBehaviour
{
    public float startZ = 0f;         // ���� z ��ġ
    public float endZ = -50f;         // �� z ��ġ (�ǵ��ư� ����)
    public static float speed = 0.5f;          // �̵� �ӵ�

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

        // ������ �̵� �Ͽ� �ݺ�
        if (transform.position.z >= endZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
            Player.Instance.isGrounded = true;
        }

        // ����
        if(speed < 0.7f)
        {
            speed += 0.02f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ��ֹ� �浹 �� ����
        if (collision.gameObject.name != "Floor")
        {
            if (speed > 0.2f) { speed -= 0.2f; }
        }
    }
}
