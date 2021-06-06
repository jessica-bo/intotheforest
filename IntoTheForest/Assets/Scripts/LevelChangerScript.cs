using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour
{

    public Animator animator;
    private string newLevel;

    // Fade current scene
    public void FadeToLevel (string levelToLoad) {
        newLevel = levelToLoad;
        animator.SetTrigger("FadeOut");

    }

    // load new scene
    public void OnFadeComplete() {
        SceneManager.LoadScene(newLevel);
    }
}
