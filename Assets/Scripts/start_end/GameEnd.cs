using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �� ������ ���ӽ����̽� �߰�

public class GameEnd : MonoBehaviour
{
    public static bool isGameEnded = false;

    public Slider runBarSlider;
    public Animator animator;

    public Canvas gameEndCanvas;
    public TMP_Text resultText;
    public TMP_Text infoText;

    public Button restartButton;  // �� ����� ��ư�� ������ ���� �߰�
    public Button exitButton;     // ���� ���� ��ư�� ������ ���� �߰�


    void Start()
    {
        // ������ �� ĵ���� ����
        if (gameEndCanvas != null)
        {
            gameEndCanvas.enabled = false;
        }

        // ��ư�� Ŭ�� �̺�Ʈ �߰�
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);  // ��ư Ŭ�� �� RestartGame �޼��� ȣ��
        }

        // ���� ���� ��ư Ŭ�� �� ���� ���� �޼��� ȣ��
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);  // ��ư Ŭ�� �� ExitGame �޼��� ȣ��
        }
    }

    void Update()
    {
        if (isGameEnded)
        {
            // ��� LoopingZMovemet_reverse ��ũ��Ʈ�� ��Ȱ��ȭ
            LoopingZMovemet_reverse[] allMovementScripts = FindObjectsOfType<LoopingZMovemet_reverse>();
            foreach (var movementScript in allMovementScripts)
            {
                if (movementScript != null)
                {
                    movementScript.enabled = false;
                }
            }

            animator.SetBool("isEnd", true);

            // ���� ���� �� ĵ���� Ȱ��ȭ
            if (gameEndCanvas != null && !gameEndCanvas.enabled)
            {
                gameEndCanvas.enabled = true;
            }

            return; // ������ ����Ǿ����� �� �̻� ó������ ����
        }

        // ���� Ŭ����
        if (runBarSlider.value == 1)
        {
            SoundManager.instance.gameClearSound.Play();
            Debug.Log("game clear!");

            EndGame("Game Clear!", "catch the train");
        }

        // ���� ����
        if (GameManager.Instance.LimitTime < 0f)
        {
            SoundManager.instance.gameOverSound.Play(); 
            Debug.Log("game over!");

            EndGame("Game Over", "miss the train");
        }
    }

    void EndGame(string title, string message)
    {
        // ��� �ؽ�Ʈ ����
        if (resultText != null) resultText.text = title;
        if (infoText != null) infoText.text = message;

        isGameEnded = true;
    }

    // ���� ����� �޼���
    void RestartGame()
    {
        isGameEnded=false;
        GameManager.Instance.LimitTime = 60f;
        GameManager.Instance.runDistance = 0f;
        LoopingZMovemet_reverse.totalZDistance = 0f;
        // ���� �� �̸��� �� �ٽ� �ε�
        LoopingZMovemet_reverse[] allMovementScripts = FindObjectsOfType<LoopingZMovemet_reverse>();
        foreach (var movementScript in allMovementScripts)
        {
            if (movementScript != null)
            {
                movementScript.enabled = true;
            }
        }

        Collider[] allColliders = FindObjectsOfType<Collider>();
        foreach (Collider col in allColliders)
        {
            GameObject obj = col.gameObject;

            // ����: DontDestroyOnLoad�� �ƴϰ�, ���� ���� ������Ʈ�̰�, �̸����� ���͸��ϰų� �±׷� ���� ����
            if (obj.scene.IsValid() && obj.scene.name == SceneManager.GetActiveScene().name)
            {
                Destroy(obj);
            }
        }
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);  // ���� ���� �ٽ� �ε�
    }

    // ���� ���� �޼���
    void ExitGame()
    {
        // ���� ����
        Debug.Log("Exiting Game...");

        // ���� ����
        Application.Quit();

        // �����Ϳ����� ���� ���ᰡ ���� �����Ƿ�, �����Ϳ��� �׽�Ʈ �� �ֿܼ� �޽����� ��µ˴ϴ�.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
