// GENERATED AUTOMATICALLY FROM 'Assets/Input System/InputTouch.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputTouch : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputTouch()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputTouch"",
    ""maps"": [
        {
            ""name"": ""mainControls"",
            ""id"": ""a39b9878-5768-4b10-affa-e09670660a88"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ddc092aa-6fe2-4fd3-88a8-2a919f9c96be"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""21faa0df-8851-4084-adbc-0acd2d8d53e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ce1b4ab6-758e-4f8b-9295-14f74cf88cbc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c9a558b0-db18-4d09-a651-bffc4cff217c"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b095b899-71ad-4b32-94a3-5a844769727f"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""897f2e3f-15b0-4f8a-a510-33d0522a50d1"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
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
        // mainControls
        m_mainControls = asset.FindActionMap("mainControls", throwIfNotFound: true);
        m_mainControls_TouchInput = m_mainControls.FindAction("TouchInput", throwIfNotFound: true);
        m_mainControls_TouchPress = m_mainControls.FindAction("TouchPress", throwIfNotFound: true);
        m_mainControls_TouchPosition = m_mainControls.FindAction("TouchPosition", throwIfNotFound: true);
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

    // mainControls
    private readonly InputActionMap m_mainControls;
    private IMainControlsActions m_MainControlsActionsCallbackInterface;
    private readonly InputAction m_mainControls_TouchInput;
    private readonly InputAction m_mainControls_TouchPress;
    private readonly InputAction m_mainControls_TouchPosition;
    public struct MainControlsActions
    {
        private @InputTouch m_Wrapper;
        public MainControlsActions(@InputTouch wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchInput => m_Wrapper.m_mainControls_TouchInput;
        public InputAction @TouchPress => m_Wrapper.m_mainControls_TouchPress;
        public InputAction @TouchPosition => m_Wrapper.m_mainControls_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_mainControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMainControlsActions instance)
        {
            if (m_Wrapper.m_MainControlsActionsCallbackInterface != null)
            {
                @TouchInput.started -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchInput;
                @TouchInput.performed -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchInput;
                @TouchInput.canceled -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchInput;
                @TouchPress.started -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchPress;
                @TouchPosition.started -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_MainControlsActionsCallbackInterface.OnTouchPosition;
            }
            m_Wrapper.m_MainControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchInput.started += instance.OnTouchInput;
                @TouchInput.performed += instance.OnTouchInput;
                @TouchInput.canceled += instance.OnTouchInput;
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
            }
        }
    }
    public MainControlsActions @mainControls => new MainControlsActions(this);
    public interface IMainControlsActions
    {
        void OnTouchInput(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}
