using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class toUI : ScriptToUse
{
    [Header("PC UI Settings")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private List<UIIndex> uIIndices = new List<UIIndex>();
    private void Awake()
    {
        // Отключаем UI при старте, если он не активен
        if (canvas != null)
            canvas.SetActive(active);
    }
    public override void Toggle()
    {
        active = !active;
        canvas.SetActive(active);
       

        PlayerController controller = FindFirstObjectByType<PlayerController>();
        if (active)
        {
            controller.effectController.AddEffect(PlayerEffect.InUI);
            controller.effectController.RemoveEffect(PlayerEffect.Photo);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            controller.effectController.RemoveEffect(PlayerEffect.InUI);
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Можно добавить здесь логику блокировки управления игроком
        Debug.Log($"PC UI {(active ? "opened" : "closed")}");
    }
    // Метод для добавления данных в первый свободный слот
    public void TryAddDataToUI(Sprite spr, string text)
    {
        foreach (var uiIndex in uIIndices)
        {
            if (uiIndex.TMPROtext.text == "" && uiIndex.image.sprite == null)
            {
                Debug.Log(uiIndex.image.name + " " + uiIndex.TMPROtext.text + " Я тут Сучки");
                uiIndex.image.sprite = spr;
                uiIndex.TMPROtext.text = text;
                uiIndex.image.gameObject.SetActive(true);
                uiIndex.TMPROtext.transform.parent?.gameObject.SetActive(true);
                break;
            }
            
            /*if (IsSlotEmpty(uiIndex))
            {
                
            }*/
        }
    }
    public void CloseUI()
    {
        active = false;
        canvas.SetActive(false);
    }
}
[System.Serializable]
public class UIIndex
{
    public UIIndex() { }
    public UIIndex(Sprite image, string text)
    {
        this.image.sprite = image;
        this.TMPROtext.text = text;
    }
    public Image image;
    public TextMeshProUGUI TMPROtext;
}
/*
 * У нас есть UI, (MONITOR) его нужно сука вбрубить.
 * Сменить состояние для персонажа
 * Добавить там кнопки
 * Сделать так чтобы этот пк выдавал свой UI.
 */
