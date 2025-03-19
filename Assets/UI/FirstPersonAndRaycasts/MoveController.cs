using System;
using UnityEngine;

    public abstract class MoveController : MonoBehaviour
    {
        [SerializeField] [Range(1f, 300f)] protected float mouseSensetivityX;
        [SerializeField] [Range(1f, 300f)] protected float mouseSensetivityY;
        protected float RotationAxe;

        [SerializeField] [Range(0.2f, 50f)] private float walkSpeed = 1f;
        [SerializeField] [Range(1f, 100f)] private float runSpeed = 10f;


    [SerializeField] private Transform feet;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravityScale = 9.8f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private LayerMask groundSurface;

    private const string SIDE = "Horizontal";
    private const string FORWARD = "Vertical";
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";
    private const string JUMP = "Jump";


    private CharacterController controller;
    private Vector3 moveDirection;
    private Vector3 gravitation;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public virtual void OnPlayerActivate(bool isActive) {}

    public virtual void UpdateController()
    {

        isGrounded = Physics.CheckSphere(feet.position, 0.5f, groundSurface);
        if (isGrounded && gravitation.y < 0) gravitation.y = -1f; 
      

        Jump();
        MouseLook();
        Move();
        
    }


    private void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown(JUMP))
                gravitation.y = Mathf.Sqrt(jumpHeight * gravityScale);
        }
        else
        {
            gravitation.y -= gravityScale * Time.deltaTime;
           
        }

        controller.Move(gravitation * Time.deltaTime);
    }


    protected virtual void MouseLook()
    {
        float bodyRotation = Input.GetAxis(MOUSE_X);
        RotationAxe = Input.GetAxis(MOUSE_Y);
        transform.Rotate(Vector3.up * bodyRotation * mouseSensetivityX * Time.deltaTime);
    }


    private void Move()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float side = Input.GetAxis(SIDE);
        float forward = Input.GetAxis(FORWARD);

        moveDirection = transform.right * side + transform.forward * forward;
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }



    }
