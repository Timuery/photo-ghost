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
 * � ��� ���� UI, (MONITOR) ��� ����� ���� ��������.
 * ������� ��������� ��� ���������
 * �������� ��� ������
 * ������� ��� ����� ���� �� ������� ���� UI.
 */
