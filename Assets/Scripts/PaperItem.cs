using UnityEngine;

public class PaperItem : ItemPickup
{
    public Sprite _sprite;
    public override void Toggle()
    {
        base.Toggle();
        UIController.Instance.VisibleImage(_sprite);
    }
}
