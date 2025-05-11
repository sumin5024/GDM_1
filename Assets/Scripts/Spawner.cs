using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    private float fixedX = 0.347f;
    public float groundY = 0.0141f;
    public Vector2 airYRange = new Vector2(0.125f, 0.15f);
    public Vector2 ZRange = new Vector2(-0.66f, 0.54f);
    //public float minDistance = 3f;

    private Vector3 lastSpawnPos;

    [Header("References")]
    public GameObject[] gameObjects;

    private void Start()
    {
        lastSpawnPos = new Vector3(fixedX, 0f, 0f);
        Spawn();
    }

    private void Update()
    {
        if(GameManager.Instance.isSpawn == true)
        {
            DeleteAllObstacles();
            Spawn();
            GameManager.Instance.isSpawn = false;
        }
    }

    void Spawn()
    {
        Vector3 spawnPos;
        int randRange = Random.Range(0, gameObjects.Length);
        GameObject randomObject = gameObjects[randRange];
        float randY;
        float randZ = Random.Range(ZRange.x, ZRange.y);
        if(randRange < 3) // 땅에 있는 물체
        {
            randY = groundY;
        }
        else // 공중에 있는 물체
        {
            randY = Random.Range(airYRange.x, airYRange.y);
        }

        spawnPos = new Vector3(fixedX, randY, randZ);
        
        Instantiate(randomObject, spawnPos, Quaternion.identity);
        
    }

    void DeleteAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach(GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }
}
