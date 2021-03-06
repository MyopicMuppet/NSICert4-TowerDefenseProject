﻿using UnityEngine;
using System.Collections;
public class WaveSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bossPrefab;
    public GameObject knightPrefab;
    public GameObject tankKnightPrefab;

    [Header("Spawn Point")]
    public Transform spawnPoint;

    [Header("Path to Follow")]
    public Transform[] waypoints;
    // private int currentIndex = 1;
    /// private float stoppingDistance = 1f;
    [Header("Timing")]
    public float timeBetweenWaves = 5f;

    private float countdown = 2f;
    private int waveIndex = 0;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

    }


    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(10f);
        }
        waveIndex++;
        //Debug.Log("Wave Incoming!");
    }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        clone.transform.parent = this.transform;
    }

    private void SpawnEnemies()
    {
        SpawnEnemy(knightPrefab);
        SpawnEnemy(bossPrefab);
        SpawnEnemy(tankKnightPrefab);
    }

    /*void MoveOnWaypoints()
    {
        Transform point = waypoints[currentIndex];

        float distance = Vector3.Distance(transform.position, point.position);

        if (distance < stoppingDistance)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
            {
                currentIndex = 1;
            }
        } 
    }*/

}
