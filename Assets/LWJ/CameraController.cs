using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float rotateCamXAxisSpeed = 5f;
    [SerializeField]
    private float rotateCamYAxisSpeed = 3f;

    private float limitMinX = -80f;
    private float limitMaxX = 50f;
    private float eulerAngleX;
    private float eulerAngleY;
        

    //private void Update()
    //{
    //    Vector2 mouseDelta = lookInput * sensitivity;

    //    xRotation -= mouseDelta.y;
    //    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    //    cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //    transform.Rotate(Vector3.up * mouseDelta.x);
    //}

    public void UpdateRotate(Vector2 mouseRotate)
    {
        eulerAngleX += mouseRotate.x * rotateCamXAxisSpeed;
        eulerAngleY -= mouseRotate.y * rotateCamYAxisSpeed;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleY, eulerAngleX, 0f);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360;
        if (angle > 360f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    //public void OnLook(InputAction.CallbackContext context)
    //{
    //    lookInput = context.ReadValue<Vector2>();
    //}
}
