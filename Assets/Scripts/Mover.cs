using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;  
    }
}
