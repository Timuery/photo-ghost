using UnityEngine;

public class ZoneTrigger : Trigger
{
    [Header("���� � �������� ����� �������������� �������")]
    public LayerMask lms;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("PIDORAS1");
            Toggle();
        }
        Debug.Log("PIDORAS");
    }
    
}
