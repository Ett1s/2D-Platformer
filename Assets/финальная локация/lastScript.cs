using UnityEngine;
using Cinemachine;
using System.Collections;

public class lastScript : MonoBehaviour
{
    public Animator objectAnimator; // ������ �� Animator �������
    public CinemachineVirtualCamera cinemachineCamera; // ������ �� Cinemachine Virtual Camera
    public Transform newFollowTarget; // ����� ���� ������ ����� ��������� � ���������
    public Animator otherObjectAnimator;
    public Animator otherObjectAnimator2;// ������ �� Animator ������� �������
    public Animator otherObjectAnimator3;
    public Animator otherObjectAnimator4;
    private bool hasPlayed = false;
    public AudioClip soundToPlay; // ���������, ������� ����� ����������������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ���������, ��� � ��������� ����� �����
        {
            if (!hasPlayed)
            {
                GetComponent<AudioSource>().PlayOneShot(soundToPlay);
                hasPlayed = true;
            }
            // ���������� ��������
            objectAnimator.SetTrigger("FadeOut");

            // �������� ���� ������������� ��� Cinemachine �� ����� ����
            if (newFollowTarget != null)
            {
                cinemachineCamera.Follow = newFollowTarget;
            }

            // ��������� �������� ��� ��������� ������ �������� ����� 3 �������
            StartCoroutine(ActivateOtherAnimationAfterDelay());
        }
    }

    IEnumerator ActivateOtherAnimationAfterDelay()
    {
        // ������� 3 �������
        yield return new WaitForSeconds(3f);

        // ���������� ������ ��������
        otherObjectAnimator.SetTrigger("FadeIn");
        otherObjectAnimator2.SetTrigger("FadeOut2");
        otherObjectAnimator3.SetTrigger("FadeOut3");
        otherObjectAnimator4.SetTrigger("FadeOut4");
    }
}