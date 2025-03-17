using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] float fadeTo = 1;
    public float fadeDuration = 2f;
    [SerializeField] Image _prefabUsePanel;
    Coroutine _fadeCoroutine;

    private void Start()
    {
        _prefabUsePanel = 
            GameObject.Find("UsePanel").
            GetComponent<Image>();
    }
    public void ActiveUsePanel()
    {
        ActivatePanel(fadeTo, _prefabUsePanel);
    }


    public void ActivatePanel(float delay, Image p)
    {

        _prefabUsePanel = p;
        // Устанавливаем альфа-канал в 1 (полная непрозрачность)
        Color color = _prefabUsePanel.color;
        color.a = 1f; // Устанавливаем альфа-канал в 1 (0-1)
        _prefabUsePanel.color = color;

        // Если корутина уже выполняется, останавливаем её
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        // Запускаем корутину, которая ждет перед затуханием
        _fadeCoroutine = StartCoroutine(WaitAndFadeOut(delay));
    }
    private IEnumerator WaitAndFadeOut(float delay)
    {
        // Ждем заданное время
        yield return new WaitForSeconds(delay);

        // Запускаем затухание
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        Color color = _prefabUsePanel.color;
        float startAlpha = color.a; // Начальный альфа-канал 
        // Время, за которое альфа-канал станет 0
        float time = 0f; // Время, прошедшее с начала

        while (time < fadeDuration)
        {
            time += Time.deltaTime; // Увеличиваем время
            float t = time / fadeDuration; // Нормализуем время от 0 до 1
            color.a = Mathf.Lerp(startAlpha, 0, t); // Линейная интерполяция альфа-канала
            _prefabUsePanel.color = color; // Применяем измененный цвет
            yield return null; // Ждем следующего кадра
        }

        // Убедимся, что альфа-канал точно равен 0 в конце
        color.a = 0;
        _prefabUsePanel.color = color;
    }


}
