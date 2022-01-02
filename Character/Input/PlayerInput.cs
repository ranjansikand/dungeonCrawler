// GENERATED AUTOMATICALLY FROM 'Assets/Character/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""fe10e031-c8f0-44a9-9e86-8589b8cb7d44"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""194e4861-3d87-4fa1-ab49-fc68ee7dd697"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""64e7f107-c568-4067-942f-94ceb2ed67ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""8ef173de-c631-45ea-8f47-28b012f7d4f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quickstep"",
                    ""type"": ""Button"",
                    ""id"": ""de706667-2c41-4d28-8a08-2ff9a2e4e6fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d473b196-bcf7-422e-94a7-2008098a341c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""a0b434e0-c0c1-47d8-bd15-8fbbfc2b543f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""ba12b8fe-30b9-4a79-b3c2-4d4d37d69fcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraControls"",
                    ""type"": ""Value"",
                    ""id"": ""b44ebc7e-da63-4a3e-bcf7-b42eb883cb2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""c72cb6c1-b352-48be-a51a-89979d5344df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""b706083d-5bad-4f30-b706-6d9803c7fbd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b45f6146-292b-4b62-a928-08395f6daf7b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f06b1ebe-c106-411f-ba39-ea216dd5c33f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""257a8923-09fc-426e-89ec-7c91dfb1779d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cc0d27eb-9298-4f15-bdce-73d3ca4632d6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6debf723-41da-403c-af9a-872454bf2e59"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ce40a909-a993-484f-a945-66d31cc4e3c4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""333c50dd-7000-4a3f-bc3f-ba9031d26c42"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""706fe556-fb6a-4fbe-b62d-7a1d2ee3410b"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""260dc143-c3ee-493a-87ef-aecd2dc8d77d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fc9c6b3-ec66-41af-b542-baa0551c5b7f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb9f8da3-9369-406b-ab69-5646a04fd09b"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7d17d84-43e3-4f53-8d76-a1dc981cbb59"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbc595be-6f9d-46c2-8123-969fa8c76e53"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08080b5a-d1dc-4b59-bd91-e567216b7225"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eb8ec37-680d-45ba-981b-423446b6107e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1caf6983-5b1c-4358-80a1-5e6ab4e41caf"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9ae09ca-9be4-4b59-b575-e7baea133ff3"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d52dbe2-d8d7-43f5-9030-ee7a16735e16"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""043dd670-28f6-413d-a021-fcfe4e0d73d6"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a6380c9-f466-4215-8847-ccde323525db"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b8adabe-248e-448a-87eb-f2600ac2ef4c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18a9cfc7-93a0-43db-8a4a-8abb12bf7b61"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d9e7761-7941-46e1-aaaa-4724faeb170b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quickstep"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""673e7e86-9c4b-4843-a163-48ba1dec4a05"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quickstep"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Move = m_CharacterControls.FindAction("Move", throwIfNotFound: true);
        m_CharacterControls_Dodge = m_CharacterControls.FindAction("Dodge", throwIfNotFound: true);
        m_CharacterControls_Run = m_CharacterControls.FindAction("Run", throwIfNotFound: true);
        m_CharacterControls_Quickstep = m_CharacterControls.FindAction("Quickstep", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_CharacterControls_Menu = m_CharacterControls.FindAction("Menu", throwIfNotFound: true);
        m_CharacterControls_Attack = m_CharacterControls.FindAction("Attack", throwIfNotFound: true);
        m_CharacterControls_CameraControls = m_CharacterControls.FindAction("CameraControls", throwIfNotFound: true);
        m_CharacterControls_LockOn = m_CharacterControls.FindAction("LockOn", throwIfNotFound: true);
        m_CharacterControls_Block = m_CharacterControls.FindAction("Block", throwIfNotFound: true);
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

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Move;
    private readonly InputAction m_CharacterControls_Dodge;
    private readonly InputAction m_CharacterControls_Run;
    private readonly InputAction m_CharacterControls_Quickstep;
    private readonly InputAction m_CharacterControls_Jump;
    private readonly InputAction m_CharacterControls_Menu;
    private readonly InputAction m_CharacterControls_Attack;
    private readonly InputAction m_CharacterControls_CameraControls;
    private readonly InputAction m_CharacterControls_LockOn;
    private readonly InputAction m_CharacterControls_Block;
    public struct CharacterControlsActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterControls_Move;
        public InputAction @Dodge => m_Wrapper.m_CharacterControls_Dodge;
        public InputAction @Run => m_Wrapper.m_CharacterControls_Run;
        public InputAction @Quickstep => m_Wrapper.m_CharacterControls_Quickstep;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputAction @Menu => m_Wrapper.m_CharacterControls_Menu;
        public InputAction @Attack => m_Wrapper.m_CharacterControls_Attack;
        public InputAction @CameraControls => m_Wrapper.m_CharacterControls_CameraControls;
        public InputAction @LockOn => m_Wrapper.m_CharacterControls_LockOn;
        public InputAction @Block => m_Wrapper.m_CharacterControls_Block;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Dodge.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDodge;
                @Run.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Quickstep.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnQuickstep;
                @Quickstep.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnQuickstep;
                @Quickstep.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnQuickstep;
                @Jump.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Menu.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMenu;
                @Attack.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAttack;
                @CameraControls.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCameraControls;
                @CameraControls.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCameraControls;
                @CameraControls.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCameraControls;
                @LockOn.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLockOn;
                @Block.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnBlock;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Quickstep.started += instance.OnQuickstep;
                @Quickstep.performed += instance.OnQuickstep;
                @Quickstep.canceled += instance.OnQuickstep;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @CameraControls.started += instance.OnCameraControls;
                @CameraControls.performed += instance.OnCameraControls;
                @CameraControls.canceled += instance.OnCameraControls;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    public interface ICharacterControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnQuickstep(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnCameraControls(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
    }
}
