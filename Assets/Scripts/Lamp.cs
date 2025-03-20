using UnityEngine;

public class Lamp : MonoBehaviour, IUseble
{
    public Condition condition;

    Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
    }
    public void Use()
    {
        if (condition == Condition.Destroyed) return;

        // ����������� ��������� ����� Active � Deactive
        condition = condition == Condition.Active
            ? Condition.Deactive
            : Condition.Active;
    }
    public void UseAnyWhere(Color color = default)
    {
        if (condition == Condition.Destroyed) condition = Condition.Deactive;
        condition = ~condition;
        if (color != default) ChangeColor(color);
    }
    public string Information()
    {
        return condition switch
        {
            Condition.Active => "�����: ��������",
            Condition.Deactive => "�����: ���������",
            Condition.Destroyed => "�����: �������",
            _ => "����������� ���������"
        };
    }
    public void ChangeColor(Color newColor)
    {
        if (condition != Condition.Destroyed)
        {
            lightComponent.color = newColor;
        }
    }
    public Condition GetCondition() => condition;
    public void SetCondition(Condition cdn)
    {
        condition = cdn;
        if (cdn == Condition.Destroyed)
        {
            StartDestroy();
        }
    }
    private void StartDestroy()
    {
        condition = Condition.Destroyed;
    }
}
