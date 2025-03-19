using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Photographer : MonoBehaviour
{

    [SerializeField] private RenderTexture ghostRT;
    [SerializeField] private UIPhoto uiPhotoPrefab;
    [SerializeField] private Transform uiPhotosRoot;

    [SerializeField] private Image latestPhotoDisplay;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private PhotoCollectionManager photoCollectionManager;

    private bool isActive = false;

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                CaptureRenderTexture(ghostRT);
                audioSource.Play();
            }
        }

            if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log('п');
            photoCollectionManager.TogglePanel();
        }


    
    }

    public void CameraOpen(bool state)
    {
        isActive = state;

    }



    public void CaptureRenderTexture(RenderTexture renderTexture)
    {
        Texture2D texture = SaveRenderTextureToTexture2D(renderTexture);
        Sprite sprite = ConvertTextureToSprite(texture);

        if (sprite != null)
        {
            MakePhoto(sprite);
        }
    }

    private void MakePhoto(Sprite image)
    {
        UIPhoto photo = Instantiate(uiPhotoPrefab, uiPhotosRoot);
        photo.Init(image);

        // Обновляем отображение последней фотографии
        if (latestPhotoDisplay != null)
        {
            latestPhotoDisplay.sprite = image;
            latestPhotoDisplay.enabled = true; // Показываем Image, если он скрыт
        }
    }
    
    
    public static Texture2D SaveRenderTextureToTexture2D(RenderTexture renderTexture)
    {
        // Создаем новую текстуру
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

        // Сохраняем текущий активный RenderTexture
        RenderTexture currentActiveRT = RenderTexture.active;
    
        // Устанавливаем наш RenderTexture активным
        RenderTexture.active = renderTexture;

        // Копируем пиксели из RenderTexture в Texture2D
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Возвращаем старый активный RenderTexture
        RenderTexture.active = currentActiveRT;

        return texture;
    }
    
    public static Sprite ConvertTextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
