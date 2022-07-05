using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : MonoBehaviour
{
    //Regular Movement Speed
    private float _speed = 3.0f;

    //Ram Variables
    private Player _player;
    private float _distance;
    [SerializeField]
    private float _ramSpeed = 2.5f;
    private float _attackRange = 4.0f;
    private float _ramMultiplier = 2.0f;

    //Variables for Effects
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private bool isFlickerEnabled = false;
    AudioSource _explosionSound;
    [SerializeField]
    private GameObject _explosionAnim;

    //Spawner
    [SerializeField]
    private int enemyID;


    // Start is called before the first frame update
    void Start()
    {
        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("The Explosion Audio Source is NULL!");
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        RamPlayer();
        calculateMovement();
    }
    public void calculateMovement()
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
            if (_distance <= 1.1f)
            {

                DestroyEnemy();
            }
        }
    }
        void DestroyEnemy()
        {
        _ramSpeed = 0;
        _speed = 0;
        _explosionSound.Play();
        Instantiate(_explosionAnim, transform.position, Quaternion.identity);
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, .20f);

        }

    
    IEnumerator colorFlickerRoutine()
    {
        while (isFlickerEnabled == true)
        {

            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    void EnableFlicker()
    {
        isFlickerEnabled = true;
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
        if (other.gameObject.CompareTag("AoE"))
        {

            DestroyEnemy();


        }

    }

}
   

 
