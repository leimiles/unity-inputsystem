using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveToTouchManager : MonoBehaviour
{
    Camera mainCamera;
    MoveToTouch inputActions;
    public static Vector3 WorldPositionTarget = Vector3.zero;

    [SerializeField]
    float touchDepth = 20.0f;    // set camera at 0, 0, 0, world space, this value is there object z

    void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new MoveToTouch();
        inputActions.Miles.MoveTo.performed += MoveTo;      // this action bound to touch contact, every pressing works
        inputActions.Miles.Fire.started += Fire;

        //WorldPositionTarget.y = touchDepth;

    }

    void OnEnable()
    {
        inputActions?.Enable();
    }

    void OnDisable()
    {
        inputActions?.Disable();
    }

    private void MoveTo(InputAction.CallbackContext context)
    {
        SetWorldPosition(context.ReadValue<Vector2>());
    }

    private void Fire(InputAction.CallbackContext context)
    {
    }

    public void SetWorldPosition(Vector2 screenPostion)
    {
        Vector3 position = new Vector3(screenPostion.x, screenPostion.y, touchDepth);
        WorldPositionTarget = mainCamera.ScreenToWorldPoint(position);
    }
}
