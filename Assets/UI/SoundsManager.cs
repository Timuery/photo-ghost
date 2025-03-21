using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;  // Источник звука
    public AudioClip[] sounds;       // Массив звуков (5 штук)
    public float minInterval = 10f;  // Минимальный интервал (можно менять в Inspector)
    public float maxInterval = 30f;  // Максимальный интервал (можно менять в Inspector)

    void Start()
    {
        StartCoroutine(PlayRandomSound());
    }

    IEnumerator PlayRandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval)); // Ждём случайное время

            if (sounds.Length > 0)
            {
                int randomIndex = Random.Range(0, sounds.Length); // Выбираем случайный звук
                audioSource.PlayOneShot(sounds[randomIndex]); // Проигрываем звук
            }
        }
    }
}
