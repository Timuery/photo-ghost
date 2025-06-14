using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum GhostState
{
    None,
    Aggressive
}

public class GhostBehavior : MonoBehaviour
{
    [SerializeField] private int ghostHP;
    public Transform[] teleportPoints; // Массив точек для телепортации
    public float teleportDistance = 3f; // Расстояние, на котором призрак телепортируется
    public float minTeleportDelay = 2f; // Минимальная задержка между телепортациями
    public float maxTeleportDelay = 5f; // Максимальная задержка между телепортациями

    [SerializeField] private float attachDistane = 6f;
    public float minAttach = 2f; // Минимальная задержка между телепортациями
    public float maxAttach = 5f; // Максимальная задержка между телепортациями
    public float nextAttachtime = 0f;
    [SerializeField] private LayerMask mask;
    public GameObject Player;
    private float nextTeleportTime; // Время следующей телепортации

    public GhostState state;

    public AudioSource audioSource;
    public AudioClip ghostSound;

    private void Start()
    {
        // Устанавливаем время следующей телепортации
        SetNextTeleportTime();
    }

    private void Update()
    {
        // Проверяем, настало ли время для случайной телепортации
        if (Time.time >= nextTeleportTime)
        {
            Teleport();
            SetNextTeleportTime();
        }

        // Проверяем, находится ли игрок слишком близко
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            GhostLogic();
        }
    }

    public void GetDamage(int damage)
    {
        Teleport();
        SetNextTeleportTime();
        ghostHP -= damage;
        if (ghostHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Teleport()
    {
        try
        {
            if (teleportPoints.Length == 0)
            {
                Debug.LogWarning("Нет точек для телепортации!");
                return;
            }

            // Выбираем случайную точку из массива
            int randomIndex = Random.Range(0, teleportPoints.Length);
            Transform newPosition = teleportPoints[randomIndex];


            // Телепортируем призрака на новую позицию
            transform.position = newPosition.position;
            audioSource.PlayOneShot(ghostSound);

            Debug.Log("Призрак телепортировался на новую позицию!");
        }
        catch
        {
            return;
        }
        
    }

    private void SetNextTeleportTime()
    {
        // Устанавливаем случайное время для следующей телепортации
        nextTeleportTime = Time.time + Random.Range(minTeleportDelay, maxTeleportDelay);
    }

    private void GhostLogic()
    {
        int id = Random.Range(0, 1);
        if (state == GhostState.Aggressive)
        {
            // Бросок Предмета
            if (id == 0)
            {
                ThrowItem();
            }

            if (id == 1)
            {

            }
        }
    }

    private void ThrowItem()
    {
        var objectsInCollider = GetObjectsInRadius(mask);
        Debug.Log("БРОСОК начало");

        // Если объекты найдены, выбираем случайный и бросаем его
        if (objectsInCollider.Count > 0)
        {
            // Создаем список для объектов с Rigidbody
            List<GameObject> validObjects = new List<GameObject>();

            // Фильтруем объекты, оставляя только те, у которых есть Rigidbody
            foreach (var obj in objectsInCollider)
            {
                if (obj.GetComponent<Rigidbody>() != null)
                {
                    validObjects.Add(obj);
                }
            }

            // Если есть объекты с Rigidbody, выбираем случайный и бросаем его
            if (validObjects.Count > 0)
            {
                GameObject item = validObjects[Random.Range(0, validObjects.Count)];
                Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

                ThrowItem(itemRigidbody, Player.transform, 20f);
                Debug.Log("БРОСОК конец");
            }
            else
            {
                // Если объектов с Rigidbody нет, перестаем "кидаться"
                Debug.Log("Объектов с Rigidbody для броска не найдено. Прекращаем кидаться.");
                state = GhostState.None; // Меняем состояние на неактивное
            }
        }
        else
        {
            // Если объектов нет, перестаем "кидаться"
            Debug.Log("Объектов для броска не найдено. Прекращаем кидаться.");
            state = GhostState.None; // Меняем состояние на неактивное
        }
    }
    public void UseItem()
    {
        // Получение всех объектов с которыми можно взаимодействовать
        var objectsInCollider = GetObjectsInRadius(LayerMask.GetMask("Useble"));
    }

    private List<GameObject> GetObjectsInRadius(LayerMask msk)
    {
        
        // Используем позицию текущего объекта как центр сферы
        Vector3 center = transform.position;

        // Находим все коллайдеры в радиусе
        Collider[] hitColliders = Physics.OverlapSphere(center, attachDistane, mask);

        // Преобразуем коллайдеры в GameObject
        List<GameObject> result = new List<GameObject>();
        foreach (var collider in hitColliders)
        {
            result.Add(collider.gameObject);
        }

        return result;
    }

    private void ThrowItem(Rigidbody a, Transform b, float forceMultiplier = 1f)
    {
        if (Time.time >= nextAttachtime)
        {
            if (a == null || b == null)
            {
                Debug.LogError("Не найден объект");
                return;
            }
            Vector3 dir = (b.position - a.position).normalized;

            float mass = a.mass;

            Vector3 force = dir * mass * forceMultiplier;

            // Применяем силу к объекту A
            a.AddForce(force, ForceMode.Impulse);
            nextAttachtime = Time.time + Random.Range(minAttach, maxAttach);
            Debug.Log($"Объект {a.name} брошен в {b.name} с силой {force.magnitude}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextAttachtime = Time.time + Random.Range(minAttach, maxAttach);
            state = GhostState.Aggressive;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = GhostState.None;
        }
    }
}