using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TypeWriterEffect : MonoBehaviour
{
    
    public float delay = 0.04f; // delay between each letter displayed
    private string currentText = ""; // displayed text
    public AudioSource AudioSource_type; // typewriter sound

    void Start()
    {
        StartCoroutine(ShowText()); // run typewriter script
    }

    IEnumerator ShowText() {
        string fullText = TimerScript.getEndText(); // get full text from end condition

        // start typewriter sound
        AudioSource_type.Play();
        yield return new WaitForSeconds(1.5f);

        // print each letter in sequence
        for (int i = 0; i <= fullText.Length; i++) {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
