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
        if (active) controller.effectController.AddEffect(PlayerEffect.InUI);
        controller.effectController.RemoveEffect(PlayerEffect.Photo);

        if (!active)
        {
            controller.effectController.RemoveEffect(PlayerEffect.InUI);
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
                break;
            }
            
            /*if (IsSlotEmpty(uiIndex))
            {
                
            }*/
        }
    }
    private bool IsSlotEmpty(UIIndex uiIndex)
    {
        if (uiIndex == null)
            return true; // Если сам объект null — слот пуст

        // Проверяем, что Image либо null, либо не имеет спрайта
        bool isImageEmpty = (uiIndex.image == null) ||
                          (uiIndex.image.sprite == null);

        // Проверяем, что Text либо null, либо пустая строка
        bool isTextEmpty = (uiIndex.TMPROtext == null) ||
                          (uiIndex.TMPROtext.text == "");

        return isImageEmpty && isTextEmpty;
    }

    public void CloseUI()
    {
        active = false;
        canvas.SetActive(false);
    }
    // Метод для очистки конкретного слота
    public void ClearSlot(int index)
    {
        if (index >= 0 && index < uIIndices.Count)
        {
            if (uIIndices[index].image != null)
            {
                uIIndices[index].image.sprite = null;
                uIIndices[index].image.enabled = false;
            }

            if (uIIndices[index].TMPROtext != null)
                uIIndices[index].TMPROtext.text = "";
        }
    }

    public void Enter()
    {
        // Чо-то там
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
