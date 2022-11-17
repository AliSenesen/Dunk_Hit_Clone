using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Gamemanager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameStates CurrentState;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
       
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void OnChangeGameState(GameStates currentState)
    {
        CurrentState = currentState;
    }
  
    private void OnPlay()
    {
        CurrentState = GameStates.GameOpen;
    }
}


