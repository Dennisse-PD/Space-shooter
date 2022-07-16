using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniShotMovement : MonoBehaviour
{
    private Vector2 _movementDirection;
    private float _movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _movementSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_movementDirection * _movementSpeed * Time.deltaTime);
    }
    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    
    public void SetMovementDirection(Vector2 dir)
    {
        _movementDirection = dir;
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.damagePlayer();
                Destroy();
            }


        }
    }
}
