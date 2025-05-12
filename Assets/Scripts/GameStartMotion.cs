using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameStartMotion : MonoBehaviour
{
    [Header("Movement Settings")]
    public float startZ = -0.96f;
    public float targetZ = 0.0609f;
    public float moveSpeed = 1.0f;      // ����: ����/��
    public float arriveThreshold = 0.001f;
    public GameObject uiCanvas;

    [Header("References")]
    public Animator animator;           // isstop �Ķ���Ͱ� �ִ� Animator

    private bool isMoving = false;
    private bool readyForInput = false; // Ű �Է� ��� ����


    void Start()
    {
        // ���� ��ġ ����
        Vector3 p = transform.position;
        p.z = startZ;
        transform.position = p;

        // �ڵ����� �̵� �ڷ�ƾ ����
        StartCoroutine(MoveZ());
    }

    private IEnumerator MoveZ()
    {
        isMoving = true;
        animator.SetBool("isstop", false);

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, targetZ);

        // ��ǥ ������ ������ MoveTowards
        while (Mathf.Abs(transform.position.z - targetZ) > arriveThreshold)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // ��Ȯ�� ����
        //transform.position = targetPos;

        // ���� ó��
        animator.SetBool("isstop", true);
        isMoving = false;
        uiCanvas.SetActive(true);
        readyForInput = true;

    }
    void Update()
    {
        if (readyForInput && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
