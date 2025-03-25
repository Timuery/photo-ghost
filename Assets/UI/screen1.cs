using UnityEngine;
using System.IO;

public class screen1 : MonoBehaviour
{
    public Camera screenshotCamera; // Камера, с которой делаем скриншот
    public int width = 2480;       // Ширина изображения
    public int height = 3500;      // Высота изображения

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        // Создаем RenderTexture с нужным разрешением
        RenderTexture rt = new RenderTexture(width, height, 24);
        screenshotCamera.targetTexture = rt; // Назначаем камере
        screenshotCamera.Render();           // Рендерим кадр

        // Создаем Texture2D и читаем пиксели из RenderTexture
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        // Сохраняем в файл
        byte[] bytes = screenshot.EncodeToPNG();
        string filename = $"Screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
        File.WriteAllBytes(Application.dataPath + "/" + filename, bytes);

        // Очищаем ресурсы
        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        Debug.Log($"Скриншот сохранен: {filename}");
    }
}
