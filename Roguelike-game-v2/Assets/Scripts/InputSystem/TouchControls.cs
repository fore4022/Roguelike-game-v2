//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/TouchControls.inputactions
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

public partial class @TouchControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchControls"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""efd5e837-92b5-4c67-ae4d-911dceb21ca1"",
            ""actions"": [
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""Button"",
                    ""id"": ""efc1af79-054c-487e-90ea-e2f8ac1adb90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""28bdd81b-d77a-4adf-a5af-b9a63356cb28"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_TouchPosition = m_Touch.FindAction("TouchPosition", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private List<ITouchActions> m_TouchActionsCallbackInterfaces = new List<ITouchActions>();
    private readonly InputAction m_Touch_TouchPosition;
    public struct TouchActions
    {
        private @TouchControls m_Wrapper;
        public TouchActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchPosition => m_Wrapper.m_Touch_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void AddCallbacks(ITouchActions instance)
        {
            if (instance == null || m_Wrapper.m_TouchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TouchActionsCallbackInterfaces.Add(instance);
            @TouchPosition.started += instance.OnTouchPosition;
            @TouchPosition.performed += instance.OnTouchPosition;
            @TouchPosition.canceled += instance.OnTouchPosition;
        }

        private void UnregisterCallbacks(ITouchActions instance)
        {
            @TouchPosition.started -= instance.OnTouchPosition;
            @TouchPosition.performed -= instance.OnTouchPosition;
            @TouchPosition.canceled -= instance.OnTouchPosition;
        }

        public void RemoveCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITouchActions instance)
        {
            foreach (var item in m_Wrapper.m_TouchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TouchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}