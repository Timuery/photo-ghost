using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 20f;
    [SerializeField] private float maxZoom = 60f;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float nowZoom = 60f;

    public void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void Zoom (float axis)
    {
        if (axis != 0)
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - axis * zoomSpeed, minZoom, maxZoom);
        }
    }


}
