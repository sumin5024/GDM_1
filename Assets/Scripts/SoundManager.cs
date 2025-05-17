using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource playerJumpSound;
    public AudioSource playerSlidingSound;
    public AudioSource gameClearSound;
    public AudioSource gameOverSound;
    public AudioSource getPItemSound;
    public AudioSource getNItemSound;
    public AudioSource bossSound;
    public AudioSource clickSound;
    


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(gameObject);
    }
}
