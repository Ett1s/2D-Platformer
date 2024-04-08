using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathTrigger : MonoBehaviour
{
    public Animator playerAnimator; // Ссылка на аниматор игрока
    public GameObject pauseMenu; // Ссылка на объект с паузным меню
    public float pauseDelay = 2f; // Задержка перед паузой
    [SerializeField] private AudioClip Death_Sount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Начать анимацию смерти
            playerAnimator.SetTrigger("Death");

            AudioSource.PlayClipAtPoint(Death_Sount, transform.position);

            // Запустить корутину для паузы и вывода меню
            StartCoroutine(PauseAndShowMenu());
        }
    }

    public void Loadmenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void exit()
    {
        Application.Quit();
    }

    IEnumerator PauseAndShowMenu()
    {
        // Задержка перед паузой
        yield return new WaitForSeconds(pauseDelay);

        // Пауза игры
        Time.timeScale = 0f;

        // Вывод паузного меню
        pauseMenu.SetActive(true);
    }
}