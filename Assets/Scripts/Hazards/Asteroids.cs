using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion zRotation = Quaternion.Euler(0, 0, 3);
        transform.Rotate(Vector3.forward* _speed * Time.deltaTime);



    }
}
