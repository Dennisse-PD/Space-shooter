using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject[] enemies; //added for new enemy logic movement


    private bool _stopSpawning = false;

    // Start is called before the first frame update
    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0f);
            //Add ranomizer here 4
            int randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity); //Change this to instantiate with randomizer
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }
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
            int randomPowerUp = Random.Range(0, 6);
            Instantiate(powerups[randomPowerUp], spawnPos, Quaternion.identity);

            if(randomPowerUp == 5)
            {
                yield return new WaitForSeconds(Random.Range(10, 20));
                Debug.Log("The Omnishot power-up has spawned");
            }
            
            
                Debug.Log("Any other power up was spawned");
                yield return new WaitForSeconds(Random.Range(5, 8));
            
            
            //MAKE THE OMNISHOT POWER UP SPAWN RARELY
        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}