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
    public float moveSpeed = 1.0f;      // 단위: 유닛/초
    public float arriveThreshold = 0.001f;
    public GameObject uiCanvas;

    [Header("References")]
    public Animator animator;           // isstop 파라미터가 있는 Animator

    private bool isMoving = false;
    private bool readyForInput = false; // 키 입력 대기 여부


    void Start()
    {
        // 시작 위치 세팅
        Vector3 p = transform.position;
        p.z = startZ;
        transform.position = p;

        // 자동으로 이동 코루틴 시작
        StartCoroutine(MoveZ());
    }

    private IEnumerator MoveZ()
    {
        isMoving = true;
        animator.SetBool("isstop", false);

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, targetZ);

        // 목표 도달할 때까지 MoveTowards
        while (Mathf.Abs(transform.position.z - targetZ) > arriveThreshold)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 정확히 정렬
        //transform.position = targetPos;

        // 도착 처리
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
