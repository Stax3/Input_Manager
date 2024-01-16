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

        protected CallbackData m_newInputData;
#if NEW_INPUT_SYSTEM
        protected InputAction action;
#endif

        public string bindingName => m_bindingName;
        public event Action<CallbackData> callback;

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

        protected void InvokeCallBack(CallbackData data) => callback?.Invoke(data);

        protected virtual void SubscribeEvents()
        {
#if NEW_INPUT_SYSTEM
            action.started += OnNewInputStarted;
            action.performed += OnNewInputPerformed;
            action.canceled += OnNewInputCanceled;
#endif
        }

        public virtual void UnSubscribeEvents()
        {
#if NEW_INPUT_SYSTEM
            action.started -= OnNewInputStarted;
            action.performed -= OnNewInputPerformed;
            action.canceled -= OnNewInputCanceled;
#endif
            callback = null;
        }

        #region new input callbacks
#if NEW_INPUT_SYSTEM
        protected virtual void OnNewInputStarted(CallbackContext context)
        {
            m_newInputData = new CallbackData(context);
            callback?.Invoke(m_newInputData);
        }
        protected virtual void OnNewInputPerformed(CallbackContext context)
        {
            m_newInputData = new CallbackData(context);
            callback?.Invoke(m_newInputData);
        }
        protected virtual void OnNewInputCanceled(CallbackContext context)
        {
            m_newInputData = new CallbackData(context);
            callback?.Invoke(m_newInputData);
        }
#endif
        #endregion
    }
}