using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class SwitchSceneWithoutKey : MonoBehaviour
{
    public Animator animator; // Ссылка на Animator объекта "Затухание"

    // Метод вызывается при нажатии на кнопку
    public void OnClick()
    {
        // Запуск анимации FadeIn
        animator.SetTrigger("FadeIn");

        // Загрузка сцены через 2 секунды после начала анимации
        StartCoroutine(LoadSceneAfterFadeIn(2f));
    }

    IEnumerator LoadSceneAfterFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}