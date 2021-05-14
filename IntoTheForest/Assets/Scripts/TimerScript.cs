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

    public AudioSource AudioSource_ominous;
    public AudioSource AudioSource_ambient;
    public AudioSource AudioSource_birds;
    public AudioSource AudioSource_crows;
    public AudioSource AudioSource_heart;

    bool fadeInit = false;

    void Start()
    {
        //AudioSource_ominous.PlayDelayed(halfRemaining - fadeTime / 2);
        //AudioSource_crows.PlayDelayed(halfRemaining - fadeTime / 2);
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
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ambient, fadeTime, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, fadeTime, 1));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_birds, fadeTime, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, fadeTime, 0.25f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, timeRemaining-5, 1));
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
