using UnityEngine;
using UnityEngine.UI;

public class PhotoSystem : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera photoCamera; // Камера фотоаппарата
    [SerializeField] private Camera mainCamera; // Основная камера
    [SerializeField] private RenderTexture photoRenderTexture; // Рендер-текстура для фото

    [Header("UI Settings")]
    [SerializeField] private Canvas photoUICanvas; // UI фотоаппарата
    [SerializeField] private RawImage photoPreview; // Превью фотографии

    [Header("Photo Object")]
    [SerializeField] private GameObject photoPaperPrefab; // Префаб "бумаги" для фото

    private bool _isActive;
    private Texture2D _currentPhoto;

    private void Start()
    {
        // Инициализация состояний
        SetCameraState(false);
        photoUICanvas.gameObject.SetActive(false);
    }

    public void ChangeActive(bool active = true)
    {
        if (active)
        {
            _isActive = !_isActive;
        }
        else
        {
            _isActive = false;
        }
        SetCameraState(_isActive);

        // Переключение UI
        photoUICanvas.gameObject.SetActive(_isActive);

        // Синхронизация с основной камерой
        if (_isActive) SyncCameraPosition();

            
    }

    private void SetCameraState(bool state)
    {
        photoCamera.gameObject.SetActive(state);
        mainCamera.gameObject.SetActive(!state);
    }

    public void CreatePhoto()
    {
        if (!_isActive) return;

        // Создание текстуры из рендер-текстуры
        _currentPhoto = new Texture2D(
            photoRenderTexture.width,
            photoRenderTexture.height,
            TextureFormat.RGB24,
            false
        );

        RenderTexture.active = photoRenderTexture;
        _currentPhoto.ReadPixels(new Rect(0, 0,
            photoRenderTexture.width,
            photoRenderTexture.height), 0, 0);
        _currentPhoto.Apply();
        RenderTexture.active = null;

        // Создание объекта "бумага"
        CreatePhotoPaper();

        // Обновление превью
        photoPreview.texture = _currentPhoto;
    }

    private void CreatePhotoPaper()
    {
        var paper = Instantiate(photoPaperPrefab,
            transform.position + transform.forward * 2,
            Quaternion.identity);

        var renderer = paper.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = _currentPhoto;
        }
    }

    private void SyncCameraPosition()
    {
        // Синхронизация позиции с основной камерой
        photoCamera.transform.SetPositionAndRotation(
            mainCamera.transform.position,
            mainCamera.transform.rotation
        );
    }

    // Дополнительные методы
    public void AdjustFOV(float value) => photoCamera.fieldOfView = value;
    public void ToggleFilter() { /* Реализация фильтров */ }
}