using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }
    void MoveDown()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }

    //Used to test the damage, I need to switch this to damage the power up instead
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "PowerUp")
        {
            
            Destroy(other.gameObject);
            
        }
    }
   

}
