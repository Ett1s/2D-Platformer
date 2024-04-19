using UnityEngine;

public class HealthRestorer : MonoBehaviour
{
    private bool isPlayerInTrigger = false;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            ControllerHeropicsel playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerHeropicsel>();
            if (playerController != null)
            {
                playerController.RestoreHealth();
            }
        }
    }
}