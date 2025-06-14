using UnityEngine;

public class LightSwitch : SriptToUse
{
    public Light[] lightSources; // Массив источников света
    public KeyCode interactKey = KeyCode.E; // Клавиша для взаимодействия
    public float pressAngle = 15f; // Угол, на который поворачивается кнопка при нажатии
    public float pressSpeed = 5f; // Скорость вращения кнопки
    public AudioClip pressSound; // Звук нажатия кнопки

    private bool isPressed = false; // Кнопка нажата
    private Quaternion initialRotation; // Начальное вращение кнопки
    private Quaternion targetRotation; // Целевое вращение кнопки
    private AudioSource audioSource; // Источник звука
    private bool[] areLightsInitiallyEnabled; // Начальное состояние источников света

    private void Start()
    {
        // Сохраняем начальное вращение кнопки
        initialRotation = transform.rotation;
        // Вычисляем целевое вращение (нажатое состояние)
        targetRotation = initialRotation * Quaternion.Euler(pressAngle, 0, 0);

        // Получаем или добавляем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Сохраняем начальное состояние источников света
        areLightsInitiallyEnabled = new bool[lightSources.Length];
        for (int i = 0; i < lightSources.Length; i++)
        {
            if (lightSources[i] != null)
            {
                areLightsInitiallyEnabled[i] = lightSources[i].enabled;
            }
        }
    }

    private void Update()
    {
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

    public void ToggleLights()
    {
        // Переключаем состояние всех источников света
        for (int i = 0; i < lightSources.Length; i++)
        {
            if (lightSources[i] != null)
            {
                lightSources[i].enabled = !lightSources[i].enabled;
            }
        }
        ToggleButton(); // Переключаем состояние кнопки
        PlayPressSound(); // Проигрываем звук нажатия
    }

    private void ToggleButton()
    {
        // Переключаем состояние кнопки
        isPressed = !isPressed;
    }

    private void PlayPressSound()
    {
        // Проигрываем звук нажатия кнопки, если он назначен
        if (pressSound != null)
        {
            audioSource.PlayOneShot(pressSound);
        }
    }

    public override void Toggle()
    {
        ToggleLights();
    }
}