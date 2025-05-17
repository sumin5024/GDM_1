using UnityEngine;

public class Time_Items : MonoBehaviour
{
    public float timeChangeAmount = 5f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (anim != null)
        {
            anim.SetTrigger("Activate");
        }

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddTime(timeChangeAmount);
            {
                Debug.Log("시간 변화");

                FindObjectOfType<Item_Spawner>()?.OnTimeItemCollected(); // 스포너호출

                GetComponent<Collider>().enabled = false;
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
