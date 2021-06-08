using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Unity.FPS.Gameplay;

public class TimerScript : MonoBehaviour
{

    public float timeRemaining; // the time remaining until the killscreen
    public float fadeTime; // fade time for audio transitions

    // light source to dim and initial / final colours
    public Light lightToDim;
    public Color colStart = new Color(1f, 0.92f, 0.92f, 1f); 
    public Color colEnd = new Color(0.55f, 0.51f, 1f, 1f);
    
    // level changer script
    public LevelChangerScript leverChangerScript;

    // Player objects
    public PlayerCharacterController PlayerCharacterController;
    public GameObject player;

    // audio mixer
    public AudioMixer mixer;

    // Audio sounds to fade in and out
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

    private float totalTime;
    private float halfRemaining;

    void Start()
    {
        totalTime = timeRemaining; // total time from beginning until killscreen
        halfRemaining = timeRemaining / 2; // time to initiate major changes

        // render fog
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.005f;

        // Set volume to menu value
        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);

        // set initial colour / intensity profile of lights
        lightToDim.color = colStart;
        lightToDim.intensity = 1f;

    }

    void Update()
    {
        // Countdown time
        if (timeRemaining > 0)
        {
            // Update time
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

            // Slow character speed
            if (PlayerCharacterController.MaxSpeedOnGround > 5 && timeRemaining < halfRemaining)
            {
                PlayerCharacterController.MaxSpeedOnGround -= 0.05f * Time.deltaTime;
                PlayerCharacterController.MaxSpeedInAir -= 0.0625f * Time.deltaTime;
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

                endText = "Drats, no dinner for me tonight . . .           You found the Protective Sigils, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Owl tree win zone
            if (Mathf.Abs(player.transform.position.x - winZoneOwl.transform.position.x) < 25f && 
            (Mathf.Abs(player.transform.position.z - winZoneOwl.transform.position.z) < 25f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the Warden Owl, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Mountain ruins win zone
            if (Mathf.Abs(player.transform.position.x - winZoneMountain.transform.position.x) < 25f && 
            (Mathf.Abs(player.transform.position.z - winZoneMountain.transform.position.z) < 25f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the Ancient Altar, I'll let you live this time";
                StartCoroutine(winCoroutine(3f));
            }

            // Hut ruins win zone
            if (Mathf.Abs(player.transform.position.x - winZoneHut.transform.position.x) < 25f && 
            (Mathf.Abs(player.transform.position.z - winZoneHut.transform.position.z) < 25f))
            {
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0f));
                StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

                endText = "Drats, no dinner for me tonight . . .           You found the Wizard's House, I'll let you live this time";
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
            if ((Mathf.Abs(player.transform.position.x) > 475f) || (Mathf.Abs(player.transform.position.z) > 475f))
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
            // Fade audio and enter time out kill screen
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_ominous, 1, 0));
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_crows, 1, 0));
            StartCoroutine(FadeAudioSource.StartFade(AudioSource_heart, 1, 0));

            endText = "Oh dear, another one lost to the forest . . .  Better hurry up next time!";
            StartCoroutine(loseCoroutine(2f));
        }
    }

    // Get win / kill screen end text
    public static string getEndText()
    {
        return endText;
    }

    // transition to win screen
    private IEnumerator winCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        leverChangerScript.FadeToLevel("WinGame");
    }

    // transition to lose screen
    private IEnumerator loseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        leverChangerScript.FadeToLevel("LoseGame");
    }

}
