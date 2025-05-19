using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class LoopingZMovement : MonoBehaviour
{
    public float startZ = -0.96f;         // ���� z ��ġ
    public float endZ = 0.54f;         // �� z ��ġ (�ǵ��ư� ����)
    public static float speed = 0.5f;          // �̵� �ӵ�
    private float previousZ;
    public float totalZDistance = 0f;

    public float speedAmount = 0.4f;  // ���ǵ� ��ȭ��
    public float effectDuration = 2.0f; // ȿ�� ���� �ð�

    

    public static bool isShieldActive = false; // ���� ������ 

    void Start()
    {
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if (!isShieldActive) // ���� ���� 
            {
                SoundManager.instance.getNItemSound.Play();
               // LoopingZMovemet_reverse.speed = -Mathf.Abs(LoopingZMovemet_reverse.speed);
              //  Debug.Log("�ӵ� ���� ������: " + speedAmount);
               // if (LoopingZMovemet_reverse.speed > 0.2f) { LoopingZMovemet_reverse.speed -= 0.2f; }
                GameManager.Instance.isSpawn = true;
                StartCoroutine(ApplyTemporarySpeed());
                
            }
            else
            {
                Debug.Log("Shield On");
                GameManager.Instance.isSpawn = true;
            }
            
        }
    }
    private IEnumerator ApplyTemporarySpeed()
    {
        float originalSpeed = LoopingZMovemet_reverse.speed;// LoopingZMovement_reverse.speed;
        Debug.Log("���� �ӵ�: " + originalSpeed);
        float newSpeed = Mathf.Clamp(originalSpeed - speedAmount, 0.1f, 1.5f);

        LoopingZMovemet_reverse.speed = newSpeed;
        Debug.Log("�ӵ� ���� ����: " + newSpeed);

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovemet_reverse.speed = originalSpeed;
        Debug.Log("�ӵ� ����: " + originalSpeed);

       

        yield return new WaitForSeconds(0.1f);
       

    }


}
