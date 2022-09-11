using DefaultNamespace.StateMachine;
using System;
using UnityEngine;

namespace DefaultNamespace.Enemies.Spider.SpiderStateMachine
{
    [RequireComponent(typeof(SpiderMoving))]
    public class SpiderStateManager : StateManager
    {
        public enum MoveDirections
        {
            Right = 1,
            Stay = 0,
            Left = -1
        }

        private Rigidbody2D _rb;
        private SpiderMoving _spiderMoving;

        private float _currentSpeed;
        private MoveDirections _currentMoveDirection = MoveDirections.Stay;

        public LayerMask GroundLayer { get; private set; }

        [Header("Moving")]
        public float maxJumpDistance = 10f;
        public float jumpForce = 20f;
    
        [Header("Web making state")]
        private SpiderWebMakingState _webMakingState;
        public float WebMakingStateSpeed;
        [Range(0, 1)] public float chanceToJump;
        


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
            _spiderMoving = GetComponent<SpiderMoving>();
            GroundLayer = _spiderMoving.GroundLayer;
            _webMakingState = new SpiderWebMakingState();
            
            SetState(_webMakingState);
        }

        protected override void Update()
        {

            
            base.Update();
        }

        public void JumpAndMakeWeb(Vector2 direction, float force)
        {
            SetMoveDirection(MoveDirections.Stay);
            StartCoroutine(_spiderMoving.JumpAndMakeWeb(direction, force));
        } 

        public void SetSpeed(float value)
        {
            _currentSpeed = value;
            _spiderMoving.speed = _currentSpeed;
        }

        public void SetMoveDirection(MoveDirections newDirection)
        {
            _currentMoveDirection = newDirection;
            _spiderMoving.CurrentMoveDirection = (int)_currentMoveDirection;
        }

        public Vector2 GetUpwardVector() => _spiderMoving.Upward;

        public bool Climbing() => _spiderMoving.Climbing;

        public bool IsOnChasm() => _spiderMoving.OnChasm;

        public bool Jumping() => _spiderMoving.Jumping;
    }
}