using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagEnemy : MonoBehaviour
{
    [SerializeField]
    private int enemyID;
    private Player _player;

    //Zigzag varibles
     private float _frequency = 1.0f;
    private float _amplitude = 5.0f;
    private float _cycleSpeed = 1.0f;

    private Vector3 pos;
    private Vector3 xAxis;

    //Regular Movement
    private float _speed = 3.0f;

    //Laser
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 1.0f;
    private float _canfire = -0.6f;

    //Effects
    AudioSource _explosionSound;
    [SerializeField]
    private GameObject _explosionAnim;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        xAxis = transform.right;

        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("The Explosion Audio Source is NULL!");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ZigZagMovement();
        if (Time.time > _canfire)
        {
            FireLaser();

        }
    }
    void ZigZagMovement()
    {
        pos += Vector3.down * Time.deltaTime * _cycleSpeed;
        transform.position = pos + xAxis * Mathf.Sin(Time.time * _frequency) * _amplitude;
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
    void DestroyEnemy()
    {
        _speed = 0;
        _cycleSpeed = 0;
        _explosionSound.Play();
        Instantiate(_explosionAnim, transform.position, Quaternion.identity);
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, .20f);

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
            DestroyEnemy();
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {

                DestroyEnemy();
            }

        }
        }
    }
