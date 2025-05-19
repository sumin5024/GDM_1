using UnityEngine;
using System.Collections;
public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    private float fixedX = 0.359f;
    public float groundY = 0.0141f;
    public Vector2 airYRange = new Vector2(0.125f, 0.15f);
    public Vector2 ZRange = new Vector2(-0.66f, 0.54f);
    //public float minDistance = 3f;
    private float spawnInterval = 2f;  // 3�� ����
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
        float randZ = 0f;
        if(randRange < 3) // ���� �ִ� ��ü
        {
            randY = groundY;
        }
        else // ���߿� �ִ� ��ü
        {
            randY = Random.Range(airYRange.x, airYRange.y);
        }

        spawnPos = new Vector3(fixedX, randY, randZ);
        
       
        Instantiate(randomObject, spawnPos, Quaternion.identity);
       
    }

    void DeleteAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
       
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
           
        }

    }
    
}
