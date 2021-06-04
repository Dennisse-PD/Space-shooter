using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    /* To-Do List:
     * Find a way to make the AoE ShockWave Effect. I should Already know how to trigger the animation.
     * But if I go with enable, how should I handle collisions?
     * Program the collision behavior: Should the player toss a "bomb" then that detonates and the showaves comes out?
     * Make the coroutine so that it lasts only 5 second or so, and make it spawn rarely
     * Call the method from the Power-Up Script. Look at other power-ups for reference
     * 
     * POSSIBLE SOLUTIONS
     * Use Spehre Overlap to detect collisions within a sphere's radius
     * Use a shader or graphical effect for the shockwave then made the sphere^ the same size as it
     * Make the shader into a child of whatever I use to detect the collision(probably the bomb object thing) so that it always spawns from it
     * -------------------------------------------------------------------------------
     * TO-DO 6/3/2021
     * [x]Create Shockwave animation 
     * []Create a Coroutine or way to delay the explosion before the animation
     * 
     * HINT: Look at the Tripple-Shot Corotine and the shield power-up.(Shield is one-time, Tripple Shot is a weapon)
     * start the corountine within the isOmniShot Enabled?
     * Do I need the coroutine or just to delay the enemy destruction..I think that's it.
     * DEfinelt will trigger the animation within a corutoine and then after 3 seconds set it to false and call the delay from tehre too
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("You pressed K");
            AreaOfEffectDamage(new Vector3(1, 1, 0), 50f);
        }
        
    }
    void AreaOfEffectDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Found Collider");
            hitCollider.SendMessage("AddDamage");
        }
    }
}
