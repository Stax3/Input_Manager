using UnityEngine;

namespace Stax3.Plugins.InputSystem
{
    [CreateAssetMenu(fileName = "Button Action", menuName = "Stax3/Plugins/Actions/Button Action")]
    public class ButtonAction : Input_Action
    {
        [SerializeField] private KeyCode key;

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

        protected override void OnNewInputPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            m_newInputData.performed = true;
            CallbackData data = m_newInputData;
            data.started = false;
            InvokeCallBack(data);
        }

        private void CheckInputInUpdate()
        {
            CallbackData data = new CallbackData();
            if (Input.GetKeyDown(key) && m_newInputData.started == false)
            {
                data.started = true;
                InvokeCallBack(data);
            }
            else if (Input.GetKey(key) && m_newInputData.performed == false)
            {
                data.performed = true;
                InvokeCallBack(data);
            }
            if (Input.GetKeyUp(key) && m_newInputData.canceled == false)
            {
                data.canceled = true;
                InvokeCallBack(data);
            }
        }
    }
}
