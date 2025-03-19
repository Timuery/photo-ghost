using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] float fadeTo = 1;
    public float fadeDuration = 2f;
    [SerializeField] Image _prefabUsePanel;
    Coroutine _fadeCoroutine;
    [SerializeField] private Photographer photoController;

    private void Start()
    {
        _prefabUsePanel = 
            GameObject.Find("Image").
            GetComponent<Image>();
    }
    public void ActiveUsePanel()
    {
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

    public void ToggleCameraUI(bool state)
    {
        photoController.CameraOpen(state);
    }

    


}
