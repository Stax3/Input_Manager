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
        protected override void SubscribeEvents()
        {
            //action.started += (context) =>
            //{
            //    m_vector2Input = context.ReadValue<Vector2>();
            //    callback?.Invoke(new CallbackData { vector2Input = m_vector2Input });
            //};
            //action.canceled += (context) =>
            //{
            //    m_vector2Input = Vector2.zero;
            //    callback?.Invoke(new CallbackData { vector2Input = m_vector2Input });
            //};
            //InputManager.Instance.monoUpdate.update += CheckifInputSuccess;
            InputManager.Instance.monoUpdate.update += CheckifInputSuccess;
        }
        public override void UnSubscribeEvents()
        {
            //base.UnSubscribeEvents();
            //InputManager.Instance.monoUpdate.update -= CheckifInputSuccess;
            InputManager.Instance.monoUpdate.update -= CheckifInputSuccess;
        }
        private void CheckifInputSuccess()
        {
            //if (m_vector2Input.sqrMagnitude == 0)
            //{
            //}
            callback?.Invoke(new CallbackData { vector2Input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) });
        }
    }
}
