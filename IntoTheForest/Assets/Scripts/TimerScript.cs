using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining = 10f;

    public Light lightToDim;
    public float dimValue;

    public GameObject player;

    public LevelChangerScript leverChangerScript;
    

    void Awake() {
        dimValue = 0.00005f; // Would be better to calculate this based on timeRemaining 
    }

    void Update()
    {
        // Countdown time
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;

            // Dim light
            if (lightToDim.intensity > 0.1) {
                lightToDim.intensity -= dimValue;
            }

            if (player.transform.position.x > 10 && player.transform.transform.position.z > 10) {
                leverChangerScript.FadeToLevel("WinGame");
                // SceneManager.LoadScene("WinGame");
            }
     
        }

        else {
            leverChangerScript.FadeToLevel("LoseGame");
            // SceneManager.LoadScene("LoseGame");
        }
    }
}
