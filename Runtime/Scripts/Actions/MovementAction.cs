using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stax3.Plugins.InputSystem
{
    [CreateAssetMenu(fileName = "Movement Action", menuName = "Stax3/Plugins/Actions/Movement Action")]
    public class MovementAction : Input_Action
    {
        private Vector2 m_vector2Input = Vector2.zero;
        private bool invokeOnceOnZero;
        protected override void SubscribeEvents()
        {
            InputManager.Instance.monoUpdate.update += CheckifInputSuccess;
        }
        public override void UnSubscribeEvents()
        {
            InputManager.Instance.monoUpdate.update -= CheckifInputSuccess;
        }
        private void CheckifInputSuccess()
        {
            m_vector2Input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (m_vector2Input.sqrMagnitude > 0)
            {
                callback?.Invoke(new CallbackData { vector2Input = m_vector2Input });
                invokeOnceOnZero = true;
            }
            else if (invokeOnceOnZero)
            {
                callback?.Invoke(new CallbackData { vector2Input = m_vector2Input });
                invokeOnceOnZero = false;
            }
        }
    }
}
