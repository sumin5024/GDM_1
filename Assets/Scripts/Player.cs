using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static Player instance;
    [Header("Settings")]
    public float jumpForce;

    [Header("References")]
    public Rigidbody PlayerRigidBody;
    public Animator PlayerAnimator;

    public CapsuleCollider PlayerCollider;

    public bool isGrounded = true;

    public static Player Instance
    {
        get { return instance; } 
    }

    private void Start()
    {
        //transform.position = new Vector3(0.347f, 0.015f, -0.96f);
    }

    private void Update()
    {
        Jump();
        if(isGrounded )
        {
            PlayerAnimator.SetBool("isJumping", false);
        }
            
    }

    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void Awake()
    {
        Physics.gravity = new Vector3(0, -2.5f, 0);
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded && !PlayerAnimator.GetBool("isJumping"))
        {

            Debug.Log("jump");
            PlayerAnimator.SetBool("isJumping", true);
            //PlayerRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
            PlayerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }
    }
}
