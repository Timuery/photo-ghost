using UnityEngine;
using System.IO;

public class screen1 : MonoBehaviour
{
    public Camera screenshotCamera; // ������, � ������� ������ ��������
    public int width = 2480;       // ������ �����������
    public int height = 3500;      // ������ �����������

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        // ������� RenderTexture � ������ �����������
        RenderTexture rt = new RenderTexture(width, height, 24);
        screenshotCamera.targetTexture = rt; // ��������� ������
        screenshotCamera.Render();           // �������� ����

        // ������� Texture2D � ������ ������� �� RenderTexture
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        // ��������� � ����
        byte[] bytes = screenshot.EncodeToPNG();
        string filename = $"Screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
        File.WriteAllBytes(Application.dataPath + "/" + filename, bytes);

        // ������� �������
        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        Debug.Log($"�������� ��������: {filename}");
    }
}
