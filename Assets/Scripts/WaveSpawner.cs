using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        // run through the waves defined in the unity inspector
        for (int i = 0; i < currentWave.count; i ++)
        {
            //check if player deaded
            if (player == null)
            {
                Debug.Log("SpwanWave: player is dead!");
                yield break;
            }

            //spawn a new enemy - select a random one
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomEnemySpwan = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomEnemySpwan);

            //check if done spawning enemies in current wave
            if (i == currentWave.count - 1)
            {
                finishedSpawning = true;
            } 
            else
            {
                finishedSpawning = false;
            }

            //wait for time between spawns
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if (finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            //check if new wave exists to start
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                Debug.Log("starting wave: " + currentWaveIndex);
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                //player is still alive at this point, there just aren't any more waves
                Debug.Log("GAME COMPLETED!");
            }

        }
    }

}
