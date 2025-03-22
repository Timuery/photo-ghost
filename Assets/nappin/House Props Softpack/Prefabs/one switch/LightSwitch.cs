using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light lightSource; 
    public KeyCode interactKey = KeyCode.E; 
    public float pressAngle = 15f; 
    public float pressSpeed = 5f; 

    private bool isPlayerNear = false; 
    private bool isPressed = false; 
    private Quaternion initialRotation; 
    private Quaternion targetRotation; 

    private void Start()
    {
        
        initialRotation = transform.rotation;
        
        targetRotation = initialRotation * Quaternion.Euler(pressAngle, 0, 0);
    }

    private void Update()
    {
        
        if (isPlayerNear && Input.GetKeyDown(interactKey))
        {
            ToggleLight(); 
            ToggleButton(); 
        }

        // Плавно вращаем кнопку в нужное состояние
        if (isPressed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * pressSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * pressSpeed);
        }
    }

    private void ToggleLight()
    {
        
        lightSource.enabled = !lightSource.enabled;
    }

    private void ToggleButton()
    {
        
        isPressed = !isPressed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}