using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //must make sure I remove unnessary searialized fields
    [SerializeField]
    private float _speed = 3.0f;
    private Player _player;
  
    [SerializeField]
    private int powerUpID;

    [SerializeField]
    AudioClip _PowerUpSound;

  
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);

        if (transform.position.y <= -3.19)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (player != null)
        {
            
            if (other.gameObject.CompareTag("Player"))
            {

                AudioSource.PlayClipAtPoint(_PowerUpSound, transform.position);
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotEnabled();
                        break;
                    case 1:
                        player.SpeedBoostEnabled();
                        break;
                    case 2:
                        player.ShieldsEnabled();
                        break;
                    case 3:
                        player.replenishAmmo();
                        break;
                    case 4:
                        player.restoreLives(3);
                            break;
                    case 5:
                        player.ShockWaveEnabled();
                        break;
                }
                Destroy(gameObject);
            }
        }

    }
}

   

