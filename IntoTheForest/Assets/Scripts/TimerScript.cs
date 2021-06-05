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
    
    public GameObject skull;
    public GameObject winZoneRuins;
    public GameObject winZoneOwl;
    public GameObject winZoneMountain;
    public GameObject winZoneHut;
    public Light winLight;
    public Color winColStart = new Color(1f, 0.5f, 1f, 1f);
    public Color winColEnd = new Color(1f, 0.8f, 1f, 1f);
    public static string endText;

    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.005f;

        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);

        lightToDim.color = colStart;
        lightToDim.intensity = 1f;

    }

    void Update()
    {
        // Countdown time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Skybox
            if (_skyboxBlendFactor < 1) 
            {
                _skyboxBlendFactor += 0.01f * Time.deltaTime;
                RenderSettings.skybox.SetFloat("_Blend", _skyboxBlendFactor);
            }

            // Dim light
            lightToDim.color = Color.Lerp(colStart, colEnd, Mathf.PingPong(Time.time, totalTime) / totalTime);
            lightToDim.intensity -= 0.002f * Time.deltaTime;

            if (PlayerCharacterController.MaxSpeedOnGround > 3 && timeRemaining < halfRemaining)
            {
                PlayerCharacterController.MaxSpeedOnGround -= 0.1f * Time.deltaTime;
                PlayerCharacterController.MaxSpeedInAir -= 0.125f * Time.deltaTime;
            }

            // Audio
            if (fadeInit != true && timeRemaining < halfRemaining + fadeTime / 2)
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ambient, fadeTime, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, fadeTime, 1));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_birds, fadeTime, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, fadeTime, 0.25f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, timeRemaining-5, 1));
                fadeInit = true;
            }

            // Fog
            if (timeRemaining < halfRemaining*2 && RenderSettings.fogDensity < 0.04f) {
                RenderSettings.fogDensity += 0.000015f; 
                //fog colour?
            }

            // Ruins win zone
            if (Mathf.Abs(player.transform.position.x - winZoneRuins.transform.position.x) < 12f && 
            (Mathf.Abs(player.transform.position.z - winZoneRuins.transform.position.z) < 12f))
            {
                float t = Mathf.PingPong (Time.time, 0.5f) / 0.5f;
                winLight.color = Color.Lerp(winColStart, winColEnd, t);
                winLight.intensity = Mathf.Lerp(0.5f, 2f, t);

                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the protective sigils, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Owl tree win zone
            if (Mathf.Abs(player.transform.position.x - winZoneOwl.transform.position.x) < 25f && 
            (Mathf.Abs(player.transform.position.z - winZoneOwl.transform.position.z) < 25f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the warden owl, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Mountain ruins win zone
            if (Mathf.Abs(player.transform.position.x - winZoneMountain.transform.position.x) < 25f && 
            (Mathf.Abs(player.transform.position.z - winZoneMountain.transform.position.z) < 25f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the ancient altar, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Hut ruins win zone
            if (Mathf.Abs(player.transform.position.x - winZoneHut.transform.position.x) < 25f && 
            (Mathf.Abs(player.transform.position.z - winZoneHut.transform.position.z) < 25f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the wizard's home, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Cave death scenario
            if ((player.transform.position.x - skull.transform.position.x) < -18f && 
            (Mathf.Abs(player.transform.position.z - skull.transform.position.z) < 10f) &&
            (Mathf.Abs(player.transform.position.y - skull.transform.position.y) < 5f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "How foolish, you stumbled right into my cave! Delicious morsels . . .";
                StartCoroutine(loseCoroutine(1.5f));
            }

            // Out of zone scenario
            if ((Mathf.Abs(player.transform.position.x) > 425f) || (Mathf.Abs(player.transform.position.z) > 400f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "You've crossed into the forbidden lands . . .  Enjoy a slow and painful death!";
                StartCoroutine(loseCoroutine(1f));
            }

        }

        else
        {
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0));
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

            endText = "Oh dear, another one lost to the forest . . .  Better hasten your steps next time";
            StartCoroutine(loseCoroutine(2f));
            // leverChangerScript.FadeToLevel("LoseGame");
            // SceneManager.LoadScene("LoseGame");
        }
    }

    public static string getEndText()
    {
        return endText;
    }

    private IEnumerator winCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        leverChangerScript.FadeToLevel("WinGame");
    }

    private IEnumerator loseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        leverChangerScript.FadeToLevel("LoseGame");
    }

}
