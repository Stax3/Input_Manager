using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stax3.Plugins.InputSystem
{
    [CreateAssetMenu(fileName = "Button Action", menuName = "Stax3/Plugins/Actions/Button Action")]
    public class ButtonAction : Input_Action
    {
        [SerializeField] KeyCode key;
        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            if (key != KeyCode.None)
                InputManager.Instance.monoUpdate.update += CheckInputInUpdate;
        }
        public override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            if (key != KeyCode.None)
                InputManager.Instance.monoUpdate.update -= CheckInputInUpdate;
        }
        private void CheckInputInUpdate()
        {
            CallbackData data = new CallbackData();
            if (Input.GetKeyDown(key))
                data.started = true;
            else if (Input.GetKey(key))
                data.started = !(data.performed = true);
            if (Input.GetKeyUp(key))
                data.performed = !(data.canceled = true);
            InvokeCallBack(data);
        }
    }
}
