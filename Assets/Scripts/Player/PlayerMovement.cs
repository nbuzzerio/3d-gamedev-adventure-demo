using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController characterController;
    private PlayerInput playerInput;
    private Vector3 moveDirection;

    void Start()
    {
        // Get the required components
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Get input direction
        float horizontal = playerInput.horizontal;
        float vertical = playerInput.vertical;

        // Calculate movement direction
        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // Move the character
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
