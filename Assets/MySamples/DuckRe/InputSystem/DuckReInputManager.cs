using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// this script is not needed if using player input component
[DisallowMultipleComponent]
public class DuckReInputManager : MonoBehaviour
{
    Ia_duckRe inputActions;
    void Awake()
    {
        inputActions = new Ia_duckRe();
        inputActions.DuckRe.TouchPosition.performed += TouchPosition;
        inputActions.DuckRe.TouchOn.started += TouchOn;
    }

    private void TouchPosition(InputAction.CallbackContext context)
    {
    }

    private void TouchOn(InputAction.CallbackContext context)
    {
        if (!DuckReGameplay.gameStart)
        {
            DuckReGameplay.gameStart = true;
        }
    }

    void OnEnable()
    {
        inputActions?.Enable();
    }

    void OnDisable()
    {
        inputActions?.Disable();
    }
}
