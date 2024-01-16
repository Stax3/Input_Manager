using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Stax3.Plugins.InputSystem
{
    [CreateAssetMenu(fileName = "Movement Action", menuName = "Stax3/Plugins/Actions/Movement Action")]
    public class MovementAction : Input_Action
    {
        private Vector2 m_newInputData = Vector2.zero;
        private Vector2 m_vector2Input = Vector2.zero;
        private bool invokeOnceOnZero;

        protected override void SubscribeEvents()
        {
            action.performed += (context) => { m_newInputData = context.ReadValue<Vector2>(); };
            action.canceled += OnNewInputCanceled;
            InputManager.Instance.monoUpdate.update += CheckifInputSuccess;
        }

        public override void UnSubscribeEvents()
        {
            action.performed -= (context) => { m_newInputData = context.ReadValue<Vector2>(); };
            action.canceled -= OnNewInputCanceled;
            InputManager.Instance.monoUpdate.update -= CheckifInputSuccess;
        }

        private void OnNewInputCanceled(CallbackContext context)
        {
            if (m_newInputData.sqrMagnitude > 0)
            {
                m_newInputData = Vector2.zero;
                InvokeCallBack(new CallbackData { vector2Input = m_newInputData });
            }
        }

        private void CheckifInputSuccess()
        {
            if (m_newInputData.sqrMagnitude > 0)
            {
                InvokeCallBack(new CallbackData { vector2Input = m_newInputData });
                return;
            }
            m_vector2Input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (m_vector2Input.sqrMagnitude > 0)
            {
                InvokeCallBack(new CallbackData { vector2Input = m_vector2Input });
                invokeOnceOnZero = true;
            }
            else if (invokeOnceOnZero)
            {
                InvokeCallBack(new CallbackData { vector2Input = m_vector2Input });
                invokeOnceOnZero = false;
            }
        }

        private void OnDisable()
        {
            m_newInputData = m_vector2Input = Vector2.zero;
        }
    }
}
