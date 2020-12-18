// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerMove"",
            ""id"": ""50bd6f66-d99f-4f5b-918c-7fe70db51455"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""68151b64-a6ad-4390-8859-0ac830f6a68a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f028f92a-71b8-49dd-9eab-b65ed61c7510"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WSAD"",
                    ""id"": ""5bf1b69f-c358-4671-ae69-49000c6c30de"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""00ddc7eb-a908-4446-8f03-c0de1951abc9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""914512d6-31c6-4840-8a38-cfaf126e4331"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""224bb06c-51f4-451a-9ae6-a0acb4719a18"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d7ec64c8-eb7d-4e2a-97df-95dc7a96a151"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""83d7e9db-53c8-4116-ad21-274630eb14a3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
            ""devices"": []
        }
    ]
}");
        // PlayerMove
        m_PlayerMove = asset.FindActionMap("PlayerMove", throwIfNotFound: true);
        m_PlayerMove_Movement = m_PlayerMove.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMove_Jump = m_PlayerMove.FindAction("Jump", throwIfNotFound: true);
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

    // PlayerMove
    private readonly InputActionMap m_PlayerMove;
    private IPlayerMoveActions m_PlayerMoveActionsCallbackInterface;
    private readonly InputAction m_PlayerMove_Movement;
    private readonly InputAction m_PlayerMove_Jump;
    public struct PlayerMoveActions
    {
        private @InputActions m_Wrapper;
        public PlayerMoveActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMove_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerMove_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMoveActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMoveActions instance)
        {
            if (m_Wrapper.m_PlayerMoveActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerMoveActions @PlayerMove => new PlayerMoveActions(this);
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    public interface IPlayerMoveActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
