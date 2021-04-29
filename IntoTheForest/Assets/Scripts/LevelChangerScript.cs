using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour
{

    public Animator animator;
    private string newLevel;

    public void FadeToLevel (string levelToLoad) {
        newLevel = levelToLoad;
        animator.SetTrigger("FadeOut");

    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(newLevel);
    }
}
