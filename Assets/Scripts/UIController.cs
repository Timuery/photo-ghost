using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController Instance {  get; private set; }



    [SerializeField] float fadeTo = 1;
    [SerializeField] Image _prefabUsePanel;

    [SerializeField] int nowImages = 0;
    [SerializeField] int maxImages = 5;


    [SerializeField] Sprite _Eimage;
    [SerializeField] Sprite _Fimage;

    [SerializeField]
    private TextMeshProUGUI _textToChange;
    [SerializeField] private TextMeshProUGUI uiTextPanel;
    [SerializeField] private Image _image;


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
        if (Instance == null) Instance = this;
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
            Color color = uiTextPanel.color;
            color.a = 255f;
            uiTextPanel.color = color;

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

    public void VisibleImage(Sprite sprite)
    {
        _image.sprite = sprite;
        _image.gameObject.SetActive(true);
        StartCoroutine(timeToClose());
    }
    private IEnumerator timeToClose()
    {
        yield return new WaitForSeconds(3);
        _image.sprite = null;
        _image.gameObject.SetActive(false);
    }

}
