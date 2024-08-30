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
        inputActions.DuckRe.Attack.started += OnAction;
        inputActions.DuckRe.Position.performed += ClickPostion;
    }

    private void ClickPostion(InputAction.CallbackContext context)
    {
        debugText.text = context.ReadValue<Vector2>().ToString();
    }


    private void OnAction(InputAction.CallbackContext context)
    {
        if (!DuckReGameplay.gameStart)
        {
            DuckReGameplay.gameStart = true;
        }
        Debug.Log("clicked");
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
