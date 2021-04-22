using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining = 10f;

    public Light lightToDim;
    public float dimValue;
    

    void Awake() {
        dimValue = 0.00001f;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (lightToDim.intensity > 0.1) {
                lightToDim.intensity -= dimValue;
            }
     
        }

        else
        {
            SceneManager.LoadScene("EndGame");
        }
    }
}
