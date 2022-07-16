using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] PowerUps;
    [SerializeField]
    private GameObject[] enemies; 

    
    

    //Spawn Control
    private int waveCount;
    private float spawnRate = 1.0f; 
    private float timeBetweenWaves = 5.0f;
    [SerializeField]
    private int enemyCount;
    private bool _isWaveActive = true;
    private bool _stopSpawning = false;


    // Wave Counter Text Variables
    [SerializeField]
    private Text _waveCountTxt;
    private float waveTextTimer = 1.0f;
    [SerializeField]
    private Text _bossWaveIndicatorText;


    //Boss
    public bool _isFinalWaveStarted = false;
    private Player _player;


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
       
        while (_isWaveActive == true && _stopSpawning == false)
        {
            
            Vector3 spawnPos = new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0f);
            int randomEnemy = Random.Range(0, 5);
            _isWaveActive = false; 

            for (int i = 0; i < enemyCount; i++)
            {
               
                    ActivateWaveText();
                    yield return new WaitForSeconds(waveTextTimer);
                    _waveCountTxt.gameObject.SetActive(false);
                  

                    GameObject enemyClone = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity); 
                    yield return new WaitForSeconds(spawnRate);
              
                if (waveCount == 5)
                {
                    _bossWaveIndicatorText.gameObject.SetActive(true);
                    EndEnemyWaves();
                    yield return new WaitForSeconds(3f);
                    Debug.Log("Final Wave! Enter Boss Fight!");
                    SceneManager.LoadScene(2);
                }
            }
                spawnRate -= 1.0f;
                enemyCount += 1;
                yield return new WaitForSeconds(timeBetweenWaves);
                waveCount += 1;
                _isWaveActive = true;
            
           
        }
       
    }

    public void EndEnemyWaves()
    {
        _stopSpawning = true;
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
    
  
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPowerUp = Random.Range(0, 8); 
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }

    }
    IEnumerator RarePowerUpRoutine()
    {
        
        yield return new WaitForSeconds(15.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPowerUp = Random.Range(4, 8); 
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(25.0f);


        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        _isWaveActive = false;
    }
   
}
