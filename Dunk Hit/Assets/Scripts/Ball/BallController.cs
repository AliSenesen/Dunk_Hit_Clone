using System;
using Basket;
using Enums;
using UnityEngine;

namespace Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;

        private InputManager _ınputManager;
        private GameStates _currentState;
        private DirectionStates _directionStates;

        private bool isEnter = false;
        private bool isInside = false;
        private bool isOut = false;

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
            rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        void RightDirection()
        {
            Direction = (_directionStates == DirectionStates.Right) ? 1 : -1;
            rigidbody.AddForce(new Vector2((float)Direction * Speed, 0f));
        }

        void LeftDirection()
        {
            Direction = (_directionStates == DirectionStates.Left) ? -1 : 1;
            rigidbody.AddForce(new Vector2((float)Direction * Speed, 0f));
        }

        public void ChangeDirection()
        {
            if (_directionStates == DirectionStates.Right)
            {
                _directionStates = DirectionStates.Left;
                LeftDirection();
                BasketSignals.Instance.onChangeBasketDirection?.Invoke();
            }
            else if (_directionStates == DirectionStates.Left)
            {
                _directionStates = DirectionStates.Right;
                RightDirection();
                BasketSignals.Instance.onChangeBasketDirection?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enter"))
            {
                isEnter = true;
            }

            if (other.CompareTag("In"))
            {
                isInside = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("RightWall"))
            {
                if (Direction == 1)

                {
                    transform.position = new Vector2(-other.transform.position.x, transform.position.y);
                }
            }
            else if (other.CompareTag("LeftWall"))
            {
                if (Direction == -1)
                {
                    transform.position = new Vector2(-other.transform.position.x, transform.position.y);
                }
            }

            if (other.CompareTag("Out"))
            {
                if (isEnter == true && isInside == true)
                {
                    isEnter = false;
                    isInside = false;
                    ChangeDirection();
                }
            }
        }
    }
}