using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class RotateAroundManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    RotateAround ia_RotateAround;
    Camera mainCamera;
    static Vector3 hitPosition = Vector3.zero;
    public static Vector3 HitPosition { get => hitPosition; set => hitPosition = value; }
    public static SphericalCoordinateSystem sphericalCoordinateSystem;

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
        ia_RotateAround.Rotate.ClickOnTarget.started += SetEyesOnTargetPosition;
        ia_RotateAround.Rotate.ClickOnTarget.canceled += SlideEnd;
        ia_RotateAround.Rotate.CameraLookAround.performed += SlidingOnScreen;
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
        textMeshProUGUI.text = energy2D.ToString();
        sphericalCoordinateSystem.SetEnergyX(energy2D.x);
        CountSystem.Reset();
    }



    Vector2 firstTouchPositionOnScreen = Vector2.zero;
    Vector2 slideOffset = Vector2.zero;
    void SlidingOnScreen(InputAction.CallbackContext context)
    {
        Vector2 slidingPosition = context.action.ReadValue<Vector2>();
        slideOffset.x = (slidingPosition.x - firstTouchPositionOnScreen.x) / Screen.width;
        slideOffset.y = (slidingPosition.y - firstTouchPositionOnScreen.y) / Screen.height;
        //sphericalCoordinateSystem.AzimuthalAngle -= slideOffset.x * Time.deltaTime * 20.0f;
        sphericalCoordinateSystem.PolarAngle += slideOffset.y * Time.deltaTime * 20.0f;
    }

    private void SetEyesOnTargetPosition(InputAction.CallbackContext context)
    {
        CountSystem.Active = true;
        Vector2 touchPositionOnScreen = context.action.ReadValue<Vector2>();
        firstTouchPositionOnScreen = touchPositionOnScreen;
        Ray ray = mainCamera.ScreenPointToRay(touchPositionOnScreen);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            HitPosition = raycastHit.point;
        }

    }



    void Start()
    {
        sphericalCoordinateSystem = new SphericalCoordinateSystem(mainCamera.transform.position);
    }


    void Update()
    {
        CountSystem.Count();
        sphericalCoordinateSystem.AutoRotate();
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

    public class SphericalCoordinateSystem
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
        public void SetEnergyX(float energy)
        {
            energyX = energy;
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
