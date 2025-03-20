using System;

[Flags]
public enum PlayerEffect
{
    None = 0,           // ����� ����� �����, 0
    Walk = 1 << 0,      // ����� ����� ����� 1
    Running = 1 << 1,   // ����� ����� ������ 2
    Stunning = 1 << 2,  // ����� ����� �������� 4
    Hit = 1 << 3,       // ����� ����� �������� ���� 8
    Photo = 1 << 4      // ����� ����� ��������� � ������ ���������� 16
}
public enum Condition
{
    Deactive,
    Active,
    Destroyed,
    Anywhere

}
public enum EntityCondition
{
    Friendly,
    Neutral,
    Agressive
}
public interface IUseble
{
    public void Use();
    public string Information();
    public Condition GetCondition();
    public void SetCondition(Condition cdn);
}
public interface IEntity
{
    public string Information();
    public EntityCondition GetCondition();
    public void SetCondition(EntityCondition cdn);
}
