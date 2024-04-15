using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class SwitchToFinallyScene2 : MonoBehaviour
{
    public Animator animator; //Ссылка на Animator объекта "Затухание"
    private bool isInsideTrigger = false; // Флаг для отслеживания нахождения внутри триггера

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Предполагается, что у персонажа есть тег "Player"
        {
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
        }
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            //Запуск анимации FadeIn
            animator.SetTrigger("FadeIn");

            //Загрузка сцены через 2 секунды после начала анимации
            StartCoroutine(LoadSceneAfterFadeIn(2f));
        }
    }

    IEnumerator LoadSceneAfterFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(9);
    }
}