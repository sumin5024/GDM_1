using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public float runDistance = 0f; // �÷��̾ �޸� �� �Ÿ�
    public float arrivalDistance = 17f; // �÷��̾ �޷����� �� �Ÿ�

    public float LimitTime = 60f;
    public TextMeshProUGUI text_Timer;

    public Slider runBarSlider;

    public bool isSpawn = true;
    void Start()
    {
        runDistance = 0f;
    }
   

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void AddTime(float amount) // �ð� ������ ����
    {
        LimitTime += amount;
        if (LimitTime < 0f) LimitTime = 0f; 
    }

    

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        CheckRun();
        LimitTime -= Time.deltaTime;
        if(LimitTime < 0f)
        {
            Debug.Log("GameOver!!");
        }
        text_Timer.text =  Mathf.Round(LimitTime) + "";
    }

    public void CheckRun()
    {
        if(runBarSlider != null)
        {
            runBarSlider.value = runDistance/arrivalDistance;
        }
    }

    
}
