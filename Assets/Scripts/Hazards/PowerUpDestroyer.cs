using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDestroyer : MonoBehaviour
{
    //Circle Cast
    float _rayDistance = 8.0f;
    [SerializeField]
    float _rayCastRad = 0.5f;

    //Power Up Destroyaer prefab
    [SerializeField]
    private GameObject _puDestroyerPrefab;

    //Attack Variables
    private Player _player;
    private float _fireRate = 0.5f;
    private float _canFire = -0.6f;

    //Effects
    AudioSource _explosionSound;
    private bool _isPowerUpInRange = false;
    [SerializeField]
    private GameObject _explosionAnim;

    //Movement
    private float _speed = 4.0f;

    //Spawner
    [SerializeField]
    private int enemyID;

    // Start is called before the first frame update
    void Start()
    {
        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
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
        calculateMovement();
        destroyPowerUp();
    }
    private void fireAtPowerUp()
    {
        _fireRate = 3f;
        _canFire = Time.time + _fireRate;
        GameObject enemeyLaser = Instantiate(_puDestroyerPrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180.0f));
    }
    private void PowerUpInRange()
    {
        _isPowerUpInRange = true;
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
    void DestroyEnemy()
    {
        _speed = 0;
        _explosionSound.Play();
        Instantiate(_explosionAnim, transform.position, Quaternion.identity);
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, .20f);

    }
    private void destroyPowerUp()
    {
        _rayCastRad = 5.0f;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _rayCastRad, Vector2.down, _rayDistance, LayerMask.GetMask("collectible"));

        Debug.DrawRay(transform.position, Vector3.down * _rayCastRad * _rayDistance, Color.red);

        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("PowerUp") && Time.time > _canFire)
            {

                Debug.Log("PowerUp Detected");

                fireAtPowerUp();
            }
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
