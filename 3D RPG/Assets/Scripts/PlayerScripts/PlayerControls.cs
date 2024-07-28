using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private CharacterController characterController;
    private float movementX;
    private float movementY;

    private PlayerInputActions playerControls;
    private InputAction jump;

    [Header("Movement")]
    private Vector3 playerVelocity;
    [SerializeField] float moveSpeed = 0.0f;

    [Header("Jump")]
    private bool isGrounded;
    [SerializeField] float jumpHeight = 0.0f;
    private float gravityValue = -9.81f;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        jump.Disable();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        characterController.Move(movement * Time.deltaTime * moveSpeed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }
}
