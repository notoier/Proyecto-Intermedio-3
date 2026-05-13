using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float walkSpeed = 4.0f;
    [SerializeField]
    private float gravity = -20f;

    [Header("Look")]
    [SerializeField]
    private float mouseSensitibity = 0.1f;
    [SerializeField]
    private Transform cameraHolder;

    private CharacterController characterController; 
    private Vector2 moveInput;
    private Vector2 lookInput;

    private Vector3 velocity;
    private float rotation;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Look();
        Gravity();
    }

    public void OnMove(InputValue Value)
    {
        moveInput=Value.Get<Vector2>();
    }

    public void OnLook(InputValue Value)
    {
        lookInput = Value.Get<Vector2>();
    }

    private void Move()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * walkSpeed * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX = lookInput.x * mouseSensitibity;
        float mouseY = lookInput.y * mouseSensitibity;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -80f, 80);

        cameraHolder.localRotation = Quaternion.Euler(rotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void Gravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
