using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// this script is not needed if using player input component
[DisallowMultipleComponent]
public class DuckReInputManager : MonoBehaviour
{
    [SerializeField] Text debugText;
    [SerializeField] LayerMask touchButtonLayer;
    Ia_duckRe inputActions;
    Camera mainCamera;
    void Awake()
    {
        inputActions = new Ia_duckRe();
        inputActions.DuckRe.TouchOn.started += TouchOn;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void TouchOn(InputAction.CallbackContext context)
    {
        Vector2 position = context.action.ReadValue<Vector2>();
        GetButtonNameByInputPosition(position);
    }

    void OnEnable()
    {
        inputActions?.Enable();
    }

    void OnDisable()
    {
        inputActions?.Disable();
    }


    private void GetButtonNameByInputPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, touchButtonLayer))
        {
            debugText.text = raycastHit.collider.gameObject.name;
        }
    }
}
