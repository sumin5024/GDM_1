using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    public GameObject uiCanvas;

    // 애니메이션 타임라인에서 호출될 함수
    public void ShowCanvas() => uiCanvas.SetActive(true);
    public void HideCanvas() => uiCanvas.SetActive(false);
}
