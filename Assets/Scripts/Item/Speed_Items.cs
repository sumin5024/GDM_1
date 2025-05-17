using UnityEngine;
using System;
using System.Collections;

public class Speed_Item : MonoBehaviour
{
    public float speedAmount = 0.7f;  // 스피드 변화량
    public float effectDuration = 2.0f; // 효과 지속 시간

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            if (gameObject.name.Contains("Faster"))
            {
                speedAmount = Mathf.Abs(speedAmount);
                Debug.Log("속도 증가 아이템: " + speedAmount);

            }
            else if (gameObject.name.Contains("Slower"))
            {
                speedAmount = -Mathf.Abs(speedAmount);
                Debug.Log("속도 감소 아이템: " + speedAmount);

            }

            if (anim != null)
            {
                anim.SetTrigger("Activate");
            }

            StartCoroutine(ApplyTemporarySpeed());


        }
    }

    private IEnumerator ApplyTemporarySpeed()
    {
        float originalSpeed = LoopingZMovement.speed;
        Debug.Log("원래 속도: " + originalSpeed);
        float newSpeed = Mathf.Clamp(originalSpeed + speedAmount, 0.1f, 1.5f);

        LoopingZMovement.speed = newSpeed;
        Debug.Log("속도 변경 적용: " + newSpeed);

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovement.speed = originalSpeed;
        Debug.Log("속도 복귀: " + originalSpeed);

        FindObjectOfType<Item_Spawner>()?.OnSpeedItemCollected(); // 스포너 호출

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);

    }
}
