using System;


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
