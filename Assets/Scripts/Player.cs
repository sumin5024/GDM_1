using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    private static Player instance;
    [Header("Settings")]
    public float jumpForce;

    [Header("References")]
    public Rigidbody PlayerRigidBody;
    public Animator PlayerAnimator;
    public Animator PlayerAnimator_slide;

    public CapsuleCollider PlayerCollider;

    public bool isGrounded = true;


   
    private Vector3 originalColliderCenter;



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
        Slide();
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
        originalColliderCenter = PlayerCollider.center;
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
    public void Slide()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame && isGrounded)
        {

            Debug.Log("slide");
            PlayerAnimator.SetBool("isSliding", true);
            PlayerCollider.direction = 2;
            PlayerCollider.center = Vector3.zero;
            //PlayerRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;

            
            StartCoroutine(StopSlidingAfterDelay(0.5f)); // 슬라이딩 시간 설정
        }
    }
    private System.Collections.IEnumerator StopSlidingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerAnimator.SetBool("isSliding", false);
        PlayerCollider.direction = 1;
        // 애니메이션 상태 복원
        PlayerCollider.center = originalColliderCenter;
        
        

        // 콜라이더 방향 원래대로 (Z축 → Y축)
        
    }

}
