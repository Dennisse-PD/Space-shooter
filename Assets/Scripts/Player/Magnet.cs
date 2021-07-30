using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private float magnetSPeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("I found the player collider but that's the problem...");
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, magnetSPeed * Time.deltaTime);
        }
       
    }
}
