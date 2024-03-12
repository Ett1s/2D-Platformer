using UnityEngine;
using System.Collections;
using TMPro;

public class TextWriterOnTrigger : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float textSpeed;
    public string[] texts; // Массив текстов
    public bool HasTrigger = false;

    private int currentIndex = -1;
    private Coroutine currentCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HasTrigger != true)
        {
            if (other.CompareTag("Player"))
            {
                currentIndex = (currentIndex + 1) % texts.Length; // Переход к следующему тексту
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine); // Остановить предыдущую корутину, если она есть
                currentCoroutine = StartCoroutine(ShowText(texts[currentIndex])); // Запуск новой корутины
                HasTrigger = true;
            }
        }
    }

    IEnumerator ShowText(string fullText)
    {
        textDisplay.text = ""; // Очистка текстового поля перед новым текстом
        int i = 0;
        while (i < fullText.Length)
        {
            textDisplay.text += fullText[i];
            yield return new WaitForSeconds(textSpeed);
            i++;
        }
    }
}