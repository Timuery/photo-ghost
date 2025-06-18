using UnityEngine;

public class Trigger : EnergyBox
{
    public override void Toggle()
    {
        base.Toggle();
        Destroy(gameObject);
    }
}
