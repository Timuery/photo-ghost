using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Renderer))] 
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f; 
    
    private float xRotation = 0f;
    public float interactionDistance = 5f;
    private GameObject findObject;
    private GameObject _objectOnArm;

    [Header("Body Parts")]
    [SerializeField] private Transform arm;
    private Transform playerBody;
    
    
    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] public GameObject PhotoCamera;

    [Header("MainComponent")]
    [HideInInspector] public SceneController _mainController;
    [HideInInspector] public EffectController effectController;
    Rigidbody rb;
    public Photographer photographer;

    [Header("Effects")]
    public float runningSpeedMultiplier = 1.5f;
    public float stunDuration = 2f;
    [Header("Lights")]
    public Light photoLight;
    

    

    public void Start()
    {
        effectController = new EffectController(this, GetComponent<AudioSource>());
        playerBody = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        if (photographer == null) 
            photographer = GameObject.Find("PhotoMaker").
                GetComponent<Photographer>();
    }
    public void Update()
    {
        effectController.UpdateEffects();
        //Movement();
        Looking();
        CameraChecker();
        Keys();

    }
    public void FixedUpdate()
    {
        if (!effectController.HasEffect(PlayerEffect.Stunning))
            Movement();
    }

    public void ApplyEffect(PlayerEffect effect)
    {
        effectController.AddEffect(effect);
        
    }
    public void RemoveEffect(PlayerEffect effect)
    {
        effectController.RemoveEffect(effect);
    }
    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        
        if (move.magnitude > 1f)
            move.Normalize();

        float currentSpeed = moveSpeed;
        if (Input.GetButtonDown("Run")) // ��� ��� ������� Shift
        {
            ApplyEffect(PlayerEffect.Running);
        }
        else if (Input.GetButtonUp("Run"))
        {
            RemoveEffect(PlayerEffect.Running);
        }

        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
    }
    void Looking()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // ������ Time.deltaTime
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // ������ Time.deltaTime

        // ������������ �������� ������
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    void CameraChecker()
    {
        // ������� ��� �� ������ ������
        Ray ray = playerCamera.
            ScreenPointToRay(new Vector3
            (Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

            // ���������, �������� �� ��� �� ������
            if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            findObject = hit.transform.gameObject;
            if (hit.collider.CompareTag("Useble"))
            {
                Debug.Log("Find10");
                _mainController.UIcontroller.ActiveUsePanel();
            }
            if (hit.transform.gameObject.layer == 6)
            {

                ArmController(hit.transform.gameObject);
            }
        }
        ArmController();
    }
    void Keys()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("bb");
            ApplyEffect(PlayerEffect.Photo);
            
        }

         if (findObject.CompareTag("Door"))

        {
            findObject.GetComponent<DoorScript.Door>().OpenDoor();
        }
        else if (findObject.CompareTag("Drawer"))
        
        {
            findObject.GetComponent<DoorScript.Drawer>().ToggleDrawer();
        }

     
    }
    void ArmController(GameObject _item = null)
    {
        // ���
        if (_item != null && _objectOnArm == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _objectOnArm = _item;
                _objectOnArm.transform.SetParent(gameObject.transform);
                _mainController._ItemManager.RigidbodyState(false, _objectOnArm);
            }
        }

        if (_objectOnArm != null)
        {
            
            if (Input.GetMouseButtonUp(0))
            {
                _mainController._ItemManager.RigidbodyState(true, _objectOnArm);
                _objectOnArm.transform.SetParent(null); // �������� ������������ ������
                _objectOnArm = null;
            }

            _mainController._ItemManager.MoveItem(arm);
        }

    }

    public System.Collections.IEnumerator PhotoCoroutine()
    {
        photoLight.enabled = true;
        yield return new WaitForSeconds(1f);
        photoLight.enabled = false;
    }
}
