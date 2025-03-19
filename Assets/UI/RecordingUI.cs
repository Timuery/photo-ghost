using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class RecordingUI : MonoBehaviour
{
    [SerializeField] private Image redDot;
    private bool isBlinking = true;

    void Start()
    {
        StartCoroutine(BlinkAlpha());
    }

    IEnumerator BlinkAlpha()
    {
        while (isBlinking)
        {
            for (float alpha = 1; alpha >= 0; alpha -= 0.5f)
            {
                SetAlpha(alpha);
                yield return new WaitForSeconds(0.5f);
            }

            for (float alpha = 0; alpha <= 1; alpha += 0.5f)
            {
                SetAlpha(alpha);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = redDot.color;
        color.a = alpha;
        redDot.color = color;
    }
}
