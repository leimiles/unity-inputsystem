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

    void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new MoveToTouch();
        inputActions.Miles.MoveTo.performed += OnAction;      // this action bound to touch contact, every pressing works

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

    public void SetWorldPosition(Vector2 screenPostion, float depth = 10.0f)
    {
        Vector3 position = new Vector3(screenPostion.x, screenPostion.y, depth);
        //Debug.Log(position);
        WorldPositionTarget = mainCamera.ScreenToWorldPoint(position);
    }
}
