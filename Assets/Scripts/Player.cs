using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    private static Player instance;
    [Header("Settings")]
    public float jumpForce = 0.34f;
    public float lowGravity = 3f;           
    public float highGravity = 2.2f;          

    [Header("References")]
    public Rigidbody PlayerRigidBody;
    public Animator PlayerAnimator;

    public CapsuleCollider PlayerCollider;

    public bool isGrounded = true;

   
    private Vector3 originalColliderCenter;


    public static Player Instance
    {
        get { return instance; } 
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

    void FixedUpdate()
    {

        // 공중에 있을 때 중력 조정
        if (!isGrounded)
        {
            if (PlayerRigidBody.linearVelocity.y > 0)
            {
                // 상승 중
                PlayerRigidBody.AddForce(Vector3.down * lowGravity, ForceMode.Acceleration);
            }
            else
            {
                // 낙하 중
                PlayerRigidBody.AddForce(Vector3.up * highGravity, ForceMode.Acceleration);
            }
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
        if (other.CompareTag("Boss"))
        {
            other.gameObject.SetActive(false);
        }
        else if (!other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }

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
            Vector3 velocity = PlayerRigidBody.linearVelocity;
            velocity.y = jumpForce;
            PlayerRigidBody.linearVelocity = velocity;



            SoundManager.instance.playerJumpSound.Play();
            Debug.Log("jump");
            PlayerAnimator.SetBool("isJumping", true);
            PlayerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }
    }
    public void Slide()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame && isGrounded)
        {
            SoundManager.instance.playerSlidingSound.Play();
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
