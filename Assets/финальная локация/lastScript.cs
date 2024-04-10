using UnityEngine;
using Cinemachine;
using System.Collections;

public class lastScript : MonoBehaviour
{
    public Animator objectAnimator; // Ссылка на Animator объекта
    public CinemachineVirtualCamera cinemachineCamera; // Ссылка на Cinemachine Virtual Camera
    public Transform newFollowTarget; // Новая цель слежки после вхождения в коллайдер
    public Animator otherObjectAnimator;
    public Animator otherObjectAnimator2;// Ссылка на Animator другого объекта
    public Animator otherObjectAnimator3;
    public Animator otherObjectAnimator4;
    private bool hasPlayed = false;
    public AudioClip soundToPlay; // Аудиоклип, который будет воспроизводиться

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Проверяем, что в коллайдер вошел игрок
        {
            if (!hasPlayed)
            {
                GetComponent<AudioSource>().PlayOneShot(soundToPlay);
                hasPlayed = true;
            }
            // Активируем анимацию
            objectAnimator.SetTrigger("FadeOut");

            // Изменяем цель преследования для Cinemachine на новую цель
            if (newFollowTarget != null)
            {
                cinemachineCamera.Follow = newFollowTarget;
            }

            // Запускаем корутину для активации другой анимации через 3 секунды
            StartCoroutine(ActivateOtherAnimationAfterDelay());
        }
    }

    IEnumerator ActivateOtherAnimationAfterDelay()
    {
        // Ожидаем 3 секунды
        yield return new WaitForSeconds(3f);

        // Активируем другую анимацию
        otherObjectAnimator.SetTrigger("FadeIn");
        otherObjectAnimator2.SetTrigger("FadeOut2");
        otherObjectAnimator3.SetTrigger("FadeOut3");
        otherObjectAnimator4.SetTrigger("FadeOut4");
    }
}