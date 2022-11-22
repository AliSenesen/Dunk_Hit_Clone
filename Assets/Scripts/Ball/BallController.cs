using System;
using Basket;
using Enums;
using UI;
using UnityEngine;


namespace Ball
{
    public class BallController : MonoBehaviour
    {
        public int Direction = 1;
       
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] float Speed;
        [SerializeField] float JumpForce;
     

        private GameStates _currentState;
        private DirectionStates _directionStates;

        private bool _isEnter;
        private bool _isInside;
        private bool _isOut;

      


        private void Awake()
        {
            rigidbody.isKinematic = true;
            _directionStates = DirectionStates.Right;
            _currentState = GameStates.Playing;
            rigidbody.velocity = new Vector2(Speed * Direction * Time.deltaTime, rigidbody.velocity.y);
        }

        private void FixedUpdate()
        {
            CloseTrailRenderer();

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

        private void CloseTrailRenderer()
        {
            if (rigidbody.transform.position.x < -2.3f || rigidbody.transform.position.x > 2.3f)
            {
                trailRenderer.time = 0;
            }
            else
            {
                trailRenderer.time = .3f;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enter"))
            {
                _isEnter = true;
            }

            if (other.CompareTag("In"))
            {
                _isInside = true;
            }

            if (other.CompareTag("Ground"))
            {
                UISignals.Instance.onCheckOverTime?.Invoke();
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
                if (_isEnter == true && _isInside == true)
                {
                    _isEnter = false;
                    _isInside = false;
                    ChangeDirection();
                }
            }
        }

        public void ResetBall()
        {
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.isKinematic = true;
            gameObject.transform.position = Vector3.zero;
        }
    }
}