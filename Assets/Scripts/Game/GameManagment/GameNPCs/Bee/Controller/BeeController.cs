using UnityEngine;
using Common;

namespace Game
{
    public class BeeController : MonoBehaviour, INpc
    {
        private StateMachine _stateMachine;
        [SerializeField] private Animator _animator;
        public NpcBehaviors NpcBehaviors { get; set; }
        private float _movingSpeed;
        private float _fastMovingSpeed;
        private float _searchRadius;
        private float _attackRadius;
        private float _period;
        private float _patrollingRotation;

        private void Awake()
        {
            _stateMachine = new StateMachine();
        }
        void Start()
        {
            GetLavelData();
            _stateMachine.SetState(new BeePatrol(transform, _animator, _movingSpeed));
        }
        public void GetLavelData()
        {
            _movingSpeed = NpcBehaviors.MovingSpeed;
            _fastMovingSpeed = NpcBehaviors.FastMovingSpeed;
            _searchRadius = NpcBehaviors.SearchRadius;
            _attackRadius = NpcBehaviors.AttackRadios;
            _period = NpcBehaviors.PatrollingStandingPeriod;
            _patrollingRotation = NpcBehaviors.PatrollingRotation;
        }

        private Vector3 _playerPosition;
        private Vector3 _foodPosition;
        private float _playerDistance;
        private float _foodDistance;
        private float _nextActionTime = 0.0f;
        private bool _stroll = false;
        private bool _isPlayer = false;


        void Update()
        {
            _stateMachine.Execute();

            // search for food and player
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, _searchRadius);
            for (int i = 0; i < hitObjects.Length; i++)
            {
                if (hitObjects[i].CompareTag("Player"))
                    _playerPosition = hitObjects[i].gameObject.transform.position;
            }

            BeeBrain(_playerPosition);
            _playerPosition = Vector3.zero;
            Vector3Utility.Clear();

            if (Time.time > _nextActionTime)
            {
                _nextActionTime += _period;
                _stroll = !_stroll;
            }
        }


        private void BeeBrain(Vector3 _playerPosition)
        {
            if (_playerPosition == Vector3.zero)// -> no player found
            {
                BeePatrol();
            }
            else // -> player found 
            {
                _playerDistance = (transform.position - _playerPosition).magnitude;
                if (_playerDistance > 0.5f)
                    _isPlayer = false;
                if (_isPlayer)
                    Attack();
                else // -> get to player 
                {                    
                    if (_playerDistance < 2 && _playerPosition != Vector3.zero)
                        MoveFast(_playerPosition);
                }
            }
        }




        #region States functions
        private void BeePatrol()
        {
            if (_stateMachine.GetCurrentState() != "Game.BeePatrol")
                _stateMachine.SetState(new BeePatrol(transform, _animator, _movingSpeed));
        }
        private void MoveFast(Vector3 _playerPosition)
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeMoveFast")
                _stateMachine.SetState(new BeeMoveFast(transform, _animator, _fastMovingSpeed));
            NPCRotateToTarget(_playerPosition);
            //_isPlayer = false;
        }

        //private void PatrolOrStand()
        //{
        //    if (_stateMachine.GetCurrentState() != "Game.BeePatrol")
        //        _stateMachine.SetState(new BeePatrol(transform, _animator, _movingSpeed));
        //}

        private void Walk(Vector3 FoodrPos)
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeMove")
                _stateMachine.SetState(new BeeMove(transform, _animator, _movingSpeed));
            NPCRotateToTarget(FoodrPos);
        }

        //private void EatOrStand()
        //{
        //    if (_stateMachine.GetCurrentState() != "Game.Eating" && !_stroll)
        //        _stateMachine.SetState(new Eating(transform, _animator, _movingSpeed));
        //    if (_stateMachine.GetCurrentState() != "Game..Standing" && _stroll)
        //        _stateMachine.SetState(new Standing(transform, _animator, _movingSpeed));
        //}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
                _isPlayer = true;            
        }

        private void Attack()
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeAttack")
                _stateMachine.SetState(new BeeAttack(transform, _animator));
        }

        private void NPCRotateToTarget(Vector3 TargetPosition)
        {
            var lookPos = transform.position - TargetPosition;
            lookPos.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(-lookPos);
        }

        #endregion
    }
}
