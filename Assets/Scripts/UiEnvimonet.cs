using UnityEngine;

// Singleton ��� ���������� UI
public class UIEnvironment: MonoBehaviour
{
    [SerializeField] private PC COMPUDAHTER;
    [SerializeField] private toUI DOSKA;

    private static UIEnvironment _instance;
    public static UIEnvironment Instance => _instance ??= new UIEnvironment();

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    public void SetTextAndImage(Sprite sprite, string text)
    {
        Debug.Log(text + " � ��� �����");
        if (sprite != null && text != null)
        {
            Debug.Log("� ��� ����� 3");
            Debug.Log("� ��� ����� 22");
            DOSKA.TryAddDataToUI(sprite, text);
        }
        else
        {
            Debug.LogError("�� ���-�� ���-�� �������");
        }
        
    }
    public void CloseUI()
    {
        DOSKA.CloseUI();
        COMPUDAHTER.CloseUI();
    }
}
