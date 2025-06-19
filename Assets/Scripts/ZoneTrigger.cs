using UnityEngine;

public class ZoneTrigger : Trigger
{
    [Header("���� � �������� ����� �������������� �������")]
    public LayerMask lms;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == lms)
        {
            Toggle();
        }
    }
}
