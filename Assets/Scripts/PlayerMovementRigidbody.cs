using UnityEngine;

public class PlayerMovementRigidbody : PlayerMovement
{
    public Camera playerCam;

    private Rigidbody rig;

    protected override void Start()
    {
        rig = GetComponent<Rigidbody>();
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
        Vector2 newSpeed = new Vector2(speed * horizontalModifier, speed * verticalModifier);

        Vector3 forward = Vector3.Normalize(new Vector3(transform.forward.x, 0.0f, transform.forward.z));
        Vector3 right = Vector3.Normalize(new Vector3(transform.right.x, 0.0f, transform.right.z));

        rig.velocity = (newSpeed.y * forward) + (newSpeed.x * right) + new Vector3(0.0f, rig.velocity.y, 0.0f);
    }
}
