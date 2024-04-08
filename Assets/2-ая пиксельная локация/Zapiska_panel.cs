using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // ������ �� ������ ������
    public Button closeButton; // ������ �� ������ ��������
    private bool isPlayerInTrigger = false; // ���������� ��� ������������ ���������� ������ � ��������

    private void Start()
    {
        // �������� ������ ��� ������
        panel.SetActive(false);

        // ��������� ��������� �� ������ ��������
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ������������� ����, ��� ����� ��������� � ��������
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ���������� ����, ��� ����� ����� �� ��������
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        // ���������� ������ ��� ������� �� ������ F, ���� ����� ��������� � ��������
        if (Input.GetKeyDown(KeyCode.F) && isPlayerInTrigger)
        {
            panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        // �������� ������ ��� ������� �� ������ ��������
        panel.SetActive(false);
    }
}