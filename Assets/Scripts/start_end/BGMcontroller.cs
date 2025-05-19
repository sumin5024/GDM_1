using UnityEngine;

public class BGMcontroller : MonoBehaviour
{
    public AudioSource backgroundMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (backgroundMusic != null && backgroundMusic.isPlaying&& GameEnd.isGameEnded)            
    {
        backgroundMusic.Stop();  // 오디오 소스 멈추기
    }
}
}
