using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class SwipeControls2Manager : MonoBehaviour
{
    private string info = "";
    [SerializeField] private bool m_UseMouse;
    [SerializeField] private bool m_UsePen;
    [SerializeField] private bool m_UseTouch;

    private SwipeControls2 m_SwipeControls2;
    protected virtual void Awake()
    {
        m_SwipeControls2 = new SwipeControls2();
        m_SwipeControls2.SwipeMap.SwipeActions.performed += OnAction;
        m_SwipeControls2.SwipeMap.SwipeActions.canceled += OnAction;

        SyncBindingMask();
    }

    protected void OnAction(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase.ToString());
    }

    protected virtual void OnEnable()
    {
        m_SwipeControls2?.Enable();
    }

    protected virtual void OnDisable()
    {
        m_SwipeControls2?.Disable();
    }

    private void SyncBindingMask()
    {
        if (m_SwipeControls2 == null)
        {
            return;
        }

        if (m_UseMouse && m_UsePen && m_UseTouch)
        {
            m_SwipeControls2.bindingMask = null;
            return;
        }

        m_SwipeControls2.bindingMask = InputBinding.MaskByGroups(new[] {
            m_UseMouse?"Mouse":null,
            m_UseMouse?"Pen":null,
            m_UseMouse?"Touch":null,
        });
    }

    private void OnValidate()
    {
        SyncBindingMask();
    }

    void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 50;
        labelStyle.normal.textColor = Color.red;
        GUILayout.Label(info, labelStyle);
    }
}
