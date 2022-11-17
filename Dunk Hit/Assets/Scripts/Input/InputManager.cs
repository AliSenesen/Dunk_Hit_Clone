using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private void Update()
    {
        OnInputTaken();
    }

    public void OnInputTaken()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputSignals.Instance.onInputTaken?.Invoke();
        }
    }
   
}
