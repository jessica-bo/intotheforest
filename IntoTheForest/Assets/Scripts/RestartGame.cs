using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class RestartGame : MonoBehaviour
{
    
    public LevelChangerScript leverChangerScript;
    public AudioSource AudioSource;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartButton() {
        StartCoroutine(FadeAudioSource.StartFade(AudioSource, 2, 0));
        leverChangerScript.FadeToLevel("Game");
    }

    public void MenuButton() {
        StartCoroutine(FadeAudioSource.StartFade(AudioSource, 2, 0));
        leverChangerScript.FadeToLevel("Menu");
    }
}
