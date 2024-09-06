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
        inputActions.DuckRe.TouchOn.started += TouchOn;
    }

    private void TouchOn(InputAction.CallbackContext context)
    {
        Debug.Log("clicked");
        Debug.Log(context.action.ReadValue<Vector2>().ToString());
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
