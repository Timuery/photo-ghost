using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;  // Источник звука
    public AudioClip[] sounds;       // Массив звуков (5 штук)
    public float minInterval = 10f;  // Минимальный интервал (можно менять в Inspector)
    public float maxInterval = 30f;  // Максимальный интервал (можно менять в Inspector)

}
