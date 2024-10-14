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
    Camera m_MainCamera;
    static Vector3 m_HitPosition = Vector3.zero;
    static SphericalCoordinateSystem m_SphericalCoordinateSystem;
    static Vector2 m_1stFingerStartPosition = Vector2.zero;
    static Vector2 m_1stFingerPosition = Vector2.zero;
    static Vector2 m_1stFingerOffset = Vector2.zero;
    static Vector2 m_2ndFingerStartPosition = Vector2.zero;
    static Vector2 m_2ndFingerPosition = Vector2.zero;
    static float m_DistanceBetween2Fingers = 0.0f;

    void Awake()
    {
        if (Camera.main == null)
        {
            return;
        }
        else
        {
            m_MainCamera = Camera.main;
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
        Vector2 energy2D = m_1stFingerOffset / CountSystem.GetTimePassed();
        m_SphericalCoordinateSystem.SetEnergyX(energy2D.x, true);
        CountSystem.Reset();
    }

    void SetLookAtTargetPosition()
    {
        lookAtTarget.position = Vector3.MoveTowards(lookAtTarget.position, m_HitPosition, Time.deltaTime * lookAtTargetMoveSpeed);
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

    void Sliding(InputAction.CallbackContext context)
    {
        Vector2 slidingPosition = context.action.ReadValue<Vector2>();
        m_1stFingerPosition = slidingPosition;

        if (ia_RotateAround.Rotate.Finger1.phase == InputActionPhase.Started)
        {
            return;
        }

        m_1stFingerOffset.x = (slidingPosition.x - m_1stFingerStartPosition.x) / Screen.width;
        m_1stFingerOffset.y = (slidingPosition.y - m_1stFingerStartPosition.y) / Screen.height;
        //sphericalCoordinateSystem.AzimuthalAngle -= slideOffset.x * Time.deltaTime * 20.0f;
        m_SphericalCoordinateSystem.PolarAngle += m_1stFingerOffset.y * Time.deltaTime * 20.0f;
    }

    private void SlideStart(InputAction.CallbackContext context)
    {
        CountSystem.Active = true;
        Vector2 touchPositionOnScreen = context.action.ReadValue<Vector2>();
        m_1stFingerStartPosition = touchPositionOnScreen;
        Ray ray = m_MainCamera.ScreenPointToRay(touchPositionOnScreen);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            m_SphericalCoordinateSystem.SetEnergyX(0.0f);
            m_HitPosition = raycastHit.point;
        }

    }



    void Start()
    {
        if (m_MainCamera == null)
        {
            return;
        }
        m_SphericalCoordinateSystem = new SphericalCoordinateSystem(m_MainCamera.transform.position);
    }


    void Update()
    {
        if (m_MainCamera == null || lookAtTarget == null || m_SphericalCoordinateSystem == null)
        {
            return;
        }
        CountSystem.Count();
        textMeshProUGUI.text = m_SphericalCoordinateSystem.EnergyX.ToString();
        m_SphericalCoordinateSystem.AutoRotate();
        SetMainCameraPosition();
        SetLookAtTargetPosition();

        //Debug.Log("CameraRotateAround: " + ia_RotateAround.Rotate.ClickOnTarget.phase);
        //Debug.Log("CameraZoom: " + ia_RotateAround.Rotate.CameraZoom.phase);
    }

    void SetMainCameraPosition()
    {
        m_MainCamera.transform.position = m_SphericalCoordinateSystem.GetCartesianPosition();
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
