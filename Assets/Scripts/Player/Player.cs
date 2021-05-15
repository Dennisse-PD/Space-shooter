using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _speed = 3.5f;
    float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _nextShot = 0.5f;
    [SerializeField]
    private int _lives = 3;

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





    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();
      

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL!");
        }
        if (_UIManager == null)
        {
            Debug.LogError("The UI manager is NULL!");
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


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextShot)
        {
            FireLaser();
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
    void FireLaser()
    {
        _nextShot = Time.time + _fireRate;
        Vector3 _laserOffset = new Vector3(0, 1.04f, 0);
        
        if(_isTripleShotEnabled == true)
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
        if(_isShieldsEnabled == true)
        {
          
            _isShieldsEnabled = false;
            shieldVisualizer.SetActive(false);
            return;
           
        }
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
       

        if(_lives < 1)
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
    public void AddScore(int points)
    {
        addScore += points;
        _UIManager.updateScore(addScore);
    }

}
  

