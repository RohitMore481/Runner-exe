using UnityEngine;
using UnityEngine.InputSystem; // new Input System namespace

public class GroundJump : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpSpeed = 5f;

    private Vector3 originalPosition;
    private bool isJumping = false;
    private bool goingUp = true;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        // New Input System
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            isJumping = true;
            goingUp = true;
        }

        if (isJumping)
        {
            if (goingUp)
            {
                transform.position += Vector3.up * jumpSpeed * Time.deltaTime;

                if (transform.position.y >= originalPosition.y + jumpHeight)
                    goingUp = false;
            }
            else
            {
                transform.position -= Vector3.up * jumpSpeed * Time.deltaTime;

                if (transform.position.y <= originalPosition.y)
                {
                    transform.position = originalPosition;
                    isJumping = false;
                }
            }
        }
    }
}
