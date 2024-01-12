using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMonoUpdate : MonoBehaviour
{
    public event Action update;//= new List<Action>();
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        update?.Invoke();
        //update.ForEach(x => x?.Invoke());
    }
}
