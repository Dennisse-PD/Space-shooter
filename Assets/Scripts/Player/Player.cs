using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _speed = 3.5f;
    float _speedMultiplier = 3;
    float _thrustForce = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _nextShot = 0.15f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _shieldStrengh = 3;

    private SpawnManager _spawnManager;
    private UIManager _UIManager;
    private Camera_shake _cameraShake;


    private bool _isTripleShotEnabled = false;
    private bool _isSpeedBoostEnabled = false;
    private bool _isShieldsEnabled = false;


    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject shieldVisualizer;
    

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    public int addScore;

    [SerializeField]
    private AudioClip _laserClip;

    private AudioSource _audioSource;

    [SerializeField]
    AudioClip _explosionSound;


    [SerializeField]
    private SpriteRenderer _shieldRender;


    //THRUST
    [SerializeField]
    private Slider _thrustGauge;

    private float _totalFuel = 100;
    [SerializeField]
    private float _canThrust = 0.15f;
   
    private float _thurstDelay = 1f;

    private bool _isThrusting = false;

    //AMMO COUNT VARIABLES
    [SerializeField]
    public int _maxAmmo = 15;
    [SerializeField]
    AudioClip _noAmmoSound;

    //SHAKE VARIABLES 
    private float shakeAmmount;

    //OMNI-SHOT VARIABLES

    [SerializeField]
    LayerMask AoE;
    private bool _isShockWaveEnabled = false;
    [SerializeField]
    private GameObject shockWaveVisulizer;

    //HAZARD VARIABLES
    private bool _isHazardEnabled = false;

    //Homing Projectile Variables
    public bool _isHomingProjectileEnabled = false;
    [SerializeField]
    private GameObject _homingProjectile;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<Camera_shake>();
        _thrustGauge = GameObject.Find("Thruster_Slider").GetComponent<Slider>();
        _audioSource = GetComponent<AudioSource>();


        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL!");
        }
        if (_UIManager == null)
        {
            Debug.LogError("The UI manager is NULL!");
        }
        if (_thrustGauge == null)
        {
            Debug.LogError("The Thrust Controller Component is NULL!");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource in the player NULL!");
        }
     
        else
        {
            _audioSource.clip = _laserClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        _thrustGauge.value = _totalFuel;

       
      
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextShot)
        {
            
            if (_maxAmmo == 0)
            {
                AudioSource.PlayClipAtPoint(_noAmmoSound, transform.position);
                return;
            }
            else
            {
                FireLaser();
            }   

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            //Debug.Log("The C Key is working");
           // place attraction or movetowards here
        }

        //THURSTING  INPUT LOGIC
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _canThrust)
        {
            Debug.Log("shift is being pressed");
            isThrusting();
            _canThrust = Time.time + _thurstDelay;
          

        }
        //REGENERATE

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
             stopThrusting();
        }

        thrust();

        regenFuel();
        

    }
    void regenFuel()
    {
        if (_isThrusting == false)
        {
           
            updateThrustGauge(+1 * Time.deltaTime * 50);
            //_speed = 4;
        }

    }
    void thrust()
    {
        if (_isThrusting == true)
        {
            updateThrustGauge(-2 * Time.deltaTime * 50);
           // _speed = 8;
        }
        
    }
    void calculateMovement()

    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);

        transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    public void FireLaser()
    {
        //CALLING AMMO COUNT
        
        _nextShot = Time.time + _fireRate;
        Vector3 _laserOffset = new Vector3(0, 1.04f, 0);

       if (_isTripleShotEnabled == true)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
            _audioSource.Play();
        }
        else if (_isShockWaveEnabled == true)
        {
            StartCoroutine(ShockwaveRoutine());
        }
       else if (_isHomingProjectileEnabled == true)
        {
            Instantiate(_homingProjectile, transform.position, Quaternion.identity);
            StartCoroutine(HomingProjectilePowerDownRoutine());
            //Play audio source?
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
            _audioSource.Play();
        }
        AmmoCount(-1);
    }
   
    public void damagePlayer()
    {
        if (_isShieldsEnabled == true && _shieldStrengh >= 1)
        {
            _shieldStrengh--;

            switch (_shieldStrengh)
            {
                case 0:
                Debug.Log("The shield power is at 0");

                    Debug.Log("The shield power is at 0");
                    _isShieldsEnabled = false;
                    shieldVisualizer.SetActive(false);
                   
                    break;
                case 1:
                    Debug.Log("The shield power is at 1");
                     _shieldRender.color = Color.red;

                    break;
                case 2:
                    Debug.Log("The shield power is at 2");
                    _shieldRender.color = Color.yellow;
                    break;
            }
            return;
        }
        _shieldStrengh = 3;
        _lives--;
        _cameraShake.startShaking();

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
            AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
            AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        }
        _UIManager.updateLives(_lives);


        if (_lives < 1)
        {

            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }
    public void TripleShotEnabled()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostEnabled()
    {
        _isSpeedBoostEnabled = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldsEnabled()
    {

        _isShieldsEnabled = true;
        shieldVisualizer.SetActive(true);

    }
    public void AddScore(int points)
    {
        addScore += points;
        _UIManager.updateScore(addScore);
    }
    //AMMO COUNT METHOD
    public void AmmoCount(int bullets)
    {
        
        if( bullets >= _maxAmmo)
        {
            _maxAmmo = 15;
           
        }
        else
        {
            _maxAmmo += bullets;
            
        }
        _UIManager.updateAmmoCount(_maxAmmo);

    }
    public void restoreHealth()
    {
        _lives = 3;
        _UIManager.updateLives(_lives);
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);

    }
    public void restoreLives(int healingPoints)
    {
        
        if(healingPoints >= _lives)
        {
            _lives = 3;
            
        }
        else
        {
            _lives += healingPoints;
        }
        
        _UIManager.updateLives(_lives);
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);
    }
    //THRUST BOOL METHODS
   private void isThrusting()
    {
        _isThrusting = true;
        _speed = 8;
    }
    private void stopThrusting()
    {
        _isThrusting = false;
        _speed = 4;
        
    }
   
    //THRUST METHOD
    public void updateThrustGauge(float currentFuel)
    {
        
        if (_totalFuel - currentFuel < 1 )
        {
            Debug.Log("Not Enough Fuel " + _speed);
            stopThrusting();
            _speed = 4;
            
            //stopThrusting();
           
        }
        
        _totalFuel += currentFuel;

    }
    //replenish ammo
    public void replenishAmmo()
    {     
      AmmoCount(15);
 
    }
    
    

    //AOE LOGIC PHYSICS.SPHEREOVERLAP
    private void AreaOfEffectDamage()
    {
        
        Vector2 origin = new Vector2(0f,0f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, 20f, AoE);
         foreach(Collider2D c in colliders)
        {
            if(c.GetComponent<Enemy>())
            {
                //get boss component and damage boss
                BossFight bossFight = GetComponent<BossFight>();
                if (bossFight != null)
                {
                    bossFight.DamageBoss(10);
                }
                c.GetComponent<Enemy>().EnemyDeath();
            }
        }
    }


    public void ShockWaveEnabled()
    {
        _isShockWaveEnabled = true; 
    }

    public void HazardEnabled()
    {
        _isHazardEnabled = true;
        _speed /= _speed;
        StartCoroutine(HazardPowerDownRoutine());
    }

    public void HomingProjectileEnabled()
    {
        _isHomingProjectileEnabled = true;
        //StartCoroutine(HomingProjectilePowerDownRoutine());
    }

    //MIGHT DELETE LATER. Here to make it a one time shot power up only
    public void HomingProjectileDisabled()
    {
        _isHomingProjectileEnabled = false;
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotEnabled = false;


    }


    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostEnabled = false;
        _speed /= _speedMultiplier;


    }
    IEnumerator HazardPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isHazardEnabled = false;
        _speed = 3.5f;
    }
    IEnumerator ShockwaveRoutine()
    {  
        shockWaveVisulizer.SetActive(true); 
        yield return new WaitForSeconds(0.5f);
        AreaOfEffectDamage();
        yield return new WaitForSeconds(1.0f);
        shockWaveVisulizer.SetActive(false);
        _isShockWaveEnabled = false;
    }
     IEnumerator HomingProjectilePowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isHomingProjectileEnabled = false;


    }

}


