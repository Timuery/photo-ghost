using UnityEngine;

public class Lamp : ScriptToUse
{
    Light lightComponent;
    public override void Toggle()
    {
        active = !active;

        if (lightComponent != null)
        {
            lightComponent.enabled = active;
        }
    }
    void Start()
    {
        lightComponent = GetComponent<Light>();
        if (lightComponent == null)
        {
            lightComponent = gameObject.AddComponent<Light>();
        }
        // �������������� ���������
        lightComponent.enabled = active;
    }
    // ��������� ����� �����
    public void ChangeColor(Color newColor)
    {
        if (lightComponent != null)
        {
            lightComponent.color = newColor;
        }

    }
    // ����� ��� �������� ������������� (��������, �� ������ ��������)
    public void UseAnywhere(Color color = default)
    {
        TurnOn();
        if (color != default)
        {
            ChangeColor(color);
        }
    }
    // ��������� ����� (����� ������� �����)
    public void TurnOn()
    {
        active = true;
        if (lightComponent != null)
        {
            lightComponent.enabled = true;
        }
    }
    // ���������� ����� (����� ������� �����)
    public void TurnOff()
    {
        active = false;
        if (lightComponent != null)
        {
            lightComponent.enabled = false;
        }
    }
}
