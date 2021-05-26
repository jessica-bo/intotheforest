using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    public LevelChangerScript leverChangerScript;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartButton() {
        leverChangerScript.FadeToLevel("Game");
    }

    public void MenuButton() {
        leverChangerScript.FadeToLevel("Menu");
    }
}
