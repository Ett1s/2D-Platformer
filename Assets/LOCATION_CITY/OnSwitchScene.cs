using UnityEngine;

public class OnSwitchScene : MonoBehaviour
{
    public AudioClip audioMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           GetComponent<AudioSource>().PlayOneShot(audioMessage);
        }
    }

}
