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
    Transform touchDepthObject;
    float touchDepth = 0.0f;    // set camera at 0, 0, 0, world space, this value is there object z

    void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new MoveToTouch();
        inputActions.Miles.MoveTo.performed += OnAction;      // this action bound to touch contact, every pressing works
        if (touchDepthObject != null)
        {
            touchDepth = touchDepthObject.transform.position.z;
        }
        WorldPositionTarget.z = touchDepth;

    }

    void OnEnable()
    {
        inputActions?.Enable();
    }

    void OnDisable()
    {
        inputActions?.Disable();
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        SetWorldPosition(context.ReadValue<Vector2>());
    }

    public void SetWorldPosition(Vector2 screenPostion)
    {
        Vector3 position = new Vector3(screenPostion.x, screenPostion.y, touchDepth);
        WorldPositionTarget = mainCamera.ScreenToWorldPoint(position);
    }
}
