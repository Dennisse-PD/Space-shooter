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
    private float _fireRate = 3.0f;
    private float _canfire = -1;
    
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
    



    // Start is called before the first frame update
    void Start()
    {
        _shieldRenderer.color = Color.red;
        pos = transform.position;
        xAxis = transform.right;

        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }

        _animator = GetComponent<Animator>();
       if(_animator == null)
        {
            Debug.LogError("The Animator Component is NULL!");
        }

        _explosionSound = GetComponent<AudioSource>();
       if(_explosionSound == null)
        {
            Debug.LogError("The Explosion Audio Source is NULL!");
        }

        _laserSound = GetComponent<AudioSource>();
        if(_laserSound == null)
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
                //SidetoSideMovement();
                Debug.Log("Regular Enemy Spawned");
                break;
            case 1:
                ZigzagMovement();
                Debug.Log("Alternate Enemy Spawned");
                break;
        }

        if (Time.time > _canfire)
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

    void ZigzagMovement()
    {
        pos += Vector3.down * Time.deltaTime * _cycleSpeed;
        transform.position = pos + xAxis * Mathf.Sin(Time.time * _frequency) * _amplitude;
    }
    void SidetoSideMovement()
    {
       
        transform.position = pos + xAxis * Mathf.Sin(Time.time * _frequency) * _amplitude;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        _shieldStrengh--;
        shieldVisualizer.SetActive(false);

        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if(player != null)
            {
                player.damagePlayer();
            }
            
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _explosionSound.Play();
            Destroy(this.gameObject, 2.6f);
        }

      if(other.gameObject.CompareTag("Laser"))
        {


            Destroy(other.gameObject);

            if(_player != null)
            {
                
                EnemyDeath();
            }
            
        }
      
    }
    public void EnemyDeath()
    {
       if(_shieldStrengh == 0 )
        {
            return;
        }

        _animator.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _explosionSound.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.6f);
        _player.AddScore(10);
    }
   /* private void DeactivateShield()
    {
        if (forceFieldStrengh >= 1)
        {
            shieldVisualizer.SetActive(false);
        }

    }*/
}
