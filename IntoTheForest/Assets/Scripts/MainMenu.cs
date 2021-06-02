using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public LevelChangerScript leverChangerScript;
    public AudioSource AudioSource;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    public void PlayGame()
    {
        StartCoroutine(FadeAudioSource.StartFade(AudioSource, 2, 0));
        leverChangerScript.FadeToLevel("Game");
        // SceneManager.LoadScene("Game");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
