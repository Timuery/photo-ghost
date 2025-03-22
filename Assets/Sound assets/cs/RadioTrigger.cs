using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    public AudioClip radioSound; 
    private AudioSource audioSource; 

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Назначаем аудиоклип
        audioSource.clip = radioSound;
        audioSource.playOnAwake = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что в зону триггера вошел игрок
        if (other.CompareTag("Player"))
        {
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что игрок вышел из зоны триггера
        if (other.CompareTag("Player"))
        {
            // Выключаем звук радио
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}