using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(Rigidbody), typeof(Renderer))] 
public class PlayerController : MonoBehaviour
{
    private static readonly HashSet<string> InteractableTags = new HashSet<string> { "Useble", "Door", "Drawer" };

    public LayerMask layersToRender;


    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 1f;
    public float runningSpeedMultiplier = 1.5f;
    public float gravity = -9.81f;
    private float curSpeed; bool run = false;


    private float xRotation = 0f;
    public float interactionDistance = 5f;
    public float takeDistance = 10f;
    private GameObject findObject;
    private GameObject _objectOnArm;
    private Vector3 direct;

    [Header("Body Parts")]
    [SerializeField] private Transform arm;
    private Transform playerBody;
    
    
    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] public GameObject PhotoCamera;

    [Header("MainComponent")]
    [HideInInspector] public SceneController _mainController;
    [HideInInspector] public EffectController effectController;
    
    ItemManager itemManager;

    Rigidbody rb;

    public Photographer photographer;

    [Header("Lights")]
    public Light photoLight;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        itemManager = GetComponent<ItemManager>();
        Physics.autoSyncTransforms = true;


        effectController = GetComponent<EffectController>();
        playerBody = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;

        if (photographer == null) 
            photographer = GameObject.Find("PhotoMaker").
                GetComponent<Photographer>();
    }
    public void Update()
    {
        Looking();
        Keys();
        CameraChecker();
        ArmController();
    }
    private void FixedUpdate()
    {
        if ((int)effectController.GetEffect() <= (int)PlayerEffect.Stunning)
        {
            Movement();
        }
       
    }
    void Movement()
    {
        if (direct.magnitude > 1f)
        {
            direct.Normalize();
        }
        curSpeed = Input.GetButton("Run") ? moveSpeed * runningSpeedMultiplier : moveSpeed;


        // Установка скорости только по горизонтальным осям (X и Z)
        rb.linearVelocity = new Vector3(
            direct.x * curSpeed,
            rb.linearVelocity.y,  // Сохраняем текущую вертикальную скорость (гравитация)
            direct.z * curSpeed
        );


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
        if (Physics.Raycast(ray, out hit, interactionDistance, layersToRender))
        {
            if (effectController.GetEffect() != PlayerEffect.Photo)
            {
                findObject = hit.transform.gameObject;

                if (hit.collider.CompareTag("LevelPhotoToFind"))
                {
                    _mainController.UIcontroller.ActiveUsePanel("Photo");
                }
                else
                {
                    _mainController.UIcontroller.ActiveUsePanel("Use");
                } 
            }
        }
        else findObject = null;

        if (Physics.Raycast(ray, out hit, takeDistance))
        {
            if (hit.transform.gameObject.layer == 6)
            {
                ArmController(hit.transform.gameObject);
            }
        }
    }
    void Keys()
    {
        
        // Получаем ввод
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direct = transform.right * horizontal + transform.forward * vertical;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (effectController.GetEffect() == PlayerEffect.Photo)
            {
                effectController.RemoveEffect(PlayerEffect.Photo);
                return;
            }    
            effectController.AddEffect(PlayerEffect.Photo);
            
        }
        if (effectController.GetEffect() != PlayerEffect.Photo)
        {
            if (Input.GetButtonDown("Use") && findObject != null)
            {
                findObject.GetComponent<ScriptToUse>().Toggle();
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
                itemManager.RigidbodyState(false, _objectOnArm);
            }
        }

        if (_objectOnArm != null)
        {
            
            if (Input.GetMouseButtonUp(0))
            {
                itemManager.RigidbodyState(true, _objectOnArm);
                _objectOnArm.transform.SetParent(null); // �������� ������������ ������
                _objectOnArm = null;
            }

            itemManager.MoveItem(arm);
        }

    }

    public IEnumerator PhotoCoroutine()
    {
        photoLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        photoLight.enabled = false;
    }
}
