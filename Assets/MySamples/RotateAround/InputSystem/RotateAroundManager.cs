using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class RotateAroundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Transform lookAtTarget;
    [SerializeField] float lookAtTargetMoveSpeed = 10.0f;
    RotateAround ia_RotateAround;
    Camera mainCamera;
    static Vector3 hitPosition = Vector3.zero;
    static SphericalCoordinateSystem sphericalCoordinateSystem;
    static Vector2 m_1stFingerStartPosition = Vector2.zero;
    static Vector2 m_1stFingerPosition = Vector2.zero;
    static Vector2 m_2ndFingerStartPosition = Vector2.zero;
    static Vector2 m_2ndFingerPosition = Vector2.zero;

    void Awake()
    {
        if (Camera.main == null)
        {
            return;
        }
        else
        {
            mainCamera = Camera.main;
        }
        ia_RotateAround = new RotateAround();
        ia_RotateAround.Rotate.Finger0.started += SlideStart;
        ia_RotateAround.Rotate.Finger0.performed += Sliding;
        ia_RotateAround.Rotate.Finger0.canceled += SlideEnd;
        ia_RotateAround.Rotate.Finger1.started += ZoomStart;
        ia_RotateAround.Rotate.Finger1.performed += Zooming;
    }

    void OnEnable()
    {
        ia_RotateAround?.Enable();
    }

    void OnDisable()
    {
        ia_RotateAround?.Disable();
    }

    void SlideEnd(InputAction.CallbackContext context)
    {
        CountSystem.Active = false;
        Vector2 energy2D = slideOffset / CountSystem.GetTimePassed();
        sphericalCoordinateSystem.SetEnergyX(energy2D.x, true);
        CountSystem.Reset();
    }

    void SetLookAtTargetPosition()
    {
        lookAtTarget.position = Vector3.MoveTowards(lookAtTarget.position, hitPosition, Time.deltaTime * lookAtTargetMoveSpeed);
    }

    void ZoomStart(InputAction.CallbackContext context)
    {
        m_2ndFingerStartPosition = context.action.ReadValue<Vector2>();
    }

    void Zooming(InputAction.CallbackContext context)
    {
        m_2ndFingerPosition = context.action.ReadValue<Vector2>();
        //float distanceBetweenFingers = Vector2.Distance(firstTouchPositionOnScreenSliding, context.action.ReadValue<Vector2>());
        //sphericalCoordinateSystem.Radius += 1.0f * Time.deltaTime;
    }

    Vector2 slideOffset = Vector2.zero;
    void Sliding(InputAction.CallbackContext context)
    {
        Vector2 slidingPosition = context.action.ReadValue<Vector2>();
        m_1stFingerPosition = slidingPosition;

        if (ia_RotateAround.Rotate.Finger1.phase == InputActionPhase.Started)
        {
            return;
        }

        slideOffset.x = (slidingPosition.x - m_1stFingerStartPosition.x) / Screen.width;
        slideOffset.y = (slidingPosition.y - m_1stFingerStartPosition.y) / Screen.height;
        //sphericalCoordinateSystem.AzimuthalAngle -= slideOffset.x * Time.deltaTime * 20.0f;
        sphericalCoordinateSystem.PolarAngle += slideOffset.y * Time.deltaTime * 20.0f;
    }

    private void SlideStart(InputAction.CallbackContext context)
    {
        CountSystem.Active = true;
        Vector2 touchPositionOnScreen = context.action.ReadValue<Vector2>();
        m_1stFingerStartPosition = touchPositionOnScreen;
        Ray ray = mainCamera.ScreenPointToRay(touchPositionOnScreen);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            sphericalCoordinateSystem.SetEnergyX(0.0f);
            hitPosition = raycastHit.point;
        }

    }



    void Start()
    {
        if (mainCamera == null)
        {
            return;
        }
        sphericalCoordinateSystem = new SphericalCoordinateSystem(mainCamera.transform.position);
    }


    void Update()
    {
        if (mainCamera == null || lookAtTarget == null || sphericalCoordinateSystem == null)
        {
            return;
        }
        CountSystem.Count();
        textMeshProUGUI.text = sphericalCoordinateSystem.EnergyX.ToString();
        sphericalCoordinateSystem.AutoRotate();
        SetMainCameraPosition();
        SetLookAtTargetPosition();

        //Debug.Log("CameraRotateAround: " + ia_RotateAround.Rotate.ClickOnTarget.phase);
        //Debug.Log("CameraZoom: " + ia_RotateAround.Rotate.CameraZoom.phase);
    }

    void SetMainCameraPosition()
    {
        mainCamera.transform.position = sphericalCoordinateSystem.GetCartesianPosition();
    }


    class CountSystem
    {
        static float timePassedInSeconds = 0.0f;
        private static bool active = false;

        public static bool Active { get => active; set => active = value; }

        public static void Reset()
        {
            timePassedInSeconds = 0.0f;
        }

        public static void Count()
        {
            if (Active)
            {
                timePassedInSeconds += Time.deltaTime;
            }

        }

        public static float GetTimePassed()
        {
            return timePassedInSeconds;
        }

        static string GetFormatTimeString()
        {
            int minutes = Mathf.FloorToInt(timePassedInSeconds / 60F);
            int seconds = Mathf.FloorToInt(timePassedInSeconds - minutes * 60);
            float milliseconds = (timePassedInSeconds - Mathf.Floor(timePassedInSeconds)) * 1000;
            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

    }

    class SphericalCoordinateSystem
    {
        float radius;
        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                if (value < 0)
                {
                    radius = 0;
                }
            }
        }

        float polarAngle;
        /// <summary>
        /// use radian to set angle, 0 <= polarAngle <= 180
        /// </summary>
        public float PolarAngle
        {
            get => polarAngle;
            //set => polarAngle = value;
            set
            {
                polarAngle = value;

                if (polarAngle < 0.1745f)
                {
                    polarAngle = 0.1745f;
                }
                if (polarAngle > 2.9671f)
                {
                    polarAngle = 2.9671f;
                }
            }
        }

        float energyX;
        public void SetEnergyX(float energy, bool useAccumulation = false)
        {
            if (useAccumulation)
            {
                energyX += energy;
            }
            else
            {
                energyX = energy;
            }

        }
        public float EnergyX
        {
            get
            {
                return energyX;
            }
        }
        public void AutoRotate()
        {
            if (energyX > 0.01)
            {
                energyX -= Time.deltaTime * 2.0f;
                this.azimuthalAngle += Time.deltaTime * energyX;
            }
            if (energyX < -0.01)
            {
                energyX += Time.deltaTime * 2.0f;
                this.azimuthalAngle += Time.deltaTime * energyX;
            }
        }

        float azimuthalAngle;
        /// <summary>
        /// use radian to set angle, 0 <= azimuthalAngle <= 360
        /// </summary>
        public float AzimuthalAngle
        {
            get => azimuthalAngle;
            set => azimuthalAngle = value;
        }
        public SphericalCoordinateSystem(Vector3 position)
        {
            radius = Mathf.Sqrt(position.x * position.x + position.y * position.y + position.z * position.z);
            polarAngle = Mathf.Acos(position.y / radius);       // unity uses y-up
            azimuthalAngle = Mathf.Atan2(position.z, position.x);
        }

        public Vector3 GetCartesianPosition()
        {
            Vector3 cartesianCoordinate = Vector3.zero;

            cartesianCoordinate.x = radius * Mathf.Sin(polarAngle) * Mathf.Cos(azimuthalAngle);
            cartesianCoordinate.z = radius * Mathf.Sin(polarAngle) * Mathf.Sin(azimuthalAngle);
            cartesianCoordinate.y = radius * Mathf.Cos(polarAngle);     // unity uses y-up
            return cartesianCoordinate;
        }

    }

}
