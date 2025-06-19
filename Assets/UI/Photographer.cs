using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Photographer : MonoBehaviour
{
    public static Photographer Instance { get; private set; }

    [SerializeField] private RenderTexture ghostRT;
    [SerializeField] private UIPhoto uiPhotoPrefab;

    private Transform nowPhotoRoot;
    [SerializeField] private Transform ghostPhotoRoot;
    [SerializeField] private Transform allPhotoRoot;

    [SerializeField] private Image latestPhotoDisplay;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PhotoCollectionManager photoCollectionManager;
    [SerializeField] public Camera photoCamera;
    [SerializeField] private LayerMask obstacleLayer;

    private bool findBool;
    private bool isActive = false;

    PlayerController player;

    private void Start()
    {
        nowPhotoRoot = ghostPhotoRoot;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(Time());
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                nowPhotoRoot = (nowPhotoRoot == ghostPhotoRoot) ? allPhotoRoot : ghostPhotoRoot;
                photoCollectionManager.TogglePanel();
            }
        }
    }

    public IEnumerator Time()
    {
        StartCoroutine(player.PhotoCoroutine());

        yield return new WaitForSeconds(0.1f);
        CaptureRenderTexture(ghostRT);
        audioSource.Play();
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

    public bool CheckAndDestroyGhosts()
    {
        findBool = false;
        var ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        foreach (var ghost in ghosts)
        {
            if (ghost.layer != 7) continue;

            Vector3 viewportPos = photoCamera.WorldToViewportPoint(ghost.transform.position);
            bool inFrame = viewportPos.z > 0 &&
                           viewportPos.x >= 0 && viewportPos.x <= 1 &&
                           viewportPos.y >= 0 && viewportPos.y <= 1;

            if (!inFrame) continue;

            Ray ray = new Ray(photoCamera.transform.position, ghost.transform.position - photoCamera.transform.position);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, obstacleLayer | (1 << ghost.layer)))
            {
                if (hit.collider.gameObject == ghost)
                {
                    findBool = true;
                    break;
                }
            }
        }

        return findBool;
    }

    private void MakePhoto(Sprite image)
    {
        nowPhotoRoot = CheckAndDestroyGhosts() ? ghostPhotoRoot : allPhotoRoot;

        UIPhoto photo = Instantiate(uiPhotoPrefab, nowPhotoRoot);
        photo.Init(image);

        if (latestPhotoDisplay != null)
        {
            latestPhotoDisplay.sprite = image;
            latestPhotoDisplay.enabled = true;
        }
    }

    public static Texture2D SaveRenderTextureToTexture2D(RenderTexture renderTexture)
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = currentActiveRT;
        return texture;
    }

    public static Sprite ConvertTextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
