using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

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
        vector2Input = Vector2.zero;
        //vector2Input = context.ReadValue<Vector2>();
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

