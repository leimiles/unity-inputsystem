using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class DuckReInputManager : MonoBehaviour
{
    Ia_duckRe inputActions;
    void Awake()
    {
        inputActions = new Ia_duckRe();
        inputActions.DuckRe.TouchScreen.started += OnAction;
    }

    private void OnAction(InputAction.CallbackContext context)
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
