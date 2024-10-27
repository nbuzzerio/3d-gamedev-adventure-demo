using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool jump;

    void Update()
    {
        // Get input values
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Update jump to continuously check if pressed
        jump = Input.GetButton("Jump");
    }
}
