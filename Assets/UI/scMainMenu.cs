using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scMainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public GameObject blackScreen; // перетяни объект "Black" сюда через инспектор

    public void PlayGame()
    {
        StartCoroutine(PlayIntroAndLoadGame());
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    private IEnumerator PlayIntroAndLoadGame()
    {
        // Включаем чёрный экран
        blackScreen.SetActive(true);

        // Проигрываем звук
        audioSource.PlayOneShot(clickSound);

        // Ждём окончания звука
        yield return new WaitForSeconds(clickSound.length);

        // Загружаем сцену
        SceneManager.LoadScene("MotelFinal");
    }
}

