using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Trigger : EnergyBox
{
    [Header("Trigger")]
    public bool ToState;

    [Header("Если должен выписываться текст")]
    public int textID;
    public override void Toggle()
    {
        if (textID > 0)
        {
            UIController.Instance.TextPanel(textID);
        }
        Debug.Log("PIDORASS");
        active = ToState;
        base.ToggleAudio(active);
        base.ToggleBoolInOpened(active);
        if (active) Destroy(gameObject);

    }
}
