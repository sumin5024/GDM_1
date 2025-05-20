using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public static bool isGameEnded = false;

    public Slider runBarSlider;
    public Animator animator;

    public Canvas gameEndCanvas;
    public Image GameClearimage;
    public Image GameOverimage;

    public Button restartButton;
    public Button exitButton;

    void Start()
    {
        // ������ �� ĵ������ �̹��� ����
        if (gameEndCanvas != null)
            gameEndCanvas.enabled = false;

        if (GameClearimage != null)
            GameClearimage.gameObject.SetActive(false);

        if (GameOverimage != null)
            GameOverimage.gameObject.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
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

            return;
        }

        // ���� Ŭ����
        if (runBarSlider.value == 1)
        {
            SoundManager.instance.gameClearSound.Play();
            Debug.Log("game clear!");

            EndGame(true);  // true�� Ŭ����
        }

        // ���� ����
        if (GameManager.Instance.LimitTime < 0f)
        {
            SoundManager.instance.gameOverSound.Play();
            Debug.Log("game over!");

            EndGame(false); // false�� ����
        }
    }

    void EndGame(bool isClear)
    {
        isGameEnded = true;

        if (GameClearimage != null)
            GameClearimage.gameObject.SetActive(isClear);  // Ŭ���� �ø� ���̱�

        if (GameOverimage != null)
            GameOverimage.gameObject.SetActive(!isClear); // ���� �ø� ���̱�
    }

    void RestartGame()
    {
        isGameEnded = false;
        GameManager.Instance.LimitTime = 60f;
        GameManager.Instance.runDistance = 0f;
        LoopingZMovemet_reverse.totalZDistance = 0f;

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
            if (obj.scene.IsValid() && obj.scene.name == SceneManager.GetActiveScene().name)
            {
                Destroy(obj);
            }
        }

        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
