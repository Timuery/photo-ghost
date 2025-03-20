using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class ItemManager : MonoBehaviour
{
    private Rigidbody rb;
    [Header("��������� ����")]
    [SerializeField] private float playerStrength = 30f;    // ���� ��������� (��)
    [SerializeField] private float resistanceForce = 15f;   // ������������� ��� ������� ��������

    [Header("������� ���������")]
    [SerializeField] private float acceleration = 25f;      // ������� ���������
    [SerializeField] private float maxSpeed = 8f;           // ����. �������� (�/�)



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
            // �������� ��������� ������� � ������
            float objectMass = rb.mass;
            float strengthRatio = Mathf.Clamp(playerStrength / objectMass, 0.1f, 1f);

            Vector3 targetPosition = arm.position;
            Vector3 direction = (targetPosition - rb.position).normalized;

            // ������� �������� ��� ����� �����
            float baseSpeed = Mathf.Min(
                maxSpeed,
                Vector3.Distance(rb.position, targetPosition) * acceleration
            );

            // ��������� �������� � ������ ����� � ���� ������
            float finalSpeed = baseSpeed * strengthRatio;

            // ��������� ��������
            rb.linearVelocity = direction * finalSpeed;

            // �������������� ������ "���������" ��� ������� ��������
            if (strengthRatio < 0.5f)
            {
                rb.AddForce(-direction * resistanceForce * (1 - strengthRatio));
            }
        }

    }
}
