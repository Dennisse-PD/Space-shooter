using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : MonoBehaviour
{
    [SerializeField]
    Transform Player;
    private float _distance;
    [SerializeField]
    private float _ramSpeed = 3.0f;
    private float _attackRange = 6.63f;
    private float _ramMultiplier = 2.0f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    //Smart enemy
    private bool isFlickerEnabled = false;
    private bool isDodgeEnabled = false;
    [SerializeField]
    Transform lasers;
    private float _laserDistance;
    [SerializeField]
    private GameObject EnemyVisualizer;


    // Start is called before the first frame update
    void Start()
    {
        EnableDodge();

        EnableFlicker();

        


    }

    // Update is called once per frame
    void Update()
    {
        RamPlayer();



    }
    private void RamPlayer()
    {
        transform.position += Vector3.down * (_ramMultiplier * Time.deltaTime);
        _distance = Vector3.Distance(Player.position, this.transform.position);
        Debug.Log("The distance between the Player and the Enemy is  " + _distance);
        if (_distance <= _attackRange)
        {
            StartCoroutine(colorFlickerRoutine());
            Vector3 direction = this.transform.position - Player.transform.position;
            direction = direction.normalized;
            this.transform.position -= direction * Time.deltaTime * (_ramSpeed * _ramMultiplier);

        }
        if (_distance <= 1.1f)
        {

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }

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
    IEnumerator DodgeRoutine()
    {
        while (isDodgeEnabled == true)
        {
            
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(1.0f);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Debug.Log("Trigger is working");
            StartCoroutine(DodgeRoutine());

        }

    }
    void EnableFlicker()
    {
        isFlickerEnabled = true;
    }
    void EnableDodge()
    {
        isDodgeEnabled = true;
    }
}
   

 
