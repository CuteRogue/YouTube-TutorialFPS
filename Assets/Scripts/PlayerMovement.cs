using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float cameraSensitivity;

    protected float verticalModifier = 0.0f;
    protected float horizontalModifier = 0.0f;

    protected float cameraHorizontalModifier = 0.0f;
    protected float cameraVerticalModifier = 0.0f;

    protected Vector2 cameraEulerRotation;

    protected virtual void Start()
    {
        cameraEulerRotation = transform.eulerAngles;
    }

    private void Update()
    {
        ReadInput();
        RotatePlayer();
        MovePlayer();
    }

    private void ReadInput()
    {
        verticalModifier = Input.GetAxis("vertical");
        horizontalModifier = Input.GetAxis("horizontal");

        cameraVerticalModifier = -Input.GetAxis("mouse_y");
        cameraHorizontalModifier = Input.GetAxis("mouse_x");
    }

    protected virtual void RotatePlayer()
    {
        float newDistance = (100 - cameraSensitivity) * Time.deltaTime;
        cameraEulerRotation += new Vector2(newDistance * cameraVerticalModifier, newDistance * cameraHorizontalModifier);

        transform.localRotation = Quaternion.Euler(cameraEulerRotation.x, cameraEulerRotation.y, 0.0f);
    }

    protected virtual void MovePlayer()
    {
        float newDistance = speed * Time.deltaTime;
        Vector2 newPosition = new Vector2(newDistance * horizontalModifier, newDistance * verticalModifier);

        Vector3 forward = Vector3.Normalize(new Vector3(transform.forward.x, 0, transform.forward.z));
        Vector3 right = Vector3.Normalize(new Vector3(transform.right.x, 0, transform.right.z));

        transform.localPosition += (newPosition.y * forward) + (newPosition.x * right);
    }
}
