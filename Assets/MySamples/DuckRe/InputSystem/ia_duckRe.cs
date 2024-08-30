//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/MySamples/DuckRe/InputSystem/ia_duckRe.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Ia_duckRe: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Ia_duckRe()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ia_duckRe"",
    ""maps"": [
        {
            ""name"": ""DuckRe"",
            ""id"": ""482d1d02-9f59-4425-becb-93632d5adba3"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""83198c49-4fc4-428f-83d8-6b7dc7cd518d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""4ebcb868-facd-475f-8189-51de47a48b88"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc9303ae-d0fc-47f4-88ba-543395cf65d0"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ba6607e-5475-40ca-9e6b-2ccc317bb4c5"",
                    ""path"": ""<Touchscreen>/primaryTouch/startPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DuckRe
        m_DuckRe = asset.FindActionMap("DuckRe", throwIfNotFound: true);
        m_DuckRe_Attack = m_DuckRe.FindAction("Attack", throwIfNotFound: true);
        m_DuckRe_Position = m_DuckRe.FindAction("Position", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // DuckRe
    private readonly InputActionMap m_DuckRe;
    private List<IDuckReActions> m_DuckReActionsCallbackInterfaces = new List<IDuckReActions>();
    private readonly InputAction m_DuckRe_Attack;
    private readonly InputAction m_DuckRe_Position;
    public struct DuckReActions
    {
        private @Ia_duckRe m_Wrapper;
        public DuckReActions(@Ia_duckRe wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_DuckRe_Attack;
        public InputAction @Position => m_Wrapper.m_DuckRe_Position;
        public InputActionMap Get() { return m_Wrapper.m_DuckRe; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DuckReActions set) { return set.Get(); }
        public void AddCallbacks(IDuckReActions instance)
        {
            if (instance == null || m_Wrapper.m_DuckReActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DuckReActionsCallbackInterfaces.Add(instance);
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Position.started += instance.OnPosition;
            @Position.performed += instance.OnPosition;
            @Position.canceled += instance.OnPosition;
        }

        private void UnregisterCallbacks(IDuckReActions instance)
        {
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Position.started -= instance.OnPosition;
            @Position.performed -= instance.OnPosition;
            @Position.canceled -= instance.OnPosition;
        }

        public void RemoveCallbacks(IDuckReActions instance)
        {
            if (m_Wrapper.m_DuckReActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDuckReActions instance)
        {
            foreach (var item in m_Wrapper.m_DuckReActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DuckReActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DuckReActions @DuckRe => new DuckReActions(this);
    public interface IDuckReActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
    }
}