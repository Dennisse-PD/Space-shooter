using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    private float _speed = 2;
    private Vector3 endPosition = new Vector3(0, 4.76f, 0);
    private float _fireRate = 0.5f;
    private float _canfire = -0.2f;
    [SerializeField]
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, _speed * Time.deltaTime);
            //start boss phases here
            
        }

    }
    void FireLaser()
    {
        //long laser moving side to side
        //Activate the object or instantiate it
        
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


   //The boss will have three types of attacks: Regular Lasers(tripple Shot Style), a bomb AoE Attack, and a laser that has the boss moving side to side
   //The phases could be diffrent coroutines or I could add them all to one?
   //or maybe coroutine phase1 then phase2 and so on?
}
