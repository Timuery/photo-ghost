using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhotoCollectionManager : MonoBehaviour
{
    [SerializeField] private GameObject photoCollectionPanel; // Основная панель
    [SerializeField] private GameObject[] tabs; // Вкладки (3 панели)
    [SerializeField] private Button[] tabButtons; // Кнопки вкладок

    private bool isOpen = false;

    void Start()
    {
        photoCollectionPanel.SetActive(false); // По умолчанию панель скрыта
        SwitchTab(0); // Открываем первую вкладку
    }


    public void TogglePanel()
    {
        isOpen = !isOpen;
        photoCollectionPanel.SetActive(isOpen);

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None; // Разблокируем мышь
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Прячем мышь обратно
            Cursor.visible = false;
        }
    }

    public void SwitchTab(int tabIndex)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(i == tabIndex);
        }
    }
}