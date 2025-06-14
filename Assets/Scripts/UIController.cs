using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] float fadeTo = 1;
    public float fadeDuration = 2f;
    [SerializeField] Image _prefabUsePanel;

    [SerializeField] int nowImages = 0;
    [SerializeField] int maxImages = 5;


    [SerializeField] Sprite _Eimage;
    [SerializeField] Sprite _Fimage;

    [SerializeField]
    private TextMeshProUGUI _textToChange;


    Coroutine _fadeCoroutine;
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

        ActivatePanel(fadeTo, _prefabUsePanel);
    }
    private void ActivatePanel(float delay, Image p)
    {

        _prefabUsePanel = p;
        // ������������� �����-����� � 1 (������ ��������������)
        Color color = _prefabUsePanel.color;
        color.a = 1f; // ������������� �����-����� � 1 (0-1)
        _prefabUsePanel.color = color;

        // ���� �������� ��� �����������, ������������� �
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        // ��������� ��������, ������� ���� ����� ����������
        _fadeCoroutine = StartCoroutine(WaitAndFadeOut(delay));
    }
    private IEnumerator WaitAndFadeOut(float delay)
    {
        // ���� �������� �����
        yield return new WaitForSeconds(delay);

        // ��������� ���������
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        Color color = _prefabUsePanel.color;
        float startAlpha = color.a; // ��������� �����-����� 
        // �����, �� ������� �����-����� ������ 0
        float time = 0f; // �����, ��������� � ������

        while (time < fadeDuration)
        {
            time += Time.deltaTime; // ����������� �����
            float t = time / fadeDuration; // ����������� ����� �� 0 �� 1
            color.a = Mathf.Lerp(startAlpha, 0, t); // �������� ������������ �����-������
            _prefabUsePanel.color = color; // ��������� ���������� ����
            yield return null; // ���� ���������� �����
        }

        // ��������, ��� �����-����� ����� ����� 0 � �����
        color.a = 0;
        _prefabUsePanel.color = color;
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
