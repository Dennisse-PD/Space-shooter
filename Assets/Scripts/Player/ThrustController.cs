using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrustController : MonoBehaviour
{
    [SerializeField]
    private Slider _thrustGauge;

    private int _totalFuel = 100;
    //private int _currentFuel;
   

    // Start is called before the first frame update
    void Start()
    {
        _thrustGauge = GetComponent<Slider>();

    }
    public void updateThrustGauge(int currentFuel)
    {

        _totalFuel += currentFuel;

        if (_totalFuel - currentFuel < 0)
        {  
            Debug.Log("Not Enough Fuel");
        }

    }

    // Update is called once per frame
    void Update()
    { 
        _thrustGauge.value = _totalFuel;
    }

}
