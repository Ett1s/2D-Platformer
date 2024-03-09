using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip soundToPlay; // ���������, ������� ����� ����������������

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �������� ���� �������
        if (other.gameObject.tag == "Player")
        {
            if (!hasPlayed)
            {
                GetComponent<AudioSource>().PlayOneShot(soundToPlay);
                hasPlayed = true;
            }
        }
    }
}
