using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMonoUpdate : MonoBehaviour
{
    public event Action update;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        update?.Invoke();
    }
}
