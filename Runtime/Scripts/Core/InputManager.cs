using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if NEW_INPUT_SYSTEM
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
#endif

namespace Stax3.Plugins.InputSystem
{
    [CreateAssetMenu(fileName = "Input Manager", menuName = "Stax3/Plugins/Input Manager", order = 0)]
    public class InputManager : ScriptableObject
    {
        #region Creating Instance
        private static InputManager m_instance;

        public static InputManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = (InputManager)Resources.Load("Input Manager");
                }
                return m_instance;
            }
        }
        #endregion

        [SerializeField] List<ScriptableObject> m_actions;

#if NEW_INPUT_SYSTEM
        private PlayerInput m_playerInput;
#endif
        private InputMonoUpdate m_monoUpdate;
        private bool m_isInitialized;

        public InputMonoUpdate monoUpdate => m_monoUpdate;

        public void Init()
        {
#if NEW_INPUT_SYSTEM
            m_playerInput = new PlayerInput();
            m_playerInput.Enable();
#endif

            m_monoUpdate = new GameObject("Input Mono Update", new Type[] { typeof(InputMonoUpdate) }).GetComponent<InputMonoUpdate>();
            m_actions.ForEach(x => ((Input_Action)x).Init(
#if NEW_INPUT_SYSTEM
                m_playerInput
#endif
));
            m_isInitialized = true;
        }

        private void OnDisable()
        {
            if (m_isInitialized)
            {
#if NEW_INPUT_SYSTEM
            if (m_playerInput != null)
            {
                m_playerInput.Disable();
            }
#endif
                m_actions.ForEach(x => ((Input_Action)x).UnSubscribeEvents());
            }
            m_isInitialized = false;
        }

        public Input_Action GetAction(string name)
        {
            foreach (ScriptableObject action in m_actions)
            {
                if (((Input_Action)action).bindingName == name)
                    return ((Input_Action)action);
            }
            return default;
        }
    }
}
