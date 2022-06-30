using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniShotMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
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
