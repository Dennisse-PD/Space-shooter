using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrustController : MonoBehaviour
{
    [SerializeField]
    private Slider _thrustGauge;

    private int _maxFuel = 100;

    // Start is called before the first frame update
    void Start()
    {
        _thrustGauge = GetComponent<Slider>();
      
    }
    public void updateThrustGauge(int currentFuel)
    {
        
        _maxFuel += currentFuel;
    }
    // Update is called once per frame
    void Update()
    {
      _thrustGauge.value = _maxFuel;
    }
}
