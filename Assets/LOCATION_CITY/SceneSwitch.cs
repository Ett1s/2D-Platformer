using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class TriggerShakeAndLoadScene : MonoBehaviour
{
    public Animator animator; //������ �� Animator ������� "���������"

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //������ �������� FadeIn
            animator.SetTrigger("FadeIn");

            //�������� ����� ����� 2 ������� ����� ������ ��������
            StartCoroutine(LoadSceneAfterFadeIn(2f));
        }
    }

    IEnumerator LoadSceneAfterFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
    }
}