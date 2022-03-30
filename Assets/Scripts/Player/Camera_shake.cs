using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_shake : MonoBehaviour
{

    private bool _isShaking = false; //THIS COULD BE THE PROBLEM WITH THE SHAKE NOT GOING BACK DEFAULT POSITION. THIS IS NEVER USED.
    private float duration = 0.5f;
    private float magnitude = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CameraShakeRoutine()
    {
        Vector3 defaultPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)  
        {
            float xPosition = Random.Range(-1f, 1f) * magnitude;
            float yPosition = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(xPosition, yPosition, -10f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = defaultPosition;
    }

public void startShaking()
    {
    
        StartCoroutine(CameraShakeRoutine());
    }
}
