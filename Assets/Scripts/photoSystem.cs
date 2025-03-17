using UnityEngine;
using UnityEngine.UI;

public class PhotoSystem : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera photoCamera; // ������ ������������
    [SerializeField] private Camera mainCamera; // �������� ������
    [SerializeField] private RenderTexture photoRenderTexture; // ������-�������� ��� ����

    [Header("UI Settings")]
    [SerializeField] private Canvas photoUICanvas; // UI ������������
    [SerializeField] private RawImage photoPreview; // ������ ����������

    [Header("Photo Object")]
    [SerializeField] private GameObject photoPaperPrefab; // ������ "������" ��� ����

    private bool _isActive;
    private Texture2D _currentPhoto;

    private void Start()
    {
        // ������������� ���������
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

        // ������������ UI
        photoUICanvas.gameObject.SetActive(_isActive);

        // ������������� � �������� �������
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

        // �������� �������� �� ������-��������
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

        // �������� ������� "������"
        CreatePhotoPaper();

        // ���������� ������
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
        // ������������� ������� � �������� �������
        photoCamera.transform.SetPositionAndRotation(
            mainCamera.transform.position,
            mainCamera.transform.rotation
        );
    }

    // �������������� ������
    public void AdjustFOV(float value) => photoCamera.fieldOfView = value;
    public void ToggleFilter() { /* ���������� �������� */ }
}