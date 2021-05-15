using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    AudioClip _explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);

        Destroy(this.gameObject, 3.0f);
    }
}
