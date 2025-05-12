using UnityEngine;

public class BossModel : MonoBehaviour
{
    public float speed = 1;
    private void OnEnable()
    {
        transform.localPosition = Vector3.right * 0.5f;
    }
    private void Update()
    {
        if(transform.localPosition.x > 0)
        {
            transform.localPosition += Vector3.left*Time.deltaTime * speed;
        }
    }
}
