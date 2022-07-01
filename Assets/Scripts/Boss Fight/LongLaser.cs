using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongLaser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaserPowerDownRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LaserPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.damagePlayer();
            }

        }
    }

}
