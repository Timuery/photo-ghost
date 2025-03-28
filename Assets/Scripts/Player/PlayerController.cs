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
    public float takeDistance = 10f;
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
        Looking();
        Keys();
        CameraChecker();
        ArmController();


    }
    private void FixedUpdate()
    {
        if (effectController.activeEffects != PlayerEffect.Photo)
        {
            Movement();
        }
       
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
            if (effectController.activeEffects != PlayerEffect.Photo)
            {
                findObject = hit.transform.gameObject;
                if (hit.collider.CompareTag("Useble") || hit.collider.CompareTag("Door") || hit.collider.CompareTag("Drawer"))
                {
                    Debug.Log("Find10");
                    _mainController.UIcontroller.ActiveUsePanel("Use");
                }
                if (hit.collider.CompareTag("LevelPhotoToFind"))
                {
                    _mainController.UIcontroller.ActiveUsePanel("Photo");
                }
            }
        }
        if (Physics.Raycast(ray, out hit, takeDistance))
        {
            if (hit.transform.gameObject.layer == 6)
            {
                ArmController(hit.transform.gameObject);
            }
        }
        else
        {
            findObject = null;
        }
    }
    void Keys()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("bb");
            ApplyEffect(PlayerEffect.Photo);
            
        }
        if ((effectController.activeEffects != PlayerEffect.Photo))
        {
            if (Input.GetButtonDown("Use") && findObject != null)
            {

                if (findObject.CompareTag("Door"))

                {
                    findObject.GetComponent<DoorScript.Door>().OpenDoor();
                }
                else if (findObject.CompareTag("Drawer"))

                {
                    findObject.GetComponent<DoorScript.Drawer>().ToggleDrawer();
                }
                else 
                {
                    findObject.GetComponent<SriptToUse>().ToggleMode();
                }
            }
            if (Input.GetButtonDown("TakePhoto") && findObject != null)
            {
                if (findObject.CompareTag("LevelPhotoToFind"))
                {
                    findObject.GetComponent<ItemPickup>().PickupItem();
                    _mainController.UIcontroller.AddCountPhotos();
                }
            }
        }














        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _mainController.LoadScene("Menu");
            Cursor.lockState = CursorLockMode.None;
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

    public IEnumerator PhotoCoroutine()
    {
        photoLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        photoLight.enabled = false;
    }
}
