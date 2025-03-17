using System;
using UnityEngine;

public enum Condition
{
    Deactive,
    Active,
    Destroyed 

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
