using UnityEngine;
using System.Collections;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string fullText;
    public float textSpeed;

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        int i = 0;
        while (i < fullText.Length)
        {
            textDisplay.text += fullText[i];
            yield return new WaitForSeconds(textSpeed);
            i++;
        }
    }
}
