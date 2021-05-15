using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float _speed = 4;

    [SerializeField]
    private GameObject _enemyPrefab;

    private Player _player;
    private Animator _animator;

    [SerializeField]
    AudioClip _explosionSound;


    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * (_speed* Time.deltaTime);

        if(transform.position.y <=-5f)
        {
            float _randX = Random.Range(-9.3f, 9.3f);
            transform.position = new Vector3(_randX, 7f, 0f);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if(player != null)
            {
                player.damagePlayer();
            }
            
            _animator.SetTrigger("OnEnemyDeath");
           AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
            _speed = 0;
            Destroy(this.gameObject, 2.6f);
        }

      if(other.gameObject.CompareTag("Laser"))
        {

            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10); 
            }
            _animator.SetTrigger("OnEnemyDeath");
            AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
            _speed = 0;
            Destroy(this.gameObject, 2.6f);
        }

    }
}
