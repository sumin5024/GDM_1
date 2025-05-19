using UnityEngine;
using System;
using System.Collections;

public class Time_Items : MonoBehaviour
{
    public float timeChangeAmount = 5f;
    public GameObject TimeActivePrefab;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Transform itemActPos = other.transform.Find("ItemActP");
            
            SoundManager.instance.getPItemSound.Play();
            GameManager.Instance.AddTime(timeChangeAmount);

            Transform TimeApos = other.transform.Find("ItemActP");
            GameObject ActiveS = Instantiate(TimeActivePrefab);
            ActiveS.transform.SetParent(TimeApos);
            ActiveS.transform.localPosition = Vector3.zero;
            Destroy(ActiveS,0.5f);
            
            {
                Debug.Log("시간 변화");

                FindObjectOfType<Item_Spawner>()?.OnTimeItemCollected(); // 스포너호출

                GetComponent<Collider>().enabled = false;
                Destroy(gameObject, 1f);
            }
        }
    }
}
