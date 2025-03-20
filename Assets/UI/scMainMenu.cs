using UnityEngine;
using UnityEngine.SceneManagement;


public class scMainMenu : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clickSound;
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(0);
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
}
