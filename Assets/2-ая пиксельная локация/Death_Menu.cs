using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathTrigger : MonoBehaviour
{
    public Animator playerAnimator; // ������ �� �������� ������
    public GameObject pauseMenu; // ������ �� ������ � ������� ����
    public float pauseDelay = 2f; // �������� ����� ������
    [SerializeField] private AudioClip Death_Sount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ������ �������� ������
            playerAnimator.SetTrigger("Death");

            AudioSource.PlayClipAtPoint(Death_Sount, transform.position);

            // ��������� �������� ��� ����� � ������ ����
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
        // �������� ����� ������
        yield return new WaitForSeconds(pauseDelay);

        // ����� ����
        Time.timeScale = 0f;

        // ����� �������� ����
        pauseMenu.SetActive(true);
    }
}