using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PowerUpSpawner : MonoBehaviour
{
   
    [SerializeField]
    private GameObject[] PowerUps;
    [SerializeField]
    private bool _stopSpawning = false;

   


    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(RarePowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {


    }
   

    IEnumerator SpawnPowerUpRoutine()
    {
        
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPowerUp = Random.Range(0, 4); 
                                                  
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
            int randomPowerUp = Random.Range(5, 7); 
                                                    
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(20.0f);


        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

