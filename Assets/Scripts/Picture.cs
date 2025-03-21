using System.Collections;
using UnityEngine;

public class Picture : MonoBehaviour
{
    public Transform cameraTransform; // Камера игрока
    public Transform picture; // Объект, который будет приближаться (Picture)
    public float transitionSpeed = 5f; // Скорость приближения
    public Vector3 offset = new Vector3(0, 0, 0.5f); // Расстояние до камеры

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isViewing = false;

    void Start()
    {
        if (picture != null)
        {
            originalPosition = picture.position;
            originalRotation = picture.rotation;
        }
    }

    public void ToggleView()
    {
        if (!isViewing)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToCamera());
        }
    }

    IEnumerator MoveToCamera()
    {
        isViewing = true;
        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * offset.z;
        Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward);

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            picture.position = Vector3.Lerp(picture.position, targetPosition, elapsedTime);
            picture.rotation = Quaternion.Slerp(picture.rotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        picture.position = targetPosition;
        picture.rotation = targetRotation;
    }

    public void MoveBack()
    {
        if (isViewing)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToOriginal());
        }
    }

    IEnumerator MoveToOriginal()
    {
        isViewing = false;
        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            picture.position = Vector3.Lerp(picture.position, originalPosition, elapsedTime);
            picture.rotation = Quaternion.Slerp(picture.rotation, originalRotation, elapsedTime);
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        picture.position = originalPosition;
        picture.rotation = originalRotation;
    }
}
