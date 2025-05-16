using UnityEngine;

public class Time_Items : MonoBehaviour
{
    public float timeChangeAmount = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddTime(timeChangeAmount);
            {
                Debug.Log("시간 증가");

                FindObjectOfType<Item_Spawner>()?.OnTimeItemCollected(); // 스포너호출
                Destroy(gameObject);
            }
        }
    }
}
