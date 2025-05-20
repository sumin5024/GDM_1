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
        // 시작할 때 캔버스와 이미지 끄기
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
            // 모든 LoopingZMovemet_reverse 스크립트를 비활성화
            LoopingZMovemet_reverse[] allMovementScripts = FindObjectsOfType<LoopingZMovemet_reverse>();
            foreach (var movementScript in allMovementScripts)
            {
                if (movementScript != null)
                {
                    movementScript.enabled = false;
                }
            }

            animator.SetBool("isEnd", true);

            // 게임 종료 후 캔버스 활성화
            if (gameEndCanvas != null && !gameEndCanvas.enabled)
            {
                gameEndCanvas.enabled = true;
            }

            return;
        }

        // 게임 클리어
        if (runBarSlider.value == 1)
        {
            SoundManager.instance.gameClearSound.Play();
            Debug.Log("game clear!");

            EndGame(true);  // true는 클리어
        }

        // 게임 오버
        if (GameManager.Instance.LimitTime < 0f)
        {
            SoundManager.instance.gameOverSound.Play();
            Debug.Log("game over!");

            EndGame(false); // false는 실패
        }
    }

    void EndGame(bool isClear)
    {
        isGameEnded = true;

        if (GameClearimage != null)
            GameClearimage.gameObject.SetActive(isClear);  // 클리어 시만 보이기

        if (GameOverimage != null)
            GameOverimage.gameObject.SetActive(!isClear); // 실패 시만 보이기
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
