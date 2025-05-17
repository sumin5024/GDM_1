using UnityEngine;
using System;
using System.Collections;

public class Shield_Items : MonoBehaviour
{
    public float effectDuration = 2.0f; // 효과 지속 시간
    public GameObject shieldSurroundingPrefab; 

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateShield(other.gameObject));
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            if (anim != null)
            {
                anim.SetTrigger("Activate");
            }
        }
    }

    private IEnumerator ActivateShield(GameObject player)
    {
        LoopingZMovement.isShieldActive = true;
        Debug.Log("쉴드 ON");

        GameObject shieldEffect = Instantiate(shieldSurroundingPrefab, player.transform.position, Quaternion.identity);
        shieldEffect.transform.SetParent(player.transform);
        shieldEffect.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovement.isShieldActive = false;
        Debug.Log("쉴드 OFF");

        FindObjectOfType<Item_Spawner>()?.OnShieldItemCollected();

        Destroy(shieldEffect);
        Destroy(gameObject, 0.2f);
    }
}
