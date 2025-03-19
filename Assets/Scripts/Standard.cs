using UnityEngine;

public class Standard : MonoBehaviour, IUseble
{
    public Condition condition;
    

    public Condition GetCondition()
    {
        throw new System.NotImplementedException();
    }

    public string Information()
    {
        throw new System.NotImplementedException();
    }

    public void SetCondition(Condition cdn)
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    Condition IUseble.GetCondition()
    {
        throw new System.NotImplementedException();
    }

    string IUseble.Information()
    {
        throw new System.NotImplementedException();
    }

    void IUseble.SetCondition(Condition cdn)
    {
        throw new System.NotImplementedException();
    }

    void IUseble.Use()
    {
        throw new System.NotImplementedException();
    }
}
