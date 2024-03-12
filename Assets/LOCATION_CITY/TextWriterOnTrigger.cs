using UnityEngine;
using System.Collections;
using TMPro;

public class TextWriterOnTrigger : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float textSpeed;
    public string[] texts; // ������ �������
    public bool HasTrigger = false;

    private int currentIndex = -1;
    private Coroutine currentCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HasTrigger != true)
        {
            if (other.CompareTag("Player"))
            {
                currentIndex = (currentIndex + 1) % texts.Length; // ������� � ���������� ������
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine); // ���������� ���������� ��������, ���� ��� ����
                currentCoroutine = StartCoroutine(ShowText(texts[currentIndex])); // ������ ����� ��������
                HasTrigger = true;
            }
        }
    }

    IEnumerator ShowText(string fullText)
    {
        textDisplay.text = ""; // ������� ���������� ���� ����� ����� �������
        int i = 0;
        while (i < fullText.Length)
        {
            textDisplay.text += fullText[i];
            yield return new WaitForSeconds(textSpeed);
            i++;
        }
    }
}