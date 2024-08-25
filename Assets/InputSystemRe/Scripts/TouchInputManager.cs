using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class TouchInputManager : MonoBehaviour
{
    // fired when pressing on the screen
    public event Action<TouchInput, double> Pressed;
    public event Action<TouchInput, double> Dragged;
    public event Action<TouchInput, double> Released;
    private bool m_IsDragging;

    [SerializeField]
    private bool m_UseMouse;
    [SerializeField]
    private bool m_UseTouch;


}

public struct TouchInput
{
    public bool Contact;
    public int InputId;
    public Vector2 Position;
}

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class TouchInputComposite : InputBindingComposite<TouchInput>
{
    [InputControl(layout = "Button")]
    public int contact;

    [InputControl(layout = "Integer")]
    public int inputId;

    [InputControl(layout = "Vector2")]
    public int position;
    public override TouchInput ReadValue(ref InputBindingCompositeContext context)
    {
        var contact = context.ReadValueAsButton(this.contact);
        var pointerId = context.ReadValue<int>(inputId);
        var position = context.ReadValue<Vector2, Vector2MagnitudeComparer>(this.position);
        return new TouchInput
        {
            Contact = contact,
            InputId = inputId,
            Position = position
        };
    }

#if UNITY_EDITOR
    static TouchInputComposite()
    {
        Register();
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Register()
    {
        InputSystem.RegisterBindingComposite<TouchInputComposite>();
    }
}
