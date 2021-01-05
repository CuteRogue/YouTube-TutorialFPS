using UnityEngine;

public class PlayerMovementCharacterController : PlayerMovement
{
    public Camera playerCam;

    private CharacterController control;

    // gravity
    private float gravitySpeed = 0.0f;
    private const float gravityAccel = -9.81f;

    protected override void Start()
    {
        control = GetComponent<CharacterController>();
    }

    protected override void RotatePlayer()
    {
        float newDistance = (100 - cameraSensitivity) * Time.deltaTime;
        cameraEulerRotation += new Vector2(newDistance * cameraVerticalModifier, newDistance * cameraHorizontalModifier);

        playerCam.transform.localRotation = Quaternion.Euler(cameraEulerRotation.x, 0.0f, 0.0f);
        transform.localRotation = Quaternion.Euler(0.0f, cameraEulerRotation.y, 0.0f);
    }

    protected override void MovePlayer()
    {
        float newDistance = speed * Time.deltaTime;
        Vector2 newPosition = new Vector2(newDistance * horizontalModifier, newDistance * verticalModifier);

        Vector3 forward = Vector3.Normalize(new Vector3(transform.forward.x, 0, transform.forward.z));
        Vector3 right = Vector3.Normalize(new Vector3(transform.right.x, 0, transform.right.z));

        control.Move((newPosition.y * forward) + (newPosition.x * right));

        //gravity
        if (!control.isGrounded)
        {
            gravitySpeed += gravityAccel * Time.deltaTime;
        }
        else
        {
            gravitySpeed = 0.0f;
        }

        control.Move(new Vector3(0.0f, gravitySpeed * Time.deltaTime, 0.0f));
    }
}
