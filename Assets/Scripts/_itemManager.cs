using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class ItemManager : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Настройки силы")]
    [SerializeField] private float playerStrength = 30f;    // Сила персонажа (кг)
    [SerializeField] private float resistanceForce = 15f;   // Сопротивление для тяжелых объектов

    [Header("Базовые настройки")]
    [SerializeField] private float acceleration = 25f;      // Базовое ускорение
    [SerializeField] private float maxSpeed = 8f;           // Макс. скорость (м/с)



    public void RigidbodyState(bool state, GameObject item)
    {
        rb = item.GetComponent<Rigidbody>();
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
            rb = null;
        }
    }
    public void MoveItem(Transform arm)
    {
        if (rb != null)
        {
            // Получаем параметры объекта и игрока
            float objectMass = rb.mass;
            float strengthRatio = Mathf.Clamp(playerStrength / objectMass, 0.1f, 1f);

            Vector3 targetPosition = arm.position;
            Vector3 direction = (targetPosition - rb.position).normalized;

            // Базовая скорость без учета массы
            float baseSpeed = Mathf.Min(
                maxSpeed,
                Vector3.Distance(rb.position, targetPosition) * acceleration
            );

            // Финальная скорость с учетом массы и силы игрока
            float finalSpeed = baseSpeed * strengthRatio;

            // Применяем движение
            rb.linearVelocity = direction * finalSpeed;

            // Дополнительный эффект "натяжения" для тяжелых объектов
            if (strengthRatio < 0.5f)
            {
                rb.AddForce(-direction * resistanceForce * (1 - strengthRatio));
            }
        }

    }
}
