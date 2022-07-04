using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    //Attack Variables
    [SerializeField]
    private GameObject _laserPrefab;
    private Player _player;
    private float _fireRate = 1.0f;
    private float _canfire = -0.6f;

    //Spawner
    [SerializeField]
    private int enemyID;

    //Circle Cast
    float _rayDistance = 8.0f;
    [SerializeField]
    float _rayCastRad = 0.5f;

    //Regular Movement
    private float _speed = 3.0f;

    //Effects 
    AudioSource _explosionSound;
    [SerializeField]
    private GameObject _explosionAnim;

    // Start is called before the first frame update
    void Start()
    {
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
        backAttack();
        calculateMovement();
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
    private void backAttack()
    {

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _rayCastRad, Vector2.up, _rayDistance, LayerMask.GetMask("Player"));

        Debug.DrawRay(transform.position, Vector3.down * _rayCastRad * _rayDistance, Color.red);

        if (hit.collider != null)
        {
            Debug.Log("The Collider isn't null for the back Attack!");
            if (hit.collider.CompareTag("Player") && Time.time > _canfire)
            {
                Debug.Log("Player Detected");
                fireLaserBack();

            }
        }
    }
    void DestroyEnemy()
    {
        _speed = 0;
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
