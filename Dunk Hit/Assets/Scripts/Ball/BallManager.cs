using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class BallManager : MonoBehaviour
    {
        [SerializeField] private BallController controller;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += BallMove;
        }
        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= BallMove;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void BallMove()
        {
            controller.Jump();
            
        }
       
    }
}

