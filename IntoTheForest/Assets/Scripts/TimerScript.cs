using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.FPS.Gameplay;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining;
    public const float totalTime = 120f;
    public float halfRemaining;

    public float fadeTime;


    public Light lightToDim;
    public Color colStart = new Color(1f, 0.92f, 0.92f, 1f);
    public Color colEnd = new Color(0.55f, 0.51f, 1f, 1f);

    public GameObject player;

    public LevelChangerScript leverChangerScript;
    public PlayerCharacterController PlayerCharacterController;

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
        RenderSettings.fogDensity = 0.005f;
        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);
        lightToDim.color = colStart;
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
            lightToDim.color = Color.Lerp(colStart, colEnd, Mathf.PingPong(Time.time, totalTime) / totalTime);
            lightToDim.intensity -= 0.001f * Time.deltaTime;

            if (PlayerCharacterController.MaxSpeedOnGround > 3)
            {
                PlayerCharacterController.MaxSpeedOnGround -= 0.05f * Time.deltaTime;
                PlayerCharacterController.MaxSpeedInAir -= 0.075f * Time.deltaTime;
            }

            if (player.transform.position.x > 100 && player.transform.transform.position.z > 100)
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                StartCoroutine(winCoroutine());

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

            if (timeRemaining < halfRemaining && RenderSettings.fogDensity < 0.04f) {
                RenderSettings.fogDensity += 0.00001f; 
                //fog colour?
            }

        }

        else
        {
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0));
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

            StartCoroutine(loseCoroutine());
            // leverChangerScript.FadeToLevel("LoseGame");
            // SceneManager.LoadScene("LoseGame");
        }
    }

    private IEnumerator winCoroutine()
    {
        yield return new WaitForSeconds(2);
        leverChangerScript.FadeToLevel("WinGame");
    }

    private IEnumerator loseCoroutine()
    {
        yield return new WaitForSeconds(2);
        leverChangerScript.FadeToLevel("LoseGame");
    }

}
