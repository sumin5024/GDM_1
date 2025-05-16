using UnityEngine;
using System;
using System.Collections;

public class Shield_Items : MonoBehaviour
{
    public float effectDuration = 2.0f; // 효과 지속 시간

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(ActivateShield());
            GetComponent<Collider>().enabled = false; 
            GetComponent<MeshRenderer>().enabled = false; 
        }
    }

    private IEnumerator ActivateShield()
    {
        LoopingZMovement.isShieldActive = true;
        Debug.Log("쉴드 ON");

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovement.isShieldActive = false;
        Debug.Log("쉴드 OFF");

        Destroy(gameObject);
    }
}
