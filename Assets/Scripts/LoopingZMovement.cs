using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class LoopingZMovement : MonoBehaviour
{
    public float startZ = -0.96f;         // 시작 z 위치
    public float endZ = 0.54f;         // 끝 z 위치 (되돌아갈 지점)
    public static float speed = 0.5f;          // 이동 속도
    private float previousZ;
    public float totalZDistance = 0f;

    public float speedAmount = 0.4f;  // 스피드 변화량
    public float effectDuration = 2.0f; // 효과 지속 시간

    

    public static bool isShieldActive = false; // 쉴드 아이템 

    void Start()
    {
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if (!isShieldActive) // 쉴드 판정 
            {
                SoundManager.instance.getNItemSound.Play();
               // LoopingZMovemet_reverse.speed = -Mathf.Abs(LoopingZMovemet_reverse.speed);
              //  Debug.Log("속도 감소 아이템: " + speedAmount);
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
        Debug.Log("원래 속도: " + originalSpeed);
        float newSpeed = Mathf.Clamp(originalSpeed - speedAmount, 0.1f, 1.5f);

        LoopingZMovemet_reverse.speed = newSpeed;
        Debug.Log("속도 변경 적용: " + newSpeed);

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovemet_reverse.speed = originalSpeed;
        Debug.Log("속도 복귀: " + originalSpeed);

       

        yield return new WaitForSeconds(0.1f);
       

    }


}
