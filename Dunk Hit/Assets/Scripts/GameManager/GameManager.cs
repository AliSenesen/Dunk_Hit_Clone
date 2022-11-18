using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Gamemanager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStates _currentState;

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
    private void OnChangeGameState(GameStates gameState)
    {
        _currentState = gameState;
    }
  
    private void OnPlay()
    {
        _currentState = GameStates.GameOpen;
    }
}


