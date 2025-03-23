using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light lightSource; // Ссылка на источник света
    public KeyCode interactKey = KeyCode.E; // Клавиша для взаимодействия
    public float pressAngle = 15f; // Угол, на который поворачивается кнопка при нажатии
    public float pressSpeed = 5f; // Скорость вращения кнопки
    public AudioClip switchSound; // Звук нажатия кнопки

    private bool isPlayerNear = false; // Игрок рядом с кнопкой
    private bool isPressed = false; // Кнопка нажата
    private Quaternion initialRotation; // Начальное вращение кнопки
    private Quaternion targetRotation; // Целевое вращение кнопки
    private AudioSource audioSource; // Источник звука

    private void Start()
    {
        // Сохраняем начальное вращение кнопки
        initialRotation = transform.rotation;
        // Вычисляем целевое вращение (нажатое состояние)
        targetRotation = initialRotation * Quaternion.Euler(pressAngle, 0, 0);

        // Получаем или добавляем AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Убедимся, что свет включен изначально
        if (lightSource != null)
        {
            lightSource.enabled = true;
        }
    }

    private void Update()
    {
        // Проверяем, находится ли игрок рядом с кнопкой
        if (isPlayerNear && Input.GetKeyDown(interactKey))
        {
            ToggleLight(); // Включаем/выключаем свет
            ToggleButton(); // Переключаем состояние кнопки
            PlaySwitchSound(); // Проигрываем звук нажатия
        }

        // Плавно вращаем кнопку в нужное состояние
        if (isPressed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * pressSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * pressSpeed);
        }
    }

    private void ToggleLight()
    {
        // Переключаем состояние света
        if (lightSource != null)
        {
            lightSource.enabled = !lightSource.enabled;
        }
    }

    private void ToggleButton()
    {
        // Переключаем состояние кнопки
        isPressed = !isPressed;
    }

    private void PlaySwitchSound()
    {
        // Проигрываем звук нажатия кнопки
        if (switchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(switchSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что игрок вошел в зону взаимодействия
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что игрок вышел из зоны взаимодействия
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}