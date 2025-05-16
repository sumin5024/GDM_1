using UnityEngine;
using TMPro;

public class GameEnd : MonoBehaviour
{
    private bool isGameEnded = false;

    public GameObject target;
    public GameObject Camera;
    public Animator animator;

    public Canvas gameEndCanvas;         
    public TMP_Text resultText;          
    public TMP_Text infoText;            

    void Start()
    {
        // 시작할 때 캔버스 끄기
        if (gameEndCanvas != null)
        {
            gameEndCanvas.enabled = false;
        }
    }

    void Update()
    {
        if (isGameEnded)
        {
            animator.SetBool("isEnd", true);

            // 게임 종료 후 캔버스 활성화
            if (gameEndCanvas != null && !gameEndCanvas.enabled)
            {
                gameEndCanvas.enabled = true;
            }

            return; // 게임이 종료되었으면 더 이상 처리하지 않음
        }

        // 게임 클리어
        if (GameManager.Instance.arrivalDistance < GameManager.Instance.runDistance)
        {
            Debug.Log("game clear!");

            EndGame("Game Clear!", "catch the train");
        }

        // 게임 오버
        if (GameManager.Instance.LimitTime < 0f)
        {
            Debug.Log("game over!");

            EndGame("Game Over", "miss the train");
        }
    }

    void EndGame(string title, string message)
    {
        DisableMovement();

        if (resultText != null) resultText.text = title;
        if (infoText != null) infoText.text = message;

        isGameEnded = true;
    }

    void DisableMovement()
    {
        if (target != null && Camera != null)
        {
            var movementScript = target.GetComponent<LoopingZMovement>();
            var movementScript_Camera = Camera.GetComponent<LoopingZMovement>();
            if (movementScript != null) movementScript.enabled = false;
            if (movementScript_Camera != null) movementScript_Camera.enabled = false;
        }
    }
}
