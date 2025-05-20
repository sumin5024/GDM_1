using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item_Spawner : MonoBehaviour
{
    [Header("Item Settings")]
    public GameObject[] speedItems; // 속도 관련 아이템 
    public GameObject[] timeItems;  // 시간 관련 아이템 
    public GameObject[] shieldItem; // 쉴드 

    public float fixedX = 0.347f;
    public Vector2 spawnYRange = new Vector2(0.01f, 0.05f);
    public Vector2 spawnZRange = new Vector2(-0.66f, 0.54f);

    public float spawnDelay = 5f; // 최소 5초 간격
    private float lastSpawnTime = -999f;

    private bool speedItemSpawned = false;
    private bool timeItemSpawned = false;
    private bool shieldItemSpawned = false;

    void Update()
    {
        float progress = GameManager.Instance.runBarSlider.value;

        if (progress <= 0.5f && Time.time - lastSpawnTime >= spawnDelay)
        {
            TrySpawnItem();
        }


        // if (Time.time - lastSpawnTime >= spawnDelay)
        // {
        //     TrySpawnItem();
        // }
    }

    void TrySpawnItem()
    {
        lastSpawnTime = Time.time;

        // 아이템 스폰 위치 결정
        Vector3 spawnPos = new Vector3(
            fixedX,
            Random.Range(spawnYRange.x, spawnYRange.y),
            Random.Range(spawnZRange.x, spawnZRange.y)
        );

        
        float checkRadius = 0.3f;
        Collider[] colliders = Physics.OverlapSphere(spawnPos, checkRadius);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Obstacle") || col.CompareTag("Item"))
            {
                
                return;
            }
        }
        //모든 콜라이더가 아이템이나 장애물이면 스폰 안함
        
        
        List<int> availableTypes = new List<int>();
        if (!speedItemSpawned && speedItems.Length > 0) availableTypes.Add(0);
        if (!timeItemSpawned && timeItems.Length > 0) availableTypes.Add(1);
        if (!shieldItemSpawned && shieldItem.Length > 0) availableTypes.Add(2);

        if (availableTypes.Count == 0) return; 

        
        int selectedType = availableTypes[Random.Range(0, availableTypes.Count)];

        switch (selectedType)
        {
            case 0:
                GameObject speed = speedItems[Random.Range(0, speedItems.Length)];
                Instantiate(speed, spawnPos, Quaternion.identity);
                speedItemSpawned = true;
                break;

            case 1:
                GameObject time = timeItems[Random.Range(0, timeItems.Length)];
                Instantiate(time, spawnPos, Quaternion.identity);
                timeItemSpawned = true;
                break;

            case 2:
                GameObject shield = shieldItem[Random.Range(0, shieldItem.Length)];
                Instantiate(shield, spawnPos, Quaternion.identity);
                shieldItemSpawned = true;
                break;
        }
    }

    public void OnSpeedItemCollected()
    {
        speedItemSpawned = false;
    }

    public void OnTimeItemCollected()
    {
        timeItemSpawned = false;
    }

    public void OnShieldItemCollected()
    {
        shieldItemSpawned = false;
    }
}
