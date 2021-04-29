using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    public LevelChangerScript leverChangerScript;

    public void RestartButton() {
        leverChangerScript.FadeToLevel("Game");
        // SceneManager.LoadScene("Game");
    }

    public void MenuButton() {
        leverChangerScript.FadeToLevel("Menu");
        // SceneManager.LoadScene("Menu");
    }
}
