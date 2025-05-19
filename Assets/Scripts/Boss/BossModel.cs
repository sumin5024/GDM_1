using UnityEngine;

public class BossModel : MonoBehaviour
{
    public float speed = 1;
    private void OnEnable()
    {
        transform.localPosition = Vector3.forward * 0.5f;
    }
    private void Update()
    {
        if(transform.localPosition.z > 0)
        {
            transform.localPosition += Vector3.back*Time.deltaTime * speed;
        }
    }
}
