using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 0.1f;
    
    private float horizontalSpeed;
    
    private float verticalSpeed;

    private float verticalMin = -80f;
    private float verticalMax = 50f;
    private float verticalAngle;
    private float horizontalAngle;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        horizontalSpeed = 5f * sensitivity;
        verticalSpeed = 3f * sensitivity;
    }

    public void UpdateRotate(Vector2 mouseRotate)
    {
        horizontalSpeed = 5f * sensitivity;
        verticalSpeed = 3f * sensitivity;
        horizontalAngle += mouseRotate.x * horizontalSpeed;
        verticalAngle -= mouseRotate.y * verticalSpeed;

        verticalAngle = ClampAngle(verticalAngle, verticalMin, verticalMax);

        transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360;
        if (angle > 360f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }
}
