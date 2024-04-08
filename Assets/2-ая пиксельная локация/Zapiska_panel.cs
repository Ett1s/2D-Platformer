using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Ссылка на объект панели
    public Button closeButton; // Ссылка на кнопку закрытия
    private bool isPlayerInTrigger = false; // Переменная для отслеживания нахождения игрока в триггере

    private void Start()
    {
        // Скрываем панель при старте
        panel.SetActive(false);

        // Добавляем слушатель на кнопку закрытия
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Устанавливаем флаг, что игрок находится в триггере
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Сбрасываем флаг, что игрок вышел из триггера
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        // Показываем панель при нажатии на кнопку F, если игрок находится в триггере
        if (Input.GetKeyDown(KeyCode.F) && isPlayerInTrigger)
        {
            panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        // Скрываем панель при нажатии на кнопку закрытия
        panel.SetActive(false);
    }
}