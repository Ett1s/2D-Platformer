using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class HoverButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f;
    public float hoverDuration = 0.2f;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(initialScale * hoverScale, hoverDuration));
        GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f); // устанавливаем цвет на темнее
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(ScaleOverTime(initialScale, hoverDuration));
        GetComponent<Image>().color = Color.white; // возвращаем исходный цвет
    }

    IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        Vector3 originalScale = transform.localScale;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
