using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    private float _speed = 2;

    //Laser fire cooldown
    private float _fireRate = 0.5f;
    private float _canfire = -0.2f;

    // Side to Side Movement Variables
    private float _distance = 5f;
    private Vector3 StartingPos;
    private float _laserAttackSpeed = 1f;// might or might not use
    private bool _isLongLaserActive = false;

    //Long Laser Variables
    [SerializeField]
    private GameObject LaserVisualizer;

    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSideToSide();
       /* if (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, _speed * Time.deltaTime);
            //start boss phases here
            
        }*/
       

    }
    void FireLongLaser()
    {
        // I can make the Boss flicker red before starting this attack as a warning
        ActivateLongLaser(); //will change where it becomes true later, this is here for testing. 
        //this will become true in a coroutine or when the Boss HP % is below a certain value
        if(_isLongLaserActive == true)
        {
         LaserVisualizer.SetActive(true);
        }

    }
    void MoveSideToSide()
    {
        Vector3 v = StartingPos;
        v.x = _distance * Mathf.Sin(Time.time * _laserAttackSpeed);
        transform.position = v;
        FireLongLaser();//here for testing
    }
    void OmniShot()
    {
        //fire lasers in multiple directions 
        //I need to figure out how to fire in diffrent directions
        //Want to fire a laser and then switch to a volley following the same logic
    }
    void AreaOfEffectAttack()
    {
       //Only fires when health is critical.
       //Here we have to use a logic that tracks health and if it's less or equal to a certain number, the AoE is activated once.
       //use same logic as Player to set it active
       //I could also instantiate it
    }
    void healthBar()
    {
        //A healthbar or some way to keep track of the enemy health. This might be a good time to checkout the alt way to make progress bars
    }
    void ActivateLongLaser()
    {
        _isLongLaserActive = true;
    }
    


    //The boss will have three types of attacks: Regular Lasers(tripple Shot Style), a bomb AoE Attack, and a laser that has the boss moving side to side
    //The phases could be diffrent coroutines or I could add them all to one?
    //or maybe coroutine phase1 then phase2 and so on?
}
