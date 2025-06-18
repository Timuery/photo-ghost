using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] float fadeTo = 1;
    [SerializeField] Image _prefabUsePanel;

    [SerializeField] int nowImages = 0;
    [SerializeField] int maxImages = 5;


    [SerializeField] Sprite _Eimage;
    [SerializeField] Sprite _Fimage;

    [SerializeField]
    private TextMeshProUGUI _textToChange;
    [SerializeField] private TextMeshProUGUI uiTextPanel;


    [SerializeField] bool ACTIVE = false;

    private Coroutine _fadeCoroutine;
    private Coroutine textCoroutine;
    [SerializeField] private Photographer photoController;


    private void Start()
    {
        try
        {
            _prefabUsePanel =
            GameObject.Find("UsePanel").
            GetComponent<Image>();
        }
        catch
        {
           
        }
    }
    public void ActiveUsePanel(string type)
    {
        if (type == "Photo") _prefabUsePanel.sprite = _Fimage;
        else _prefabUsePanel.sprite = _Eimage;
        Color color = _prefabUsePanel.color;
        color.a = 1f;
        _prefabUsePanel.color = color;

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(WaitAndFadeOut(fadeTo, fadeTo));
    }
    public void TextPanel(int index)
    {
        if (TextController.Instance.GetString(index) != null)
        {
            // Получаем структуру время/текст
            Texts text = TextController.Instance.GetString(index);
            uiTextPanel.text = text.text;

            if (textCoroutine != null)
            {
                StopCoroutine(textCoroutine);
            }
            textCoroutine = StartCoroutine(FadeOutText(fadeTo, text.time));

        }  
    }
    private IEnumerator WaitAndFadeOut(float delay, float timeWords=2)
    {
        yield return new WaitForSeconds(delay);

        Color color = _prefabUsePanel.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < timeWords)
        {
            time += Time.deltaTime;
            float t = time / timeWords;
            color.a = Mathf.Lerp(startAlpha, 0, t);
            _prefabUsePanel.color = color;
            yield return null;
        }

        color.a = 0;
        _prefabUsePanel.color = color;
    }

    private IEnumerator FadeOutText(float delay, float timeWords=2)
    {
        yield return new WaitForSeconds(timeWords);

        Color color = uiTextPanel.color;
        float time = 0f;
        while (time < delay)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, time / delay);
            uiTextPanel.color = color;
            yield return null;
        }
    }
    private void ChangeCountPhotos()
    {
        _textToChange.text = $"{nowImages}/{maxImages}";
    }
    public void AddCountPhotos()
    {
        ++nowImages;
        ChangeCountPhotos();
    }
}
