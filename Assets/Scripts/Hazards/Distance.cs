using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField]
    Transform Player;
    private float _distance;
    [SerializeField]
    private float _ramSpeed = 3.0f;
    private float _attackRange = 6.63f;
    private float _speed = 2.0f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);

        if (Player)
        {
            _distance = Vector3.Distance(Player.position, this.transform.position);
            Debug.Log("The distance between A and B is " + _distance);
            if (_distance <= _attackRange && _distance != 0)
            {
                Debug.Log("This if is working ");

                Vector3 direction = this.transform.position - Player.transform.position;
                direction = direction.normalized;
                this.transform.position -= direction * Time.deltaTime * (_ramSpeed * _speed);
                //transform.position = Vector3.MoveTowards(transform.position, Player.position, 1.0f);
            }
            if(_distance <= 1.1f)
            {
                Debug.Log("Distance is 1.1f");
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject);
            }
            
        }
    }
}
