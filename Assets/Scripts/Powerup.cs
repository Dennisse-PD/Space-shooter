using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private Player _player;
    private int _tripleshotPowerUp = 0;
    private int _speedPowerUp = 1;
    private int _shieldsPowerUp = 2;
    [SerializeField]
    private int powerUpID;

    // Start is called before the first frame update
    void Start()
    {


    }

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
                    }
                }
            }
            Destroy(gameObject);
        }
    }

   

