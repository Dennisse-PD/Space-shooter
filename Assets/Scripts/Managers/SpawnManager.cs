using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] PowerUps;
    [SerializeField]
    private GameObject[] enemies; //added for new enemy logic movement

    //Added for the Wave System Logic
    [SerializeField]
    private TextMeshProUGUI waveCountText;
    private int waveCount = 0;
    [SerializeField]
    private float spawnRate = 30.0f; 
    [SerializeField]
    private float timesBetweenWaves = 50.0f;
    [SerializeField]
    private int enemyCount;
    bool isWaveDone = true;
    [SerializeField]
    private GameObject enemyForWave;
    // to control how many enemies can spawn in total
    [SerializeField]
    private int enemyTotal = 3;
    //to destroy all enemies and make way for the boss
    private  GameObject[] catchEnemies;

    private bool _canSpawn; //??? Old code residue 
    private bool _stopSpawning = false;


    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartSpawnRoutines()
    {
       // StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        waveCountText.text = "Wave: " + waveCount.ToString();
      if(isWaveDone == true)
        {
         StartCoroutine(waveSpawner());
        }
      
    }
    IEnumerator waveSpawner()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0f);
        int randomEnemy = Random.Range(0, 5);
        isWaveDone = false; //create a method for this called waveIsActive or something like that
        
        for(int i = 0; i < enemyCount; i++)
        {
            if(enemyCount <= enemyTotal)
            {
                GameObject enemyClone = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity); //add spawnPos and Quaternion to it after first test
                yield return new WaitForSeconds(spawnRate);
            }
            if (waveCount >= 4)
            {
                endEnemyWaves();
                //boss starts
            }

        }
        spawnRate -= 0.1f;
        enemyCount += 1;
        waveCount += 1;
        yield return new WaitForSeconds(timesBetweenWaves);
        isWaveDone = true;
    }

    void endEnemyWaves()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }

       _stopSpawning = true;
        isWaveDone = true;
    }

    //NEW ENEMY MOVEMENT LOGIC
    //Randomize the odds of spawning an enemy with a diffrent move-set
    //spawn it with an array that had a randomize index value, the array is the two+ enemy types since I have to creat an aggressive enemy too

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPowerUp = Random.Range(0, 7); //make more and change the value 
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);


        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
