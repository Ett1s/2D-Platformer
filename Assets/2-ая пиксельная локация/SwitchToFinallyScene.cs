using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class SwitchToFinallyScene2 : MonoBehaviour
{
    public Animator animator; //������ �� Animator ������� "���������"
    private bool isInsideTrigger = false; // ���� ��� ������������ ���������� ������ ��������
    [SerializeField] private AudioClip door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ��������������, ��� � ��������� ���� ��� "Player"
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
            AudioSource.PlayClipAtPoint(door, transform.position);
            //������ �������� FadeIn
            animator.SetTrigger("FadeIn");

            //�������� ����� ����� 2 ������� ����� ������ ��������
            StartCoroutine(LoadSceneAfterFadeIn(4f));
        }
    }

    IEnumerator LoadSceneAfterFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(9);
    }
}