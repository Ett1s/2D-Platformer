using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologTrigger : MonoBehaviour
{
    [Header("Текст монолога")]
    [TextArea(3,10)]
    [SerializeField] private string message;
    public AudioClip audioMessage;
    public bool hasTriggered = false;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MonologManadger.displayTipEvent?.Invoke(message);
            if (audioMessage != null && !hasTriggered)
            {
                GetComponent<AudioSource>().PlayOneShot(audioMessage);
            }
            hasTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MonologManadger.disableTipEvent?.Invoke();
        }
    }
}
