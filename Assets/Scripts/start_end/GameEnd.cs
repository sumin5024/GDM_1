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
        // ������ �� ĵ���� ����
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

            // ���� ���� �� ĵ���� Ȱ��ȭ
            if (gameEndCanvas != null && !gameEndCanvas.enabled)
            {
                gameEndCanvas.enabled = true;
            }

            return; // ������ ����Ǿ����� �� �̻� ó������ ����
        }

        // ���� Ŭ����
        if (GameManager.Instance.arrivalDistance < GameManager.Instance.runDistance)
        {
            Debug.Log("game clear!");

            EndGame("Game Clear!", "catch the train");
        }

        // ���� ����
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
