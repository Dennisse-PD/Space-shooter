using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private GameObject _explosion;

    private SpawnManager _spawnManager;

     
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(Vector3.forward* _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Laser"))
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(GetComponent<Collider2D>());
            Destroy(other.gameObject);
            _spawnManager.StartCoroutine("StartSpawnRoutines");
            Destroy(this.gameObject,.50f);

        }
        

    }
   
}
