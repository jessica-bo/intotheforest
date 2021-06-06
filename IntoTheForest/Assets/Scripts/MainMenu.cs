using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public LevelChangerScript leverChangerScript; // make level changer script accessible
    public AudioSource AudioSource; // get the audio source to fade out

    // Ensure cursor is visible and non-locked
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Fade audio and change game
    public void PlayGame()
    {
        FadeAudioSource.StartFade(AudioSource, 2, 0);
        leverChangerScript.FadeToLevel("Game");
    }

    // Quit the application
    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
