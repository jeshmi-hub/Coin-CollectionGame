
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
 
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;

    public bool IsGrounded => controller.isGrounded;

    public float Height
    {
        get => controller.height;
        set => controller.height = value;
    }
    internal float movementSpeedMultiplier;

    public event Action OnBeforeMove;
    public event Action<bool> OnGroundStateChange;

    Vector2 look;
    public Vector3 velocity;

    CharacterController controller;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
   
    InputAction sprintAction;
    bool wasGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        sprintAction = playerInput.actions["sprint"];

    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {
        UpdateGround();
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
       
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    Vector3 GetMovementInput()
    {
        var moveInput = moveAction.ReadValue<Vector2>();
        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        var sprintInput = sprintAction.ReadValue<float>();
       // var multiplier = sprintInput > 0 ? 1.5f : 1f;
        input *= movementSpeed * movementSpeedMultiplier;
        return input;

    }

    void UpdateMovement()
    {
        /*var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");*/
        movementSpeedMultiplier = 1f;
        OnBeforeMove?.Invoke();
        var input = GetMovementInput();


        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        //transform.Translate(input * movementSpeed * Time.deltaTime, Space.World);
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;
        look.y = Mathf.Clamp(look.y, -89f, 89f);
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);

    }

    void UpdateGround()
    {
        if(wasGrounded != IsGrounded)
        {
            OnGroundStateChange?.Invoke(IsGrounded);
            wasGrounded = IsGrounded;
        }
    }
}
