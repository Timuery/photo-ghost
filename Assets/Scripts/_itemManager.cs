using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class ItemManager : MonoBehaviour
{
    private Rigidbody rb;
    private bool isAttached = false;
    private Transform targetTransform;

    [Header("��������� ����")]
    [SerializeField] private float playerStrength = 30f;
    [SerializeField] private float resistanceForce = 15f;

    [Header("��������� ��������")]
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float snapDistance = 0.1f;
    [SerializeField] private float attachRotationSpeed = 10f;

    public void RigidbodyState(bool state, GameObject item)
    {
        rb = item.GetComponent<Rigidbody>();
        isAttached = false;

        if (!state) // ����� �������
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else // ��������� �������
        {
            
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb = null;
            targetTransform = null;
            
        }
    }
    public void MoveItem(Transform arm)
    {
        if (rb == null) return;

        targetTransform = arm;

        if (isAttached)
        {
            // ���������� ������� � ������������� �������
            rb.transform.position = targetTransform.position;
            rb.transform.rotation = Quaternion.Slerp(
                rb.transform.rotation,
                targetTransform.rotation,
                attachRotationSpeed * Time.deltaTime
            );
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        float distance = Vector3.Distance(rb.position, targetTransform.position);

        // �������� �� ���������� ����������� ���������
        if (distance < snapDistance)
        {
            isAttached = true;
            rb.transform.position = targetTransform.position;
            rb.linearVelocity = Vector3.zero;
            return;
        }

        float objectMass = rb.mass;
        float strengthRatio = Mathf.Clamp(playerStrength / objectMass, 0.1f, 1f);

        Vector3 direction = (targetTransform.position - rb.position).normalized;
        float baseSpeed = Mathf.Min(maxSpeed, distance * acceleration);
        float finalSpeed = baseSpeed * strengthRatio;

        // ������� ��������� ��� �����������
        float speedMultiplier = Mathf.Clamp(distance / snapDistance, 0.5f, 1f);
        rb.linearVelocity = direction * finalSpeed * speedMultiplier;

        // �������������� ������������� ��� ������� ���������
        if (strengthRatio < 0.5f)
        {
            rb.AddForce(-direction * resistanceForce * (1 - strengthRatio));
        }
    }
}
