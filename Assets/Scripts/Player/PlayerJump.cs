using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerJump : MonoBehaviour
{
    public float jumpHeight = 5f;
    public float apexThreshold = 0.1f; // Threshold for apex of jump to allow double jump

    [SerializeField]
    private float groundCheckOffset = 0.5f; // Adjustable offset for ground detection

    private bool canDoubleJump;
    private bool doubleJumpReady;
    private float verticalVelocity;
    private bool isGrounded;

    private CharacterController characterController;
    private PlayerInput playerInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Use custom ground check method
        isGrounded = IsCharacterGrounded();

        // Debug log on jump button press
        if (playerInput.jump)
        {
            Debug.Log($"Jump Attempted! Variables - isGrounded: {isGrounded}, canDoubleJump: {canDoubleJump}, doubleJumpReady: {doubleJumpReady}, verticalVelocity: {verticalVelocity}");
        }

        HandleJump();
        ApplyGravity();
    }

    private bool IsCharacterGrounded()
    {
        // Improved ground check with serialized downward offset
        return characterController.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckOffset);
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            canDoubleJump = false;
            doubleJumpReady = false;
            verticalVelocity = -1f;
        }

        // Initial Jump
        if (playerInput.jump && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            canDoubleJump = true;
            Debug.Log("Initial Jump Executed");
        }
        else if (canDoubleJump && Mathf.Abs(verticalVelocity) < apexThreshold)
        {
            doubleJumpReady = true;
        }

        // Double Jump
        if (playerInput.jump && !isGrounded && doubleJumpReady)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            canDoubleJump = false;
            doubleJumpReady = false;
            Debug.Log("Double Jump Executed");
        }
    }

    private void ApplyGravity()
    {
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        Vector3 move = new Vector3(0, verticalVelocity, 0);
        characterController.Move(move * Time.deltaTime);
    }
}
