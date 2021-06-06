using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class RestartGame : MonoBehaviour
{
    
    public LevelChangerScript leverChangerScript; // make level changing script accessible
    public AudioSource AudioSource; // audio source to fade

    // ensure cursor is unlocked and visible
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Fade audio and restart game
    public void RestartButton() {
        FadeAudioSource.StartFade(AudioSource, 2, 0);
        leverChangerScript.FadeToLevel("Game");
    }


    // Fade audio and reset to menu
    public void MenuButton() {
        FadeAudioSource.StartFade(AudioSource, 2, 0);
        leverChangerScript.FadeToLevel("Menu");
    }
}
