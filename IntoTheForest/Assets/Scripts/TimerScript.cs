using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining = 10f;
    public float halfRemaining = 5f;

    public float fadeTime;


    public Light lightToDim;
    public float dimValue = 0.0005f;

    public GameObject player;

    public LevelChangerScript leverChangerScript;

    public AudioSource AudioSource;
    public AudioMixer AudioMixer;

    bool fadeInit = false;

    public float step = 0;

    void Start()
    {
        AudioSource.PlayDelayed(halfRemaining);
    }

    void Update()
    {
        // Countdown time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Dim light
            if (lightToDim.intensity > 0.1)
            {
                lightToDim.intensity -= dimValue / halfRemaining * Time.deltaTime;
            }

            if (lightToDim.color.g > 0.2)
            {
                lightToDim.color -= (Color.green / (halfRemaining * 2.0f)) * Time.deltaTime;
            }
            if (lightToDim.color.r > 0.3)
            {
                lightToDim.color -= (Color.red / (halfRemaining * 3.0f)) * Time.deltaTime;
            }
            if (lightToDim.color.b > 0.4)
            {
                lightToDim.color -= (Color.blue / (halfRemaining * 5.0f)) * Time.deltaTime;
            }

            if (player.transform.position.x > 10 && player.transform.transform.position.z > 10)
            {
                leverChangerScript.FadeToLevel("WinGame");
                // SceneManager.LoadScene("WinGame");
            }

            if (fadeInit != true && timeRemaining < halfRemaining - fadeTime / 2)
            {
                StartCoroutine(FadeMixerGroup.StartFade(AudioMixer, "AmbientMix", fadeTime, 0));
                StartCoroutine(FadeMixerGroup.StartFade(AudioMixer, "OminousMix", fadeTime, 1));
                fadeInit = true;
            }

        }

        else
        {
            leverChangerScript.FadeToLevel("LoseGame");
            // SceneManager.LoadScene("LoseGame");
        }
    }
}
