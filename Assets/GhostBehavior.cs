using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public Transform[] teleportPoints; // Массив точек для телепортации
    public float teleportDistance = 3f; // Расстояние, на котором призрак телепортируется
    public float minTeleportDelay = 2f; // Минимальная задержка между телепортациями
    public float maxTeleportDelay = 5f; // Максимальная задержка между телепортациями

    private float nextTeleportTime; // Время следующей телепортации

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < teleportDistance)
        {
            Teleport();
            SetNextTeleportTime();
        }
    }

    private void Teleport()
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

    private void SetNextTeleportTime()
    {
        // Устанавливаем случайное время для следующей телепортации
        nextTeleportTime = Time.time + Random.Range(minTeleportDelay, maxTeleportDelay);
    }
}