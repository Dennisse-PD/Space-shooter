using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    //HP Bar Variables
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //Boss entrance
    private Vector3 endPosition = new Vector3(0, 3.95f, 0);


    //Prefab
    [SerializeField]
    private GameObject _laserPrefab;
    private float _speed = 2;

    // Side to Side Movement Variables
    private float _distance = 5f;
    private float _laserAttackSpeed = 1f;
    private bool _isLongLaserActive = false;

    //Long Laser Variables
    [SerializeField]
    private GameObject LaserVisualizer;

    //Damage Effects
    [SerializeField]
    private GameObject DamageLeftVisualizer;
    [SerializeField]
    private GameObject DamageRightVisualizer;
    [SerializeField]
    private GameObject _explosionAnim;
    private PowerUpSpawner _PowerUpSpawner;
    private Canvas _canvas;


    //Sound Effects
    AudioSource _explosionSound;

    //boss death
    public bool _isBossAlive = true;
    private MiscroGM _microGM;
    private UIManager _UIManager;




    void Start()
    {
        _microGM = GameObject.Find("MicroGM").GetComponent<MiscroGM>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("The Explosion Audio Source is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        OmniShot omniShot = GetComponent<OmniShot>();
        if (omniShot == null)
        {
            Debug.Log("The OmniShot Script is NULL!");
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

            DamageLeftVisualizer.SetActive(true);

        }
        if (currentHealth <= 30)
        {
            LongLaserAttack();
            DamageRightVisualizer.SetActive(true);

        }
        if (currentHealth < 1)
        {
            DestroyBoss();
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            DamageBoss(10);
        }
        else if (other.tag == "AoE")
        {
            DamageBoss(15);
        }

    }
    void UserInputOptions()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isBossAlive == true)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void DestroyBoss()
    {
        
        _speed = 0;
        _explosionSound.Play();
        _UIManager.GameWonSequence();
        _microGM.GameOver();
        Instantiate(_explosionAnim, transform.position, Quaternion.identity);
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject, .20f);
      
    }

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
    public void BossIsDead()
    {
        _isBossAlive = false;
    }


}