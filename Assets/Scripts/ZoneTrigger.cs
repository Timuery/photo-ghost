using UnityEngine;

public class ZoneTrigger : Trigger
{
    [Header("Слои с которыми будет контоктировать триггер")]
    public LayerMask lms;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == lms)
        {
            Toggle();
        }
    }
}
