using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float _speed = 4;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    private Player _player;
    private Animator _animator;
    private float _fireRate = 1.0f;
    private float _canfire = -0.6f;

    AudioSource _explosionSound;
    AudioSource _laserSound;

    //Snield Enemy Variables
    [SerializeField]
    private SpriteRenderer _shieldRenderer;
    private int _shieldStrengh = 1;
    [SerializeField]
    private GameObject shieldVisualizer;

    //variables for randomzied enemy shield
    private bool _isShieldActive = false;
    private int _randomizeShield;

    //Spawner
    [SerializeField]
    private int enemyID;

    // Start is called before the first frame update
    void Start()
    {
        
        _randomizeShield = Random.Range(0, 8);

        if (_randomizeShield == 1)
        {
            ActivateShield();
            _shieldRenderer.color = Color.yellow; 
        }

       
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The Animator Component is NULL!");
        }

        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("The Explosion Audio Source is NULL!");
        }
        _laserSound = GetComponent<AudioSource>();

        if (_laserSound == null)
        {
            Debug.LogError("The Laser Audio Source is NULL!");
        }

    }

    // Update is called once per frame
    void Update()

    {
        calculateMovement();

        if (Time.time > _canfire)
        {
            FireLaser();

        }

    }
    private void FireLaser()
    {
        _fireRate = Random.Range(3f, 7f);
        _canfire = Time.time + _fireRate;
        GameObject enemeyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        Laser[] lasers = enemeyLaser.GetComponentsInChildren<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].AssignEnemyLaser();
        }

    }
    
    private void calculateMovement()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            float _randX = Random.Range(-9.3f, 9.3f);
            transform.position = new Vector3(_randX, 7f, 0f);

        }
    }
 
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();

                if (player != null)
                {
                    player.damagePlayer();
                }
                if (_isShieldActive == true)
                {
                    _isShieldActive = false;
                    shieldVisualizer.SetActive(false);
                    return;
                }
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;
                _explosionSound.Play();
                Destroy(this.gameObject, 1f);
            }

            if (other.gameObject.CompareTag("Laser")) 
            {
                Destroy(other.gameObject);

                if (_player != null)
                {

                    EnemyDeath();
                }

            }
            if (other.gameObject.CompareTag("AoE"))
        {
            {
                EnemyDeath();
            }
        }

        }
        public void EnemyDeath()
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                shieldVisualizer.SetActive(false);
                return;

            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _explosionSound.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
            _player.AddScore(10);
        }
        private void ActivateShield() 
        {
            _isShieldActive = true;
            shieldVisualizer.SetActive(true);

        }
        private void DeactivateShield()
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                shieldVisualizer.SetActive(false);
                return;
            }
        }

    
}


