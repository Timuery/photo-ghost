using UnityEngine;

public class LightSwitch : ScriptToUse
{
    public Lamp[] lightSources; // Массив источников света
    public float pressAngle = 15f; // Угол, на который поворачивается кнопка при нажатии
    public float pressSpeed = 5f; // Скорость вращения кнопки
    public AudioClip pressSound; // Звук нажатия кнопки

    private Quaternion initialRotation; // Начальное вращение кнопки
    private Quaternion targetRotation; // Целевое вращение кнопки
    private AudioSource audioSource; // Источник звука

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
    }

    private void Update()
    {
        // Плавно вращаем кнопку в нужное состояние
        if (active)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * pressSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * pressSpeed);
        }
    }
    public override void Toggle()
    {
        active = !active;

        foreach (var source in lightSources)
        {
            source.Toggle();
        }
        PlayPressSound(); // Проигрываем звук нажатия
    }
    private void PlayPressSound()
    {
        // Проигрываем звук нажатия кнопки, если он назначен
        if (pressSound != null)
        {
            audioSource.PlayOneShot(pressSound);
        }
    }

}