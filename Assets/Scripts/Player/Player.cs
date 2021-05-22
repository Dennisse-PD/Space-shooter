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

    //THRUST
    [SerializeField]
    private Slider _thrustGauge;

    [SerializeField]
    private SpriteRenderer _shieldRender;

    private float _totalFuel = 100;
    [SerializeField]
    private float _canThrust = 0.15f;
   
    private float _thurstDelay = 0.5f;

    private bool _isThrusting = false;



    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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
        _thrustGauge.value = _totalFuel;// Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextShot)
        {
            FireLaser();
        }

        //THURSTING LOGIC
        if (Input.GetKey(KeyCode.LeftShift) && Time.time > _canThrust)
        {

            isThrusting();
            _speed = 8;
            updateThrustGauge(-2);
            


        }
        //REGENERATE

       else  if( Input.GetKeyUp(KeyCode.LeftShift) && Time.time > _canThrust )
        {
            stopThrusting();
            _speed = 4;
            
            _canThrust = Time.time + _thurstDelay;
           

        }
        regenFuel();


    }
    void regenFuel()
    {
        
        if (_isThrusting == false)
        {
            updateThrustGauge(+1);
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
        _nextShot = Time.time + _fireRate;
        Vector3 _laserOffset = new Vector3(0, 1.04f, 0);

        if (_isTripleShotEnabled == true)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        _audioSource.Play();

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
    private void isThrusting()
    {
        _isThrusting = true;
    }
    private void stopThrusting()
    {
        _isThrusting = false;
    }
    //THRUST METHOD
    public void updateThrustGauge(float currentFuel)
    {
        _totalFuel += currentFuel;
        if (_totalFuel - currentFuel < 0)
        {
            _speed = 4;
            _totalFuel += currentFuel * Time.time;
            Debug.Log("Not Enough Fuel");
           
        }

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
    IEnumerator ThrustRoutine()
    {
       while(_isThrusting == true)
        {

            updateThrustGauge(-20);
            yield return new WaitForSeconds(1.0f);
        }
        
    }
    IEnumerator ThrustRegenRoutine()
    {

        while (_isThrusting == false)
        {
            updateThrustGauge(+20);
            yield return new WaitForSeconds(1.0f);

        }



    }


}


