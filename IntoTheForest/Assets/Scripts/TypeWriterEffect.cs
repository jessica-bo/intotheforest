using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TypeWriterEffect : MonoBehaviour
{
    
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";
    public AudioSource AudioSource_type;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        yield return new WaitForSeconds(1.5f);
        AudioSource_type.Play();
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i <= fullText.Length; i++) {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
