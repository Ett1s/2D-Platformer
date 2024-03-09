using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip soundToPlay; // Аудиоклип, который будет воспроизводиться

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка тега объекта
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
