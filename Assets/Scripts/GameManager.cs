using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public bool isSpawn = true;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
