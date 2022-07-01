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
    //private Vector3 StartingPos; just used transform.position
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

    //Sound Effects
    [SerializeField]
    AudioClip _explosionSound;


    void Start()
    {
       
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        OmniShot omniShot = GetComponent<OmniShot>();
        if (omniShot == null)
        {
            Debug.Log("The OmniShot Script is NULL!");//just testing, don't need to log error atm
        }

        //Boss entrance
        if (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, _speed * Time.deltaTime);
          

        }
        BossPhases();
    }
    public void DamageBoss(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    private void BossPhases()
    {
        if (currentHealth == 50)
        {
            // AudioSource.PlayClipAtPoint(_explosionSound, transform.position)
            DamageLeftVisualizer.SetActive(true);

        }
        if (currentHealth <= 30)
        {
            LongLaserAttack();
            Debug.Log("This is Phase 2");
            DamageRightVisualizer.SetActive(true);
            
        }
        if(currentHealth < 1)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            DamageBoss(10);
        }
        else if(other.tag == "AoE")
        {
            DamageBoss(15);
        }

    }
   /* void CheckPhases()
    {
        if (currentHealth > 50)
        {
            Debug.Log("This is phase 1");
            _isPhase1Started = true;

        }
        if (currentHealth <= 50)
        {
            Debug.Log("This is Phase 2");
            DamageLeftVisualizer.SetActive(true);
            _isPhase1Started = false;
            _isPhase2Started = true;

        }
        if (currentHealth <= 30)
        {
            LongLaserAttack();
            Debug.Log("This is the Final Phase");
            DamageRightVisualizer.SetActive(true);
        }
         if (currentHealth < 1)
        {
           // AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
            Destroy(this.gameObject);
        }
    }*/
   
    void LongLaserAttack()
    {
        Vector3 v = transform.position;
        v.x = _distance * Mathf.Sin(Time.time * _laserAttackSpeed);
        transform.position = v;
        LaserVisualizer.SetActive(true);
    }

    IEnumerator LaserPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        


    }
    

}