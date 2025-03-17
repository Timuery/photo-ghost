using UnityEngine;
using UnityEngine.UI;

public class PhotoSystem : MonoBehaviour
{
    [Header("Main Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("UI Elements")]
    [SerializeField] private CanvasGroup photoUI;
    [SerializeField] private RawImage photoFrame;

    [Header("Photo Settings")]
    [SerializeField] private RenderTexture photoTexture;
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private float photoDistance = 2f;

    private bool isPhotoMode;
    private Texture2D currentPhoto;

    private void Start()
    {
        SetPhotoMode(false);
    }

    private void Update()
    {
        // Активация/деактивация фоторежима
        if (Input.GetButtonDown("Photo"))
        {
            TogglePhotoMode();
        }

        // Создание фото при активном режиме
        if (isPhotoMode && Input.GetMouseButtonDown(0))
        {
            CreatePhoto();
        }
    }

    private void TogglePhotoMode()
    {
        isPhotoMode = !isPhotoMode;
        SetPhotoMode(isPhotoMode);
    }

    private void SetPhotoMode(bool state)
    {
        photoUI.alpha = state ? 1 : 0;
        photoUI.blocksRaycasts = state;
        photoUI.interactable = state;

        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    public void CreatePhoto()
    {
        StartCoroutine(CapturePhoto());
    }

    private System.Collections.IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();

        currentPhoto = new Texture2D(
            photoTexture.width,
            photoTexture.height,
            TextureFormat.RGB24,
            false
        );

        RenderTexture.active = photoTexture;
        currentPhoto.ReadPixels(new Rect(0, 0, photoTexture.width, photoTexture.height), 0, 0);
        currentPhoto.Apply();
        RenderTexture.active = null;

        InstantiatePhoto();
        photoFrame.texture = currentPhoto;
    }

    private void InstantiatePhoto()
    {
        Vector3 spawnPos = mainCamera.transform.position +
                         mainCamera.transform.forward * photoDistance;

        GameObject newPhoto = Instantiate(
            photoPrefab,
            spawnPos,
            Quaternion.LookRotation(-mainCamera.transform.forward)
        );

        MeshRenderer renderer = newPhoto.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = currentPhoto;
        }
    }
}