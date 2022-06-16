﻿using System.Collections;
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

    [SerializeField]
    private float _magnetspeed = 5.0f;

    private GameObject Player;

    [SerializeField]
    private GameObject _explosionAnim;

    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");

        if(Player == null)
        {
            Debug.LogError("The Player is NULL!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);

        if (transform.position.y <= -3.19)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKey(KeyCode.C))
        {
            Magnet();
            Debug.Log("The C Key is working");
            // place attraction or movetowards here
        }
    }
    private void Magnet()
    {
        transform.position = Vector3.Lerp(this.transform.position, Player.transform.position, _magnetspeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        Player player = other.transform.GetComponent<Player>();
        if (player != null)
        {

            if (other.gameObject.CompareTag("Player")) //add another compare tag for the laser || other.gameObject.CompareTag("puDestroyerLaser"))
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
                    case 6:
                        player.HazardEnabled();
                        break;
                    case 7:
                        player.HomingProjectileEnabled();
                        break;
                }
                Destroy(gameObject);

            }


        }

        if (other.gameObject.CompareTag("puDestroyerLaser"))
        {
            //instantiate the explosion animation here
            Instantiate(_explosionAnim, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject, .20f);
        }
    }
}

   

