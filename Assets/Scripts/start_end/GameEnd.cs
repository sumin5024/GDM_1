using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 관리용 네임스페이스 추가

public class GameEnd : MonoBehaviour
{
    public static bool isGameEnded = false;

    public Slider runBarSlider;
    public Animator animator;

    public Canvas gameEndCanvas;
    public TMP_Text resultText;
    public TMP_Text infoText;

    public Button restartButton;  // 씬 재시작 버튼을 연결할 변수 추가
    public Button exitButton;     // 게임 종료 버튼을 연결할 변수 추가


    void Start()
    {
        // 시작할 때 캔버스 끄기
        if (gameEndCanvas != null)
        {
            gameEndCanvas.enabled = false;
        }

        // 버튼에 클릭 이벤트 추가
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);  // 버튼 클릭 시 RestartGame 메서드 호출
        }

        // 게임 종료 버튼 클릭 시 게임 종료 메서드 호출
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);  // 버튼 클릭 시 ExitGame 메서드 호출
        }
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

            return; // 게임이 종료되었으면 더 이상 처리하지 않음
        }

        // 게임 클리어
        if (runBarSlider.value == 1)
        {
            SoundManager.instance.gameClearSound.Play();
            Debug.Log("game clear!");

            EndGame("Game Clear!", "catch the train");
        }

        // 게임 오버
        if (GameManager.Instance.LimitTime < 0f)
        {
            SoundManager.instance.gameOverSound.Play(); 
            Debug.Log("game over!");

            EndGame("Game Over", "miss the train");
        }
    }

    void EndGame(string title, string message)
    {
        // 결과 텍스트 설정
        if (resultText != null) resultText.text = title;
        if (infoText != null) infoText.text = message;

        isGameEnded = true;
    }

    // 게임 재시작 메서드
    void RestartGame()
    {
        isGameEnded=false;
        GameManager.Instance.LimitTime = 60f;
        GameManager.Instance.runDistance = 0f;
        LoopingZMovemet_reverse.totalZDistance = 0f;
        // 현재 씬 이름을 얻어서 다시 로드
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

            // 조건: DontDestroyOnLoad가 아니고, 씬에 속한 오브젝트이고, 이름으로 필터링하거나 태그로 구분 가능
            if (obj.scene.IsValid() && obj.scene.name == SceneManager.GetActiveScene().name)
            {
                Destroy(obj);
            }
        }
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);  // 현재 씬을 다시 로드
    }

    // 게임 종료 메서드
    void ExitGame()
    {
        // 게임 종료
        Debug.Log("Exiting Game...");

        // 게임 종료
        Application.Quit();

        // 에디터에서는 실제 종료가 되지 않으므로, 에디터에서 테스트 시 콘솔에 메시지가 출력됩니다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
