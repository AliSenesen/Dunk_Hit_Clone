using System;
using System.Collections;
using System.Collections.Generic;
using Gamemanager;
using UI;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool isOverTime = true;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputClose += OnInputClose;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputClose -= OnInputClose;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }


    private void Update()
    {
        OnInputTaken();
    }

    public void OnInputTaken()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isOverTime)
            {
                InputSignals.Instance.onInputTaken?.Invoke();
            }
        }
    }
    private void OnInputClose(bool _isOverTime)
    {
        isOverTime = _isOverTime;
    }
    
    private void OnReset()
    {
        isOverTime = true;
    }
}