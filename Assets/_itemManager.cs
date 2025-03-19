using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class ItemManager : MonoBehaviour
{
    private Rigidbody rb;

    public float acceleration = 2f; // ����� �����������
    public float maxSpeed = 15f;    // ������������ ��������
    private Vector3 _velocity;      // ������� ��������

    public void RigidbodyState(bool state, GameObject item)
    {
        rb = item.GetComponent<Rigidbody>();
        if (!state)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = false;
            // ��������� ��� ������� ������������ ������������
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else // ������������
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb = null;
        }
    }
    public void MoveItem(Transform arm)
    {
        if (rb != null)
        {
            Vector3 targetPosition = arm.position;
            Vector3 direction = (targetPosition - rb.position).normalized;

            // ������������ ��������
            rb.linearVelocity = direction * Mathf.Min(
                maxSpeed,
                Vector3.Distance(rb.position, targetPosition) * acceleration
            );
        }
        
    }
}
