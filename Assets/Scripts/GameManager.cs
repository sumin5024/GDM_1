using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public float runDistance = 0f; // 플레이어가 달린 총 거리
    public float arrivalDistance = 17f; // 플레이어가 달려야할 총 거리

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

    public void AddTime(float amount) // 시간 아이템 관련
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
