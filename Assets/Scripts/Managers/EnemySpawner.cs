using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _enemyContainer;


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
    private GameObject[] catchEnemies;

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
     
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator waveSpawner()
    {

        while (isWaveDone == true && _isFinalWaveStarted == false)// waveCount < 5 )
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
                    //instantiate boss here! The boss will have its own script which will start on instantate
                    //instantate in position 0,11.22,0

                    EndEnemyWaves(); //might remove this from here since it's being called from the next scene where the boss is


                    Debug.Log("Final Wave! Enter Boss Fight!");
                    //boss starts
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
        // _stopSpawning = true;
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

    
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
