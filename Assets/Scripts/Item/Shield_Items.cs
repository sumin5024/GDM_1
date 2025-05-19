using UnityEngine;
using System;
using System.Collections;

public class Shield_Items : MonoBehaviour
{
    public float effectDuration = 2.0f; // 효과 지속 시간
    public GameObject shieldSurroundingPrefab;
    public GameObject shieldActivateEffectPrefab;

    private Animator anim;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.getPItemSound.Play();
            
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false; //눈에 안보이게 수정. 

            StartCoroutine(ActivateShield(other.gameObject));
            
        }
    }
    

    private IEnumerator ActivateShield(GameObject player)
    {
        LoopingZMovement.isShieldActive = true;
        Debug.Log("쉴드 ON");

        //item active part 
        Transform sheildApos = player.transform.Find("ItemActP");
        GameObject ActiveS = Instantiate(shieldActivateEffectPrefab);
        ActiveS.transform.SetParent(sheildApos);
        ActiveS.transform.localPosition = Vector3.zero;
        Destroy(ActiveS,0.5f);

        //shieldsurround 구현
        Transform shieldPos = player.transform.Find("ShieldPosition");
        GameObject shieldEffect = Instantiate(shieldSurroundingPrefab);
        shieldEffect.transform.SetParent(shieldPos);
        shieldEffect.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(effectDuration);

        LoopingZMovement.isShieldActive = false;
        Debug.Log("쉴드 OFF");

        FindObjectOfType<Item_Spawner>()?.OnShieldItemCollected();

        Destroy(shieldEffect);
        Destroy(gameObject, 0.1f);
    }
}
