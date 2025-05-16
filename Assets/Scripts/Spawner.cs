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

    public GameObject[] itemObjects;

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
       
        Vector3 spawnPos_item;
        int randRange_item = Random.Range(0, itemObjects.Length);
        GameObject randomObject_item = itemObjects[randRange_item];
      
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
        
        float randY_item;
        float randZ_item = Random.Range(ZRange.x, ZRange.y);
        if (randRange < 3) // 땅에 있는 물체
        {
            randY_item = groundY;
        }
        else // 공중에 있는 물체
        {
            randY_item = Random.Range(airYRange.x, airYRange.y);
        }

        spawnPos_item = new Vector3(fixedX, randY_item, randZ_item);
          
        Instantiate(randomObject, spawnPos, Quaternion.identity);
        Instantiate(randomObject_item, spawnPos_item, Quaternion.identity);

    }

    void DeleteAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] obstacles_ITEM = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
           
        }
        foreach (GameObject obstacle in obstacles_ITEM)
        {
            Destroy(obstacle);

        }

    }
   
}
