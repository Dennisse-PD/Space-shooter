using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{ 
    //Targetting via objects 
    private Transform Target = null;
    private GameObject[] targets;

    //Movemenet physics 
    [SerializeField]
    private Rigidbody2D HomingProjectileRB;
    private float _distance;
    private float _closestTarget = Mathf.Infinity;
    private float _speed = 300f;
    private float _rotationSpeed = 900f;

    // Start is called before the first frame update
    void Start()
    {
        //Getting the rigidbody to use physics to move the projectile
        HomingProjectileRB = GetComponent<Rigidbody2D>();
        
        if(HomingProjectileRB == null)
        {
            Debug.LogError("The Rigidbody is NULL!");

        }
        FindClosestEnemy();

    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        fireProjectile();
    }

    private void FindClosestEnemy()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(var enemy in targets)
        {
            _distance = (enemy.transform.position - this.transform.position).sqrMagnitude;
            if(_distance < _closestTarget)
            {
                _closestTarget = _distance;
                Target = enemy.transform;

            }
        }     
    }
    private void fireProjectile()
    {
        HomingProjectileRB.velocity = transform.up * _speed * Time.deltaTime;
        if(Target != null)
        {
            Vector2 direction = (Vector2)Target.position - HomingProjectileRB.position;
            direction.Normalize();
            float rotationValue = Vector3.Cross(direction, transform.up).z;
            HomingProjectileRB.angularVelocity = -rotationValue * _rotationSpeed;
            HomingProjectileRB.velocity = transform.up * _speed * Time.deltaTime;
        }
        ProjectileDeadzone();

    }
    private void ProjectileDeadzone()
    {
        if (transform.position.y >= 8)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x > 11.3f)
        {
            Destroy(this.gameObject);
        }
         if (transform.position.x < -11.3f)
        {
            Destroy(this.gameObject);
        }

    }


}
