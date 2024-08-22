using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion

    private SwipeControls swipeControls;
    private Camera mainCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        swipeControls = new SwipeControls();
        mainCamera = Camera.main;

    }

    private void OnEnable()
    {
        swipeControls.Enable();
    }
    private void OnDisable()
    {
        swipeControls.Disable();
    }

    void Start()
    {
        swipeControls.Touch.PrimaryContact.started += ctx => StartTouchPrimaryContact(ctx);
        swipeControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimaryContact(ctx);
    }

    private void EndTouchPrimaryContact(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null)
        {
            OnEndTouch(InputUtils.ScreenToWorld(mainCamera, swipeControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
        }
    }

    private void StartTouchPrimaryContact(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(InputUtils.ScreenToWorld(mainCamera, swipeControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return InputUtils.ScreenToWorld(mainCamera, swipeControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
