using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 20f;
    [SerializeField] private float maxZoom = 60f;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float nowZoom = 60f;

    public void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void Zoom (float axis)
    {
        if (axis != 0)
        {
            nowZoom -= axis * zoomSpeed;
            Debug.Log(nowZoom + " ZOOM");
            cam.fieldOfView -= nowZoom;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
    }


}
