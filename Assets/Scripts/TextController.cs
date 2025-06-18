using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public static TextController Instance { get; private set; }

    
    [SerializeField] private Texts[] texts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //DontDestroyOnLoad(gameObject);
    }
    public Texts GetString(int index)
    {
        if (index >= 0 && index < texts.Length)
        {
            return texts[index];
        }
        return null;
    }
    

}

[System.Serializable]
public class Texts
{
    public int time;
    public string text;

    // ѕустой конструктор дл€ сериализации
    public Texts() { }

    //  онструктор с параметрами (необ€зательно)
    public Texts(int time, string text)
    {
        this.time = time;
        this.text = text;
    }
}
