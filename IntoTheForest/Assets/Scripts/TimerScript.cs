using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining = 10.0f;
    public float halfRemaining = 5f;

    public float fadeTime;


    public Light lightToDim;
    public float dimValue = 0.1f;

    public GameObject player;

    public LevelChangerScript leverChangerScript;

    public AudioMixer mixer;

    public AudioSource AudioSource_ominous;
    public AudioSource AudioSource_ambient;
    public AudioSource AudioSource_birds;
    public AudioSource AudioSource_crows;
    public AudioSource AudioSource_heart;

    bool fadeInit = false;

    public float _skyboxBlendFactor = 0f;

    void Start()
    {
        //AudioSource_ominous.PlayDelayed(halfRemaining - fadeTime / 2);
        //AudioSource_crows.PlayDelayed(halfRemaining - fadeTime / 2);
        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);
    }

    void Update()
    {
        // Countdown time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (_skyboxBlendFactor < 1) 
            {
                _skyboxBlendFactor += 0.01f * Time.deltaTime;
                RenderSettings.skybox.SetFloat("_Blend", _skyboxBlendFactor);
            }

            // Dim light
            if (lightToDim.intensity > 0.1)
            {
                lightToDim.intensity -= dimValue * Time.deltaTime;
            }

            if (lightToDim.color.g > 0.2)
            {
                lightToDim.color -= (Color.green / (halfRemaining * 10.0f)) * Time.deltaTime;
            }
            if (lightToDim.color.r > 0.3)
            {
                lightToDim.color -= (Color.red / (halfRemaining * 10.0f)) * Time.deltaTime;
            }
            if (lightToDim.color.b > 0.4)
            {
                lightToDim.color -= (Color.blue / (halfRemaining * 10.0f)) * Time.deltaTime;
            }

            if (player.transform.position.x > 100 && player.transform.transform.position.z > 100)
            {
                leverChangerScript.FadeToLevel("WinGame");
                // SceneManager.LoadScene("WinGame");
            }

            if (fadeInit != true && timeRemaining < halfRemaining + fadeTime / 2)
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
