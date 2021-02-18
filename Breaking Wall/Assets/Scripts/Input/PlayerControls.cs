// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""DefaultActionMap"",
            ""id"": ""6b87e932-dd9f-4224-a507-ff228fc5eba8"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""203e93ce-98c3-45ef-b30b-8d3999285e0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0f17abd1-f406-4e17-82d3-dd9edbfa326f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dive"",
                    ""type"": ""Button"",
                    ""id"": ""f16f3585-e047-4268-b833-18aef4242b3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hit"",
                    ""type"": ""Button"",
                    ""id"": ""2090ab44-95ff-458a-81dd-75f760d86244"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""d3a225ea-c215-4739-99d8-00a9a93db0fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""e72a8f33-4430-4643-9a18-e004461e5e40"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""782cb859-8931-403f-b49b-9edd1b332c83"",
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
                    ""id"": ""133ccefe-6417-4d7f-a011-d8244e71d61b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""633ee07d-4e45-418a-8b36-928ac3c8c153"",
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
                    ""id"": ""21a7b69b-1905-4531-ae72-156c8ca983e5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ca0a8557-9e4e-483e-922d-62f16f9669db"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c2f2b2e7-c814-465c-bc8e-dc210425a729"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b9ecaff5-ca65-42d5-8187-770069b46240"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""7d15ca73-9424-41c6-bfa1-e190b789f67d"",
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
                    ""id"": ""c8ce833a-7ef6-42fb-b072-a9b532ff9005"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c5f9d799-1611-4367-a918-3e497ee88015"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""60bc22e9-9039-440e-86eb-249bc4eb2918"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4abadfb4-77f3-49e6-9a3a-aa7e6c481f6f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""685b61eb-3b89-48c3-9af5-97ca7c5385e2"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f48a1b61-e080-412c-bf1f-6c5e5552d04c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c88a3c6-3fa5-4d26-9abb-4b5ba2bc3162"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb133722-8d87-4e57-8875-32a4f1a690b0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30d8c7a6-534f-4235-99c3-a85b877a8415"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GameCS"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8283380d-2294-4d04-a229-32963d46ba2b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GameCS"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""290e5d33-8efb-43f8-adf5-aab213d5c1bc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82eacd7a-9035-4dfb-afd5-9542f8c3b825"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GameCS"",
            ""bindingGroup"": ""GameCS"",
            ""devices"": []
        }
    ]
}");
        // DefaultActionMap
        m_DefaultActionMap = asset.FindActionMap("DefaultActionMap", throwIfNotFound: true);
        m_DefaultActionMap_Jump = m_DefaultActionMap.FindAction("Jump", throwIfNotFound: true);
        m_DefaultActionMap_Movement = m_DefaultActionMap.FindAction("Movement", throwIfNotFound: true);
        m_DefaultActionMap_Dive = m_DefaultActionMap.FindAction("Dive", throwIfNotFound: true);
        m_DefaultActionMap_Hit = m_DefaultActionMap.FindAction("Hit", throwIfNotFound: true);
        m_DefaultActionMap_Interaction = m_DefaultActionMap.FindAction("Interaction", throwIfNotFound: true);
        m_DefaultActionMap_Aim = m_DefaultActionMap.FindAction("Aim", throwIfNotFound: true);
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

    // DefaultActionMap
    private readonly InputActionMap m_DefaultActionMap;
    private IDefaultActionMapActions m_DefaultActionMapActionsCallbackInterface;
    private readonly InputAction m_DefaultActionMap_Jump;
    private readonly InputAction m_DefaultActionMap_Movement;
    private readonly InputAction m_DefaultActionMap_Dive;
    private readonly InputAction m_DefaultActionMap_Hit;
    private readonly InputAction m_DefaultActionMap_Interaction;
    private readonly InputAction m_DefaultActionMap_Aim;
    public struct DefaultActionMapActions
    {
        private @PlayerControls m_Wrapper;
        public DefaultActionMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_DefaultActionMap_Jump;
        public InputAction @Movement => m_Wrapper.m_DefaultActionMap_Movement;
        public InputAction @Dive => m_Wrapper.m_DefaultActionMap_Dive;
        public InputAction @Hit => m_Wrapper.m_DefaultActionMap_Hit;
        public InputAction @Interaction => m_Wrapper.m_DefaultActionMap_Interaction;
        public InputAction @Aim => m_Wrapper.m_DefaultActionMap_Aim;
        public InputActionMap Get() { return m_Wrapper.m_DefaultActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActionMapActions instance)
        {
            if (m_Wrapper.m_DefaultActionMapActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnMovement;
                @Dive.started -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnDive;
                @Dive.performed -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnDive;
                @Dive.canceled -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnDive;
                @Hit.started -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnHit;
                @Hit.performed -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnHit;
                @Hit.canceled -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnHit;
                @Interaction.started -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnInteraction;
                @Aim.started -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_DefaultActionMapActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_DefaultActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Dive.started += instance.OnDive;
                @Dive.performed += instance.OnDive;
                @Dive.canceled += instance.OnDive;
                @Hit.started += instance.OnHit;
                @Hit.performed += instance.OnHit;
                @Hit.canceled += instance.OnHit;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public DefaultActionMapActions @DefaultActionMap => new DefaultActionMapActions(this);
    private int m_GameCSSchemeIndex = -1;
    public InputControlScheme GameCSScheme
    {
        get
        {
            if (m_GameCSSchemeIndex == -1) m_GameCSSchemeIndex = asset.FindControlSchemeIndex("GameCS");
            return asset.controlSchemes[m_GameCSSchemeIndex];
        }
    }
    public interface IDefaultActionMapActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnDive(InputAction.CallbackContext context);
        void OnHit(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
