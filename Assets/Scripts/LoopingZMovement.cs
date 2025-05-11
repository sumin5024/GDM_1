using UnityEngine;
using UnityEngine.AI;

public class LoopingZMovement : MonoBehaviour
{
    public float startZ = -0.96f;         // ���� z ��ġ
    public float endZ = 0.54f;         // �� z ��ġ (�ǵ��ư� ����)
    public static float speed = 0.5f;          // �̵� �ӵ�
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
        // ������ �̵� �Ͽ� �ݺ�
        if (transform.position.z >= endZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
            Player.Instance.isGrounded = true;
            GameManager.Instance.isSpawn = true;

        }

        // ����
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
