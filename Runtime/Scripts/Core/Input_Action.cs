using UnityEngine;
using System;
#if NEW_INPUT_SYSTEM
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
#endif
namespace Stax3.Plugins.InputSystem
{
    public class Input_Action : ScriptableObject
    {
        [Header("Name Must be equal to the new Input system action name.")]
        [SerializeField] private string m_bindingName;
#if NEW_INPUT_SYSTEM
        protected InputAction action;
#endif
        public string bindingName => m_bindingName;
        public Action<CallbackData> callback;

        public void Init(
#if NEW_INPUT_SYSTEM
            PlayerInput playerInput
#endif
            )
        {
#if NEW_INPUT_SYSTEM
            action = playerInput.FindAction(m_bindingName);
#endif
            SubscribeEvents();
        }

        protected virtual void SubscribeEvents()
        {
#if NEW_INPUT_SYSTEM
            action.started += (context) => callback?.Invoke(new CallbackData(context));
            action.performed += (context) => callback?.Invoke(new CallbackData(context));
            action.canceled += (context) => callback?.Invoke(new CallbackData(context));
#endif
        }

        public virtual void UnSubscribeEvents()
        {
#if NEW_INPUT_SYSTEM
            action.started -= (context) => callback?.Invoke(new CallbackData(context));
            action.performed -= (context) => callback?.Invoke(new CallbackData(context));
            action.canceled -= (context) => callback?.Invoke(new CallbackData(context));
#endif
            callback = null;
        }
    }
    public struct CallbackData
    {
        public bool started;
        public bool performed;
        public bool canceled;
        public Vector2 vector2Input;

#if NEW_INPUT_SYSTEM
        public CallbackData(CallbackContext context)
        {
            started = context.started;
            performed = context.performed;
            canceled = context.canceled;
            vector2Input = context.ReadValue<Vector2>();
        }
#endif
        public CallbackData(CallbackData data)
        {
            started = data.started;
            performed = data.performed;
            canceled = data.canceled;
            vector2Input = data.vector2Input;
        }
    }
}