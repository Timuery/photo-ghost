using UnityEngine;

// Singleton для управления UI
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
        Debug.Log(text + " Я тут Сучки");
        if (sprite != null && text != null)
        {
            Debug.Log("Я тут Сучки 3");
            Debug.Log("Я тут Сучки 22");
            DOSKA.TryAddDataToUI(sprite, text);
        }
        else
        {
            Debug.LogError("Ты где-то что-то проебал");
        }
        
    }
    public void CloseUI()
    {
        DOSKA.CloseUI();
        COMPUDAHTER.CloseUI();
    }
}
