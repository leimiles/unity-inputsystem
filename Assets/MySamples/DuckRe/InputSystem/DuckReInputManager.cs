using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class DuckReInputManager : MonoBehaviour
{
    [SerializeField] Text debugText;
    Ia_duckRe inputActions;
    void Awake()
    {
        inputActions = new Ia_duckRe();
        inputActions.DuckRe.Attack.started += TouchOn;
        inputActions.DuckRe.Position.performed += Test;
    }

    private void Test(InputAction.CallbackContext context)
    {
        debugText.text = context.ReadValue<Vector2>().ToString();
    }


    private void TouchOn(InputAction.CallbackContext context)
    {
        if (!DuckReGameplay.gameStart)
        {
            DuckReGameplay.gameStart = true;
        }
        Debug.Log("clicked");
        //debugText.text = context.action.ReadValue<Vector2>().ToString();
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
