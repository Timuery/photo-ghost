using UnityEngine;

public abstract class ScriptToUse : MonoBehaviour
{
    [Header("ScriptToUse")]
    public bool active;
    public abstract void Toggle();
}

public abstract class Opened : ScriptToUse
{
    public bool state;
    public abstract void SetState(bool state);
    [SerializeField] public AudioClip CantOpen;
}
