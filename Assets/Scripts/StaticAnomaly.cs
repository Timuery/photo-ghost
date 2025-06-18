using UnityEngine;

public class Misk : MonoBehaviour, IUseble
{
    public Condition condition;
    public virtual void Use()
    {
        Debug.Log("Using the Misk");
    }
    public virtual string Information()
    {
        return "A handy, portable, and delicious snack.";
    }
    public virtual Condition GetCondition()
    {
        return condition;
    }
    public virtual void SetCondition(Condition cdn)
    {
        condition = cdn;
    }
}
