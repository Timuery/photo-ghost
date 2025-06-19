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
        // ��������� UI ��� ������, ���� �� �� �������
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


        // ����� �������� ����� ������ ���������� ���������� �������
        Debug.Log($"PC UI {(active ? "opened" : "closed")}");
    }
    // ����� ��� ���������� ������ � ������ ��������� ����
    public void TryAddDataToUI(Sprite spr, string text)
    {
        foreach (var uiIndex in uIIndices)
        {
            if (uiIndex.TMPROtext.text == "" && uiIndex.image.sprite == null)
            {
                Debug.Log(uiIndex.image.name + " " + uiIndex.TMPROtext.text + " � ��� �����");
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
            return true; // ���� ��� ������ null � ���� ����

        // ���������, ��� Image ���� null, ���� �� ����� �������
        bool isImageEmpty = (uiIndex.image == null) ||
                          (uiIndex.image.sprite == null);

        // ���������, ��� Text ���� null, ���� ������ ������
        bool isTextEmpty = (uiIndex.TMPROtext == null) ||
                          (uiIndex.TMPROtext.text == "");

        return isImageEmpty && isTextEmpty;
    }

    public void CloseUI()
    {
        active = false;
        canvas.SetActive(false);
    }
    // ����� ��� ������� ����������� �����
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
        // ��-�� ���
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
 * � ��� ���� UI, (MONITOR) ��� ����� ���� ��������.
 * ������� ��������� ��� ���������
 * �������� ��� ������
 * ������� ��� ����� ���� �� ������� ���� UI.
 */
