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
    private GameObject[] PowerUps;
    [SerializeField]
    private GameObject[] enemies; //added for new enemy logic movement

    //Added for the Wave System Logic
    private int _currentEnemies = 5;
    


    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }
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
        while (_currentEnemies > 0 && _stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0f);
            //Add ranomizer here 4
            
            int randomEnemy = Random.Range(0, 4);
            GameObject newEnemy = Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity); //Change this to instantiate with randomizer
            newEnemy.transform.parent = _enemyContainer.transform;
          ///  _currentEnemies--;
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
            int randomPowerUp = Random.Range(0, 7); //make more and change the value 
            Instantiate(PowerUps[randomPowerUp], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
            
     
        }

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    //I could create  range for random enemies and random powerup
    //use the same condition as the Shield Enemy to determine rarity
    //I should make it a switch statement since I need multiple cases
    //since I am dealing with power-ups and enemies, I should probably have a switch statement for each of those
    //Have seperate arrays for each power up set (rare and common)

    //WAVE SYSTEM LOGIC
/*    Modify the Spawn Manager Script:
      Add a way to see how many enemies are currently in
      Decrease enemy quantity everytime it runs? could also call enemy-- on enemy death
      Don't spawn a new wave until all current enemies are defeated 
      Delay the time that it takes for them to spawn so that I can display a warning text

     I also need to find a way to diversify the enemy types per wave*/

}