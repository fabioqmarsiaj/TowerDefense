﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField]
    private int enemyCount = 10;
    [SerializeField]
    private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField]
    private float delayBetweenSpawns;

    [Header("Random Delay")]
    [SerializeField]
    private float minRandomDelay;
    [SerializeField]
    private float maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= GetSpawnDelay();
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetRandomDelay();

            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(testGO, transform.position, Quaternion.identity);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;

        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBetweenSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }
}
