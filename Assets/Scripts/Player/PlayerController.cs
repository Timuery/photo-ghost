using System;
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
    [SerializeField]private Camera playerCamera;

    [Header("MainComponent")]
    [HideInInspector] public SceneController _mainController;
    [HideInInspector] public EffectController effectController;
    Rigidbody rb;

    [Header("Effects")]
    public float runningSpeedMultiplier = 1.5f;
    public float stunDuration = 2f;

    

    public void Start()
    {
        effectController = new EffectController(this);
        playerBody = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
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
    void Movement()
    {

        ApplyEffect(PlayerEffect.Stunning);
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (move.magnitude > 1f)
            move.Normalize();

        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) // Бег при зажатом Shift
        {
            currentSpeed *= runningSpeedMultiplier;
        }

        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
    }
    void Looking()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // Убрать Time.deltaTime
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // Убрать Time.deltaTime

        // Вертикальное вращение камеры
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    void CameraChecker()
    {
        // Создаем луч из центра камеры
        Ray ray = playerCamera.
            ScreenPointToRay(new Vector3
            (Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

            // Проверяем, попадает ли луч на объект
            if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (findObject != hit.transform.gameObject)
            {
                findObject = hit.transform.gameObject;
                if (hit.collider.CompareTag("Useble"))
                {
                    _mainController.UIcontroller.ActiveUsePanel();
                }
                if (hit.transform.gameObject.layer == 6)
                {
                    // Выполняется метод проверки при нажатии
                    ArmController(hit.transform.gameObject);
                }
            }
        }
        ArmController();
    }

    void Keys()
    {
        if (Input.GetButtonDown("Use") && findObject.CompareTag("Door"))
        {
            findObject.GetComponent<DoorScript.Door>().OpenDoor();
        }
    }
    void ArmController(GameObject _item = null)
    {
        // ЛКМ
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
                _objectOnArm.transform.SetParent(null); // Отменяем родительский объект
                _objectOnArm = null;
            }

            _mainController._ItemManager.MoveItem(arm);
        }

    }
}
public class EffectController
{
    private PlayerEffect activeEffects;
    private PlayerController player;
    private float originalSpeed;

    public EffectController(PlayerController player)
    {
        this.player = player;
        originalSpeed = player.moveSpeed;
    }

    public void AddEffect(PlayerEffect effect)
    {
        activeEffects |= effect;
        HandleEffectStart(effect);
    }

    public void RemoveEffect(PlayerEffect effect)
    {
        activeEffects &= ~effect;
        HandleEffectEnd(effect);
    }

    public bool HasEffect(PlayerEffect effect)
    {
        return (activeEffects & effect) == effect;
    }

    public void UpdateEffects()
    {
        if (HasEffect(PlayerEffect.Running))
            player.moveSpeed = originalSpeed * player.runningSpeedMultiplier;
        else
            player.moveSpeed = originalSpeed;
        if (HasEffect(PlayerEffect.Stunning))
            player.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        if (HasEffect(PlayerEffect.Hit))
            HandleHitEffect();
    }

    private void HandleEffectStart(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Stunning:
                player.StartCoroutine(StunCoroutine());
                break;
            case PlayerEffect.Hit:
                break;
        }
    }

    private void HandleEffectEnd(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Hit:
                break;
        }
    }

    private System.Collections.IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(player.stunDuration);
        RemoveEffect(PlayerEffect.Stunning);
    }

    private void HandleHitEffect()
    {
        // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    }
}

[Flags]
public enum PlayerEffect
{
    None = 0,
    Running = 1 << 0,   // 1
    Stunning = 1 << 1,  // 2
    Hit = 1 << 2        // 4
}