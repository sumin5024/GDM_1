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
                Destroy(gameObject);
            }
        }
    }
}
