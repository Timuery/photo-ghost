using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class _itemManager : MonoBehaviour
{
    private Rigidbody rb;

    public float acceleration = 2f; // Время сглаживания
    public float maxSpeed = 15f;    // Максимальная скорость
    private Vector3 _velocity;      // Текущая скорость


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
            // Настройки для лучшего отслеживания столкновений
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else // Выбрасывание
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

        // Рассчитываем скорость
        rb.linearVelocity = direction * Mathf.Min(
            maxSpeed,
            Vector3.Distance(rb.position, targetPosition) * acceleration
        );
    }
}
