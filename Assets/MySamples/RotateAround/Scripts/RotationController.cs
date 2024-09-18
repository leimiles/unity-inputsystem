using UnityEngine;

[DisallowMultipleComponent]
public class RotationController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public Camera MainCamera { get => mainCamera; set => mainCamera = value; }
    [SerializeField] Light mainLight;
    [SerializeField] float eyesOnTargetDistance = 10.0f;
    Vector3 eyesOnTargetPosition = Vector3.zero;
    public Vector3 EyesOnTargetPosition { get => eyesOnTargetPosition; set => eyesOnTargetPosition = value; }
    float CameraRotationSpeed = 20.0f;
    float CameraMovementSpeed = 10.0f;
    Vector2 slideSpeed = Vector2.zero;


    void Start()
    {
        InitializeCameraPosition();
    }


    void Update()
    {
        FocusOnTarget();
        SetCameraPosition();
    }
    private void InitializeCameraPosition()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {

    }

    void FocusOnTarget()
    {
        Vector3 targetDirection = (eyesOnTargetPosition - MainCamera.transform.position).normalized;
        if (targetDirection.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            float angle = Quaternion.Angle(MainCamera.transform.rotation, targetRotation);
            if (angle > 1.0f)
            {
                MainCamera.transform.rotation = Quaternion.RotateTowards(MainCamera.transform.rotation, targetRotation, CameraRotationSpeed * Time.deltaTime);
            }
        }

    }

    void RotateLight()
    {
    }

    void KeepDistance()
    {

    }

}
