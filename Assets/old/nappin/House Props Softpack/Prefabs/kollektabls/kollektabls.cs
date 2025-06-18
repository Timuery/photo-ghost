using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : ScriptToUse
{
    public Image itemIcon; // Иконка предмета в UI (Canvas)
    public AudioClip pickupSound; // Звук подбора предмета

    private AudioSource audioSource; // Источник звука
    private SceneController sceneController;

    private void Start()
    {
        // Скрываем иконку предмета при старте
        if (itemIcon != null)
        {
            itemIcon.enabled = false;
        }

        // Получаем или добавляем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public override void Toggle()
    {
        // Воспроизводим звук подбора
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

        // Запускаем анимацию исчезновения предмета
        StartCoroutine(AnimatePickup());
    }
    private System.Collections.IEnumerator AnimatePickup()
    {
        // Анимация масштабирования предмета до нуля
        float duration = 0.5f; // Длительность анимации
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Удаляем объект предмета со сцены
        DestroyImmediate(transform);

        // Отображаем иконку предмета в UI
        if (itemIcon != null)
        {
            itemIcon.enabled = true;
        }

        Debug.Log("Предмет подобран и удален со сцены!");
    }
}