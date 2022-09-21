//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Input/InputActions.inputactions
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

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""HumanoidLand"",
            ""id"": ""270fa713-f97c-4409-aa6b-3d204b49f35a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""99c25641-ebe0-47c0-8e3e-16b2661e0bc4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""bc452587-84e4-40a1-83b7-1e051bb555e5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangeCamera"",
                    ""type"": ""Button"",
                    ""id"": ""0e8f8f6c-b3bc-491b-b7e6-c1d9ab455dcf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ZoomCamera"",
                    ""type"": ""Value"",
                    ""id"": ""39858b5d-f7d0-493d-9708-c91a2b905a73"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""457b5dde-4367-45df-a439-247f0237c962"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""06e913ea-b4e9-48e4-9bbb-23f8fd1a7290"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""a7943437-bf96-4e3d-8e68-2d421fa00869"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""77b6b2b6-b45c-47b5-976f-621998d3c66e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""949e2e53-c99f-4b46-91f4-2490a99c981a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.03,y=0.03)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""727efb65-0ad0-4fed-9204-49550203ae1b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""ce55b652-713e-4ebb-a38d-af16d70fc9d5"",
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
                    ""id"": ""324a8541-f2aa-44cc-831e-bbcca894fb5f"",
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
                    ""id"": ""703d98f2-bd9f-4a39-9581-2329cab87ca8"",
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
                    ""id"": ""5c2ac40b-d8e8-4757-b4e1-dcab118d88e5"",
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
                    ""id"": ""abce3e04-4ab1-4ddd-ab31-566f50b1989c"",
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
                    ""id"": ""1d546ba2-d5f6-432d-9760-20afe675c7a7"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31a0fd89-7171-4787-8586-5f1cb1668edd"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a94d5651-0c0b-428d-9ab3-110884a6e431"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""225ddb26-405c-47fb-abe2-0e57b870c935"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-4,max=4)"",
                    ""groups"": """",
                    ""action"": ""ZoomCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9f02714-091e-4108-a877-5d3f6eb7b37d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e950b18b-bbcb-464f-9143-2233b1620f36"",
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
                    ""id"": ""fd94b9cc-c88a-4d00-b68e-4596c2178878"",
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
                    ""id"": ""24f9cb2f-b1a1-4f14-b6ec-732ca9ccc141"",
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
                    ""id"": ""796deb85-61ee-4d62-8760-0ef4508d0f0a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fef84dc-bea1-4900-9a71-50bf864bcf34"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // HumanoidLand
        m_HumanoidLand = asset.FindActionMap("HumanoidLand", throwIfNotFound: true);
        m_HumanoidLand_Move = m_HumanoidLand.FindAction("Move", throwIfNotFound: true);
        m_HumanoidLand_Look = m_HumanoidLand.FindAction("Look", throwIfNotFound: true);
        m_HumanoidLand_ChangeCamera = m_HumanoidLand.FindAction("ChangeCamera", throwIfNotFound: true);
        m_HumanoidLand_ZoomCamera = m_HumanoidLand.FindAction("ZoomCamera", throwIfNotFound: true);
        m_HumanoidLand_Run = m_HumanoidLand.FindAction("Run", throwIfNotFound: true);
        m_HumanoidLand_Jump = m_HumanoidLand.FindAction("Jump", throwIfNotFound: true);
        m_HumanoidLand_Crouch = m_HumanoidLand.FindAction("Crouch", throwIfNotFound: true);
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

    // HumanoidLand
    private readonly InputActionMap m_HumanoidLand;
    private IHumanoidLandActions m_HumanoidLandActionsCallbackInterface;
    private readonly InputAction m_HumanoidLand_Move;
    private readonly InputAction m_HumanoidLand_Look;
    private readonly InputAction m_HumanoidLand_ChangeCamera;
    private readonly InputAction m_HumanoidLand_ZoomCamera;
    private readonly InputAction m_HumanoidLand_Run;
    private readonly InputAction m_HumanoidLand_Jump;
    private readonly InputAction m_HumanoidLand_Crouch;
    public struct HumanoidLandActions
    {
        private @InputActions m_Wrapper;
        public HumanoidLandActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_HumanoidLand_Move;
        public InputAction @Look => m_Wrapper.m_HumanoidLand_Look;
        public InputAction @ChangeCamera => m_Wrapper.m_HumanoidLand_ChangeCamera;
        public InputAction @ZoomCamera => m_Wrapper.m_HumanoidLand_ZoomCamera;
        public InputAction @Run => m_Wrapper.m_HumanoidLand_Run;
        public InputAction @Jump => m_Wrapper.m_HumanoidLand_Jump;
        public InputAction @Crouch => m_Wrapper.m_HumanoidLand_Crouch;
        public InputActionMap Get() { return m_Wrapper.m_HumanoidLand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HumanoidLandActions set) { return set.Get(); }
        public void SetCallbacks(IHumanoidLandActions instance)
        {
            if (m_Wrapper.m_HumanoidLandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnLook;
                @ChangeCamera.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnChangeCamera;
                @ChangeCamera.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnChangeCamera;
                @ChangeCamera.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnChangeCamera;
                @ZoomCamera.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnZoomCamera;
                @ZoomCamera.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnZoomCamera;
                @ZoomCamera.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnZoomCamera;
                @Run.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_HumanoidLandActionsCallbackInterface.OnCrouch;
            }
            m_Wrapper.m_HumanoidLandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @ChangeCamera.started += instance.OnChangeCamera;
                @ChangeCamera.performed += instance.OnChangeCamera;
                @ChangeCamera.canceled += instance.OnChangeCamera;
                @ZoomCamera.started += instance.OnZoomCamera;
                @ZoomCamera.performed += instance.OnZoomCamera;
                @ZoomCamera.canceled += instance.OnZoomCamera;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
            }
        }
    }
    public HumanoidLandActions @HumanoidLand => new HumanoidLandActions(this);
    public interface IHumanoidLandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnChangeCamera(InputAction.CallbackContext context);
        void OnZoomCamera(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
    }
}
