using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SetSlideSpeed2D : MonoBehaviour
{
    [SerializeField] Text DebugText;
    [SerializeField] RotationController rotationController;
    RotateAround ia_RotateAround;

    void Awake()
    {
        if (rotationController == null)
        {
            if (!TryGetComponent<RotationController>(out rotationController))
            {
                return;
            }

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
        SetSlideSpeed();
        CountSystem.Active = false;
        DebugText.text = CountSystem.GetTimePassed().ToString();
        CountSystem.Reset();
    }

    void SetSlideSpeed()
    {

    }

    void SlidingOnScreen(InputAction.CallbackContext context)
    {
        //DebugText.text = context.action.ReadValue<Vector2>().ToString();
    }

    private void SetEyesOnTargetPosition(InputAction.CallbackContext context)
    {
        CountSystem.Active = true;
        Vector2 touchPositionOnScreen = context.action.ReadValue<Vector2>();
        Ray ray = rotationController.MainCamera.ScreenPointToRay(touchPositionOnScreen);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            rotationController.EyesOnTargetPosition = raycastHit.point;
        }
        else
        {
            rotationController.EyesOnTargetPosition = Vector3.zero;
        }
    }

    void Update()
    {
        CountSystem.Count();
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

        static string FormatTimeToString()
        {
            int minutes = Mathf.FloorToInt(timePassedInSeconds / 60F);
            int seconds = Mathf.FloorToInt(timePassedInSeconds - minutes * 60);
            float milliseconds = (timePassedInSeconds - Mathf.Floor(timePassedInSeconds)) * 1000;
            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

    }



}
