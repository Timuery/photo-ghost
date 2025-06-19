using UnityEngine;

public class PaperItem : ItemPickup
{
    public Sprite _sprite;
    public string _text;
    public override void Toggle()
    {
        base.Toggle();
        UIController.Instance.VisibleImage(_sprite);
        UIEnvironment.Instance.SetTextAndImage(_sprite, _text);
    }
}
