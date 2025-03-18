using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class _itemManager : MonoBehaviour
{
    private Rigidbody rb;

    public float acceleration = 2f; // ����� �����������
    public float maxSpeed = 15f;    // ������������ ��������
    private Vector3 _velocity;      // ������� ��������


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RigidbodyState(bool state)
    {
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
        }
    }
    public void MoveItem(Transform arm)
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
