using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    public GameObject uiCanvas;

    // �ִϸ��̼� Ÿ�Ӷ��ο��� ȣ��� �Լ�
    public void ShowCanvas() => uiCanvas.SetActive(true);
    public void HideCanvas() => uiCanvas.SetActive(false);
}
