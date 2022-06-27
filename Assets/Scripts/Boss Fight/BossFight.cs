using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    //HP Bar Variables
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //Boss entrance
    private Vector3 endPosition = new Vector3(0, 3.95f, 0);

    //Boss Phases
    private bool _isPhase1Started = false; 
    private bool _isPhase2Started = false;
    private bool _isPhase3Started = false;


    //Prefab
    [SerializeField]
    private GameObject _laserPrefab;
    private float _speed = 2;

    // Side to Side Movement Variables
    private float _distance = 5f;
    private Vector3 StartingPos;
    private float _laserAttackSpeed = 1f;// might or might not use
    private bool _isLongLaserActive = false;

    //Long Laser Variables
    [SerializeField]
    private GameObject LaserVisualizer;

    //Damage Effects
    [SerializeField]
    private GameObject DamageLeftVisualizer;
    [SerializeField]
    private GameObject DamageRightVisualizer;

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        StartingPos = transform.position;
        SpawnManager spawnManager = GetComponent<SpawnManager>();
        spawnManager.EndEnemyWaves();

    }

    // Update is called once per frame
    void Update()
    {
        //Boss entrance
        if (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, _speed * Time.deltaTime);
            //start boss phases here

        }
        CheckPhases();

        //Switch Boss Phase depending on HP Levels


    }
     public void DamageBoss(int damage)
    {
       
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
       
          DamageBoss(5);
        }
      
    }
    void CheckPhases()
    {
      if(currentHealth > 50)
        {
            Debug.Log("This is phase 1");
        }
      if(currentHealth <= 50)
        {
            Debug.Log("This is Phase 2");
            DamageLeftVisualizer.SetActive(true);
        }
        if(currentHealth <= 30)
        {
            Debug.Log("This is the Final Phase");
            DamageRightVisualizer.SetActive(true);
        }
    }
    void FireLongLaser()
    {
        // I can make the Boss flicker red before starting this attack as a warning
        ActivateLongLaser(); //will change where it becomes true later, this is here for testing. 
        //this will become true in a coroutine or when the Boss HP % is below a certain value
        if(_isLongLaserActive == true)
        {
         LaserVisualizer.SetActive(true);
        }

    }
    void MoveSideToSide()
    {
        Vector3 v = StartingPos;
        v.x = _distance * Mathf.Sin(Time.time * _laserAttackSpeed);
        transform.position = v;
        FireLongLaser();//here for testing
    }
    void OmniShot()
    {
      //Instantiate the projectile here. The actual object will hold the code
    }
    void ExplosiveShot()
    {
       //Only fires when health is critical.
       //Here we have to use a logic that tracks health and if it's less or equal to a certain number, the AoE is activated once.
       //use same logic as Player to set it active
       //I could also instantiate it
    }
   
    void ActivateLongLaser()
    {
        _isLongLaserActive = true;
    }
    
    //HOW THE BOSS PHASES WILL WORK:
    //Each phase will be a coroutine
    //Each coroutine will be triggered by the HP % of the enemy
    //I can do this with a switch statement that check how much HP the Boss has
    //can set the coroutine condition to true on the switch statements? or just call the void methods? Need to experiment


    //The boss will have three types of attacks: Regular Lasers(tripple Shot Style), a bomb AoE Attack, and a laser that has the boss moving side to side
    //The phases could be diffrent coroutines or I could add them all to one?
    //or maybe coroutine phase1 then phase2 and so on?
}
