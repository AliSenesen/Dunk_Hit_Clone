using System;
using Enums;
using UnityEngine;

namespace Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallManager ballManager;
        [SerializeField] private Rigidbody2D rigidbody;
       
        private InputManager _ınputManager;
        private GameStates _currentState;
        private DirectionStates _directionStates;
        
        public float Speed;
        public float JumpForce;
        public int Direction = 1;


        private void Awake()
        {
            rigidbody.isKinematic = true;
            _directionStates = DirectionStates.Right;
            _currentState = GameStates.GameOpen;
            rigidbody.velocity = new Vector2(Speed * Direction * Time.deltaTime, rigidbody.velocity.y);
        }

        private void FixedUpdate()
        {
            if (_directionStates == DirectionStates.Right)
            {
                RightDirection();
            }
           if (_directionStates == DirectionStates.Left)
           {
               LeftDirection();
           }

          
        }
        public void Jump()

        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(Vector2.up * JumpForce,ForceMode2D.Impulse);
        }

        void RightDirection()
        {
            Direction = (_directionStates == DirectionStates.Right) ? 1 : -1;
            rigidbody.AddForce(new Vector2((float)Direction*Speed,0f));
        }

        void LeftDirection()
        {
            Direction = (_directionStates == DirectionStates.Left) ? -1 : 1;
            rigidbody.AddForce(new Vector2((float)Direction*Speed,0f));
        }

        public void ChangeDirection()
        {
            if (_directionStates == DirectionStates.Right )
            {
                _directionStates = DirectionStates.Left;
                LeftDirection();
            }
            else if (_directionStates == DirectionStates.Left)
            {
                _directionStates = DirectionStates.Right;
                RightDirection();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("In"))
            {
                ChangeDirection();
            }
        }
    }
}