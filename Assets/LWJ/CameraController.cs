using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform cameraRoot;
    public float sensitivity = 1.5f;

    private Vector2 lookInput;
    private float xRotation;

    private void Update()
    {
        Vector2 mouseDelta = lookInput * sensitivity;

        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
