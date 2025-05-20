using UnityEngine;
using System;
using System.Collections;

public class Shield_Items : MonoBehaviour
{
    public float effectDuration = 2.0f; // 효과 지속 시간
    public GameObject shieldSurroundingPrefab;
    public GameObject shieldActivateEffectPrefab;

    private Animator anim;
    private MeshRenderer meshRenderer;


    private void Start()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<MeshRenderer>(); ;
        anim.enabled = true;
    }
    
    private void OnEnable()
    {
        if (anim != null)
        {
            anim.enabled = true;  // 오브젝트가 다시 활성화될 때 애니메이터 켜기
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.getPItemSound.Play();

            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(); // enable 
            foreach (var mr in meshRenderers)
            {
                mr.enabled = false;
                Debug.Log("MeshRenderer 끔: " + mr.gameObject.name);
            }
            anim.enabled = false;

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
        Destroy(ActiveS, 0.5f);

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
        anim.enabled = true;
    }
}
