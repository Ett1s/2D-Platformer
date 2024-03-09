using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class TriggerShakeAndLoadScene : MonoBehaviour
{
    public Animator animator; //Ссылка на Animator объекта "Затухание"

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        SceneManager.LoadScene(3);
    }
}