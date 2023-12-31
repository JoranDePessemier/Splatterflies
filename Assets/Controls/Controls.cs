//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/Controls/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""8af49f03-ed82-4bf4-8a1c-d7e1f13db439"",
            ""actions"": [
                {
                    ""name"": ""ActionPressed"",
                    ""type"": ""Button"",
                    ""id"": ""cbc85cca-108b-4b79-aeef-3a25fc8ac091"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionHolding"",
                    ""type"": ""Button"",
                    ""id"": ""d356f843-756f-4301-b455-0c5cd1c7862f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionReleased"",
                    ""type"": ""Button"",
                    ""id"": ""b3697cd6-4cb0-47f7-935a-3873501bfbd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""b21647cf-c4ff-42f5-8661-fd70e0dd4e27"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseSpeed"",
                    ""type"": ""Value"",
                    ""id"": ""f03e42f8-3a56-4de3-b8bf-d3937fbd1c92"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangeScenePressed"",
                    ""type"": ""Button"",
                    ""id"": ""320d8a21-48c6-4940-83be-d2dc515e376e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""117da5c5-c41b-4e4b-9891-ebba560cfd19"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f53d5b8-1598-4b29-8b66-91f97e5b3766"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionHolding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""498ae708-7f61-4ebb-ba90-c6bbb35538f2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionReleased"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""868f0d97-cc01-4126-b9b8-8e58dd5b1769"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d54502a5-a2c8-4635-863f-d98f597323b6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcf37ecf-57b2-49b1-813b-55828d901728"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeScenePressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_ActionPressed = m_PlayerInput.FindAction("ActionPressed", throwIfNotFound: true);
        m_PlayerInput_ActionHolding = m_PlayerInput.FindAction("ActionHolding", throwIfNotFound: true);
        m_PlayerInput_ActionReleased = m_PlayerInput.FindAction("ActionReleased", throwIfNotFound: true);
        m_PlayerInput_MousePosition = m_PlayerInput.FindAction("MousePosition", throwIfNotFound: true);
        m_PlayerInput_MouseSpeed = m_PlayerInput.FindAction("MouseSpeed", throwIfNotFound: true);
        m_PlayerInput_ChangeScenePressed = m_PlayerInput.FindAction("ChangeScenePressed", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private List<IPlayerInputActions> m_PlayerInputActionsCallbackInterfaces = new List<IPlayerInputActions>();
    private readonly InputAction m_PlayerInput_ActionPressed;
    private readonly InputAction m_PlayerInput_ActionHolding;
    private readonly InputAction m_PlayerInput_ActionReleased;
    private readonly InputAction m_PlayerInput_MousePosition;
    private readonly InputAction m_PlayerInput_MouseSpeed;
    private readonly InputAction m_PlayerInput_ChangeScenePressed;
    public struct PlayerInputActions
    {
        private @Controls m_Wrapper;
        public PlayerInputActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ActionPressed => m_Wrapper.m_PlayerInput_ActionPressed;
        public InputAction @ActionHolding => m_Wrapper.m_PlayerInput_ActionHolding;
        public InputAction @ActionReleased => m_Wrapper.m_PlayerInput_ActionReleased;
        public InputAction @MousePosition => m_Wrapper.m_PlayerInput_MousePosition;
        public InputAction @MouseSpeed => m_Wrapper.m_PlayerInput_MouseSpeed;
        public InputAction @ChangeScenePressed => m_Wrapper.m_PlayerInput_ChangeScenePressed;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerInputActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerInputActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerInputActionsCallbackInterfaces.Add(instance);
            @ActionPressed.started += instance.OnActionPressed;
            @ActionPressed.performed += instance.OnActionPressed;
            @ActionPressed.canceled += instance.OnActionPressed;
            @ActionHolding.started += instance.OnActionHolding;
            @ActionHolding.performed += instance.OnActionHolding;
            @ActionHolding.canceled += instance.OnActionHolding;
            @ActionReleased.started += instance.OnActionReleased;
            @ActionReleased.performed += instance.OnActionReleased;
            @ActionReleased.canceled += instance.OnActionReleased;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @MouseSpeed.started += instance.OnMouseSpeed;
            @MouseSpeed.performed += instance.OnMouseSpeed;
            @MouseSpeed.canceled += instance.OnMouseSpeed;
            @ChangeScenePressed.started += instance.OnChangeScenePressed;
            @ChangeScenePressed.performed += instance.OnChangeScenePressed;
            @ChangeScenePressed.canceled += instance.OnChangeScenePressed;
        }

        private void UnregisterCallbacks(IPlayerInputActions instance)
        {
            @ActionPressed.started -= instance.OnActionPressed;
            @ActionPressed.performed -= instance.OnActionPressed;
            @ActionPressed.canceled -= instance.OnActionPressed;
            @ActionHolding.started -= instance.OnActionHolding;
            @ActionHolding.performed -= instance.OnActionHolding;
            @ActionHolding.canceled -= instance.OnActionHolding;
            @ActionReleased.started -= instance.OnActionReleased;
            @ActionReleased.performed -= instance.OnActionReleased;
            @ActionReleased.canceled -= instance.OnActionReleased;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @MouseSpeed.started -= instance.OnMouseSpeed;
            @MouseSpeed.performed -= instance.OnMouseSpeed;
            @MouseSpeed.canceled -= instance.OnMouseSpeed;
            @ChangeScenePressed.started -= instance.OnChangeScenePressed;
            @ChangeScenePressed.performed -= instance.OnChangeScenePressed;
            @ChangeScenePressed.canceled -= instance.OnChangeScenePressed;
        }

        public void RemoveCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerInputActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerInputActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerInputActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnActionPressed(InputAction.CallbackContext context);
        void OnActionHolding(InputAction.CallbackContext context);
        void OnActionReleased(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseSpeed(InputAction.CallbackContext context);
        void OnChangeScenePressed(InputAction.CallbackContext context);
    }
}
