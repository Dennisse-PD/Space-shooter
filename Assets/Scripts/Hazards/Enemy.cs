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

    [SerializeField]
    private int enemyID;

    private float _frequency = 1.0f;
    private float _amplitude = 5.0f;
    private float _cycleSpeed = 1.0f;

    private Vector3 pos;
    private Vector3 xAxis;

    //Snield Enemy Variables
    [SerializeField]
    private SpriteRenderer _shieldRenderer;
    private int _shieldStrengh = 1;
    [SerializeField]
    private GameObject shieldVisualizer;

    //variables for randomzied enemy shield
    private bool _isShieldActive = false;
    private int _randomizeShield;

    //Aggressive Enemy Variables

    private float _distance;
    [SerializeField]
    private float _ramSpeed = 2.5f;
    private float _attackRange = 4.0f;
    private float _ramMultiplier = 2.0f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private bool isFlickerEnabled = false;

    //CircleCast Variables
    float _rayDistance = 8.0f;
    [SerializeField]
    float _rayCastRad = 0.5f;

    //Power Up Destroyaer prefab
    [SerializeField]
    private GameObject _puDestroyerPrefab;

    private float _altFireRate = 3.0f;
    private float _canFireAlt = -1f;

    //Enemy Pick up
    private bool isPowerUpInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        //randomizez shiled here 0-5
        _randomizeShield = Random.Range(0, 10);

        if (_randomizeShield == 1)
        {
            ActivateShield();
            _shieldRenderer.color = Color.yellow; //set this only when shield is active
        }

        //Variables for Sine Wave Zig-Zag move 
        pos = transform.position;
        xAxis = transform.right;

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

        //NEW ENEMY MOVEMENT LOGIC 
        //make a case to swtich between enemy movements depending on which spawned
        //I can use a bool that becomes true when they spawn and this will help me activate the case statments 
        //***See Modular power-up script for references***

        calculateMovement();
        switch (enemyID)
        {
            case 0:
                //SidetoSideMovement(); or can add code for them to appear from the sides here

                break;
            case 1:
              //zigzgag enemy?

                break;
            case 2:
                RamPlayer();
                break;
            case 3:
                Dodge();
                break;
            case 4:
                backAttack();
                break;
            case 5:
                destroyPowerUp();
                break;
        }
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
    //The following firing methods may be changed into >>>>> Coroutines <<<<<<
    private void fireLaserBack()
    {
        _fireRate = 1f;
        _canfire = Time.time + _fireRate;
        GameObject enemeyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180.0f));
        Laser[] lasers = enemeyLaser.GetComponentsInChildren<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].AssignEnemyLaser();
        }

    }
    private void fireAtPowerUp()
     {
          _altFireRate= 3f;
        _canFireAlt = Time.time + _altFireRate;
         GameObject enemeyLaser = Instantiate(_puDestroyerPrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180.0f));
    }

    void calculateMovement()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            float _randX = Random.Range(-9.3f, 9.3f);
            transform.position = new Vector3(_randX, 7f, 0f);

        }
    }

   
    private void RamPlayer()
    {
        StartCoroutine(colorFlickerRoutine());
        if (_player != null)
        {
            _distance = Vector3.Distance(_player.transform.position, this.transform.position);

            if (_distance <= _attackRange)
            {
                EnableFlicker();
                Vector3 direction = this.transform.position - _player.transform.position;
                direction = direction.normalized;
                this.transform.position -= direction * Time.deltaTime * (_ramSpeed * _ramMultiplier);
            }
            if (_distance <= 1.1f) //is this what destroyes the enemy when they are out of bounds? If so,I might need to turn this into a function to call for all enemies
            {

                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject);
            }
        }



    }
    private void Dodge()//ENEMY AVOID LASER. MUST REVISE LATER
    {
        //make another dodge method instead using colliders and if the right tag(s) hit, I will do the dodge thing. 
        //I might need to do this from a new script that inherits the Enemy class so I can call this dodge method wpthin it.
        float x = Random.Range(-5.0f, 5.0f);
        float y = Random.Range(-5.0f, 5.0f);
        //raycast hit code here
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _rayCastRad, Vector2.down, _rayDistance); //should add layer mask for laser later<<<IMPORTANT>>>
        // CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance = Mathf.Infinity);
        Debug.DrawRay(transform.position, Vector3.down * _rayCastRad * _rayDistance, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Laser"))
            {
                //dodge
                Debug.Log("Laser Detected");
                transform.position = new Vector2(x, y);
            }
        }

    }
    private void backAttack()
    {

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _rayCastRad, Vector2.up, _rayDistance, LayerMask.GetMask("Player"));

        Debug.DrawRay(transform.position, Vector3.down * _rayCastRad * _rayDistance, Color.red);

        if (hit.collider != null)
        {
            Debug.Log("The Collider isn't null for the back Attack!");
            if (hit.collider.CompareTag("Player")  && Time.time > _canfire)
            {
                Debug.Log("Player Detected");
                fireLaserBack();

            }
        }
    }
    private void destroyPowerUp()
    {
        _rayCastRad = 5.0f;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _rayCastRad, Vector2.down, _rayDistance, LayerMask.GetMask("collectible"));

        Debug.DrawRay(transform.position, Vector3.down * _rayCastRad * _rayDistance, Color.red);

        if (hit.collider != null)
        {
           
            if (hit.collider.CompareTag("PowerUp") && Time.time > _canFireAlt)
            {

                Debug.Log("PowerUp Detected");
               
                 fireAtPowerUp();
            }
        }
    }

        //SMART ENEMY CODE HERE USING THE SAME LOGIC AS ABOVE BUT THE VECTOR IS FORWARD AND THE LASER GETS CALLED


        IEnumerator colorFlickerRoutine()
        {
            while (isFlickerEnabled == true)
            {

                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(0.5f);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.5f);
                isFlickerEnabled = false;
            }
        }

        private void PowerUpInRange()
        {
            isPowerUpInRange = true;
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
                    shieldVisualizer.SetActive(false);//deatiaves shield after one hit so i don't need the strent variable
                    return;
                }
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;
                _explosionSound.Play();
                Destroy(this.gameObject, 2.6f);
            }

            if (other.gameObject.CompareTag("Laser"))
            {
                Destroy(other.gameObject);

                if (_player != null)
                {

                    EnemyDeath();
                }

            }

        }
        public void EnemyDeath() //MAKE AN IF SOMWHERE TO MANAGE THE DEATH TIMING FOR THE OTHER ENEMYES just needs to be an else for all the others
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                shieldVisualizer.SetActive(false);//deatiaves shield after one hit so i don't need the strent variable
                return;

            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _cycleSpeed = 0f;
            _explosionSound.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
            _player.AddScore(10);
        }
        private void ActivateShield() //called on case 3
        {
            _isShieldActive = true;
            shieldVisualizer.SetActive(true);

        }
        private void DeactivateShield()
        {
            if (_isShieldActive == true)
            {
                _isShieldActive = false;
                shieldVisualizer.SetActive(false);//deatiaves shield after one hit so i don't need the strent variable
                return;
            }
        }
        void EnableFlicker()
        {
            isFlickerEnabled = true;

        }


    
}


