using Gamemanager;
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
            CoreGameSignals.Instance.onReset += OnReset;

            InputSignals.Instance.onInputTaken += OnBallMove;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;

            InputSignals.Instance.onInputTaken -= OnBallMove;
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnBallMove()
        {
            controller.Jump();
        }

        private void OnReset()
        {
            controller.ResetBall();
        }
    }
}