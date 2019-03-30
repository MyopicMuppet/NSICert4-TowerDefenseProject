using UnityEngine;
using System.Collections;
public class WaveSpawner : MonoBehaviour
{
    public Transform knightPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 3f;

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
            SpawnKnight();
            yield return new WaitForSeconds(0.5f);
        }
        waveIndex++;
        //Debug.Log("Wave Incoming!");
    }

    private void SpawnKnight()
    {
        Instantiate(knightPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
