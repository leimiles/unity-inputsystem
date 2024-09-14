using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class RotateAroundManager : MonoBehaviour
{
    [SerializeField] Text DebugText;

    RotateAround ia_RotateAround;
    Camera mainCamera;

    void Awake()
    {
        ia_RotateAround = new RotateAround();
        ia_RotateAround.Rotate.ClickOnTarget.started += LookAt;
        ia_RotateAround.Rotate.CameraLookAround.performed += SetCameraRotation;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        ia_RotateAround?.Enable();
    }

    void OnDisable()
    {
        ia_RotateAround?.Disable();
    }

    void SetCameraRotation(InputAction.CallbackContext context)
    {
        Debug.Log("one touch sliding");
    }

    private void LookAt(InputAction.CallbackContext context)
    {
        Debug.Log("click and look at");
    }

}
