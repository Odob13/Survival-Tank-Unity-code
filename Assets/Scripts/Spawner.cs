using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//spawn waves of enemies increasing the number of enemies in each wave and when is no more enemies start the time between waves, the enemies will spawn in spawnpoints array randomly, max 20 enemies spawn in the same time

public class Spawner : MonoBehaviour
{


    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    public Text waveCountdownText;

    public Text waveNumberText;

    public float timeBetweenWaves = 10f;

    public int maxEnemies = 20;

    public float countdown = 2f;

    public RemainingEnemysGui wavebool;

    private int waveIndex = 3;

    private int waveNumber = 0;

    void Update()
    {
        waveCountdownText.text = "Time remaining to the next wave: " + Mathf.Floor(countdown).ToString();
        waveNumberText.text = "Wave: " + waveNumber.ToString();
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
        }
        else
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                countdown -= Time.deltaTime;
            }
        }
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        for (int i = 0; i < waveIndex; i++)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
                countdown = timeBetweenWaves;
            }
            else
            {
                yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies);
            }
            yield return new WaitForSeconds(0.2f);
        }
        wavebool.wavecount = true;
        waveIndex = waveIndex + waveIndex;
    }
}
