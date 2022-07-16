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
  

    // to control how many enemies can spawn in total
    [SerializeField]
    private int enemyTotal = 3;

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
                }
                if (waveCount >= 5)
                {
                    EndEnemyWaves(); 
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
    

    
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
