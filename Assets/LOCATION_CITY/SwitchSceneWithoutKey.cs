using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class SwitchSceneWithoutKey : MonoBehaviour
{
    public Animator animator; // ������ �� Animator ������� "���������"

    // ����� ���������� ��� ������� �� ������
    public void OnClick()
    {
        // ������ �������� FadeIn
        animator.SetTrigger("FadeIn");

        // �������� ����� ����� 2 ������� ����� ������ ��������
        StartCoroutine(LoadSceneAfterFadeIn(2f));
    }

    IEnumerator LoadSceneAfterFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}