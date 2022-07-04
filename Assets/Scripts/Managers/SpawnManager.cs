using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private int finalWave = 5;

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

    //Boss
    public bool _isFinalWaveStarted = false;


    // Start is called before the first frame update
    void Start()
    {    
        StartCoroutine(waveSpawner());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(RarePowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {   
      
    }
    IEnumerator waveSpawner()
    {

        while (isWaveDone == true && _isFinalWaveStarted == false)
        {
            
            Vector3 spawnPos = new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0f);
            int randomEnemy = Random.Range(0, 5);
            isWaveDone = false; 

            for (int i = 0; i < enemyCount; i++)
            {
                if (enemyCount <= enemyTotal)
                {
                    ActivateWaveText();
                    yield return new WaitForSeconds(waveTextTimer);
                    _waveCountTxt.gameObject.SetActive(false);
                  

                    GameObject enemyClone = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity); 
                    yield return new WaitForSeconds(spawnRate);
                }
                if (waveCount >= 5)
                {
                    

                    EndEnemyWaves(); 
                    Debug.Log("Final Wave! Enter Boss Fight!");
                   
                }
            }
                spawnRate -= 1.0f;
                enemyCount += 1;
                enemyTotal += 1;
                yield return new WaitForSeconds(timesBetweenWaves);
                waveCount += 1;
                isWaveDone = true;
            
           
        }
        SceneManager.LoadScene(2);
    }

    public void EndEnemyWaves()
    {
        _isFinalWaveStarted = true;
        isWaveDone = true;
        _waveCountTxt.gameObject.SetActive(false);
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
        
    }
    IEnumerator WaveCountFlicker()
    {
        while (isWaveDone == true)
        {
            
            _waveCountTxt.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _waveCountTxt.enabled = true;
            yield return new WaitForSeconds(0.5f);  
        }
        
        _waveCountTxt.enabled = false;


    }
  
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPowerUp = Random.Range(0, 3); 
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);


        }

    }
    IEnumerator RarePowerUpRoutine()
    {
        
        yield return new WaitForSeconds(10.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPowerUp = Random.Range(4, 7); 
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(20.0f);


        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
