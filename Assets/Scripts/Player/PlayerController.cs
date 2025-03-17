using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Renderer))] // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ Renderer
public class PlayerController : MonoBehaviour
{

<<<<<<< HEAD
    public float moveSpeed = 5f; // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    public float mouseSensitivity = 100f; // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ
=======
    public float moveSpeed = 2f; // Скорость передвижения
    public float mouseSensitivity = 100f; // Чувствительность мыши
>>>>>>> 643ff71e2145c8dc5345994a40ae966dab65c948

    private Transform playerBody;
    private float xRotation = 0f;
    EffectController effectController;
    Rigidbody rb;

    [SerializeField]private GameObject playerCamera;
    [Header("Effects")]
    public float runningSpeedMultiplier = 1.5f;
    public float stunDuration = 2f;
    public Material hitMaterial; // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ


    private Coroutine stunCoroutine;
    /// <summary>
    /// пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅ UI
    /// </summary>
    public Renderer screenRenderer;
    public PhotoSystem photoSystem;

    public void Start()
    {
        effectController = new EffectController(this);
        playerBody = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;


        rb = GetComponent<Rigidbody>();
        screenRenderer = GetComponent<Renderer>();
<<<<<<< HEAD
    }


    void KeyBounds()
    {
        if (Input.GetButtonDown("Photo"))
        {
            photoSystem.ChangeActive();
        }
        if (Input.GetButtonDown("РЎancel"))
        {
            photoSystem.ChangeActive(false);
        }
=======
        //rb.interpolation = RigidbodyInterpolation.Interpolate;
        
>>>>>>> 643ff71e2145c8dc5345994a40ae966dab65c948
    }
    public void Update()
    {
        effectController.UpdateEffects();
        //Movement();
        Looking();
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
<<<<<<< HEAD
        float moveX = Input.GetAxis("Horizontal"); // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ (A/D пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ/пїЅпїЅпїЅпїЅпїЅпїЅ)
        float moveZ = Input.GetAxis("Vertical"); // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ (W/S пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ/пїЅпїЅпїЅпїЅ)

        Vector3 move = transform.right * moveX + transform.forward * moveZ; // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
        playerBody.position += move * moveSpeed * Time.deltaTime; // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
=======
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
>>>>>>> 643ff71e2145c8dc5345994a40ae966dab65c948
    }

    void Looking()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // Убрать Time.deltaTime
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // Убрать Time.deltaTime

        // Вертикальное вращение камеры
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

<<<<<<< HEAD
        // пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
        Camera.main.transform.localRotation = Quaternion.Slerp(
            Camera.main.transform.localRotation,
            Quaternion.Euler(xRotation, 0f, 0f),
            Time.deltaTime * 10f // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
        );

        // пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
        playerBody.Rotate(Vector3.up * mouseX);
=======
        // Горизонтальное вращение игрока
        transform.Rotate(Vector3.up * mouseX); // Простое вращение без физики
>>>>>>> 643ff71e2145c8dc5345994a40ae966dab65c948
    }
}

class EffectController
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
        Debug.Log("1");
        if (HasEffect(PlayerEffect.Running))
            player.moveSpeed = originalSpeed * player.runningSpeedMultiplier;
        else
            player.moveSpeed = originalSpeed;
        Debug.Log("2");
        if (HasEffect(PlayerEffect.Stunning))
            player.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        Debug.Log("3");
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
                player.screenRenderer.material = player.hitMaterial;
                break;
        }
    }

    private void HandleEffectEnd(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Hit:
                player.screenRenderer.material = null;
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