using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private int waveCount;
    private float spawnRate = 1.0f; 
    private float timesBetweenWaves = 5.0f;
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

    private bool _stopSpawning = false;

    //Flickering Wave Counter Text Variables
    [SerializeField]
    private Text _waveCountTxt;
    private float waveTextTimer = 1.0f;


    // Start is called before the first frame update
    void Start()
    {    
        StartCoroutine(waveSpawner());
        //StartCoroutine(WaveCountFlicker());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {   
      
    }
    IEnumerator waveSpawner()
    {

        while (isWaveDone == true)
        {
            
            Vector3 spawnPos = new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0f);
            int randomEnemy = Random.Range(0, 5);
            isWaveDone = false; //create a method for this called waveIsActive or something like that

            for (int i = 0; i < enemyCount; i++)
            {
                if (enemyCount <= enemyTotal)
                {
                    ActivateWaveText();
                    yield return new WaitForSeconds(waveTextTimer);
                    _waveCountTxt.gameObject.SetActive(false);
                   // GameObject _enemyPrefab = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity);

                    GameObject enemyClone = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity); //add spawnPos and Quaternion to it after first test
                    yield return new WaitForSeconds(spawnRate);
                }
                if (waveCount >= 5)
                {

                    EndEnemyWaves();
                    //StopCoroutine(waveSpawner());
                    Debug.Log("Final Wave! Enter Boss Fight!");
                    //boss starts
                }
            }
            spawnRate -= 1.0f;
          //  waveTextTimer -= 1.0f;
            enemyCount += 1;
            enemyTotal += 1;
            yield return new WaitForSeconds(timesBetweenWaves);
            waveCount += 1;
            isWaveDone = true;
        }
    }

    void EndEnemyWaves()
    {
        _stopSpawning = true;
        isWaveDone = true;
        enemyCount = 0;
        enemyCount = 0;
        spawnRate = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }

       
    }
    private void ActivateWaveText()
    {
        _waveCountTxt.text = "Wave: " + waveCount.ToString();
        
        _waveCountTxt.gameObject.SetActive(true);
        // StartCoroutine(WaveCountFlicker());
        
    }
    IEnumerator WaveCountFlicker()
    {
        while (isWaveDone == true) //check the condition 
        {
            
            _waveCountTxt.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _waveCountTxt.enabled = true;
            yield return new WaitForSeconds(0.5f);  
        }
        
        _waveCountTxt.enabled = false;


    }
  
    IEnumerator TickFiveSeconds()
    {
        var wait = new WaitForSeconds(1f);
        int counter = 1;
        while (counter < 5)
        {
            WaveCountFlicker();
            counter++;
            yield return wait;
        }
        _stopSpawning = true;
        _waveCountTxt.enabled = false;
    }

    //NEW ENEMY MOVEMENT LOGIC
    //Randomize the odds of spawning an enemy with a diffrent move-set
    //spawn it with an array that had a randomize index value, the array is the two+ enemy types since I have to creat an aggressive enemy too

    IEnumerator SpawnPowerUpRoutine()
    {
        //set the rarity here so for the first few power ups set a random and then for the last few set another
        yield return new WaitForSeconds(5.0f);
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
