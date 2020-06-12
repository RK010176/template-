using UnityEngine;
using Common;

namespace Game
{
    public class BeeController : MonoBehaviour, INpc
    {
        public NpcBehaviors NpcBehaviors { get; set; }
        private StateMachine _stateMachine;
        private Animator _animator;        
        private AudioSource _audioSource;
        [SerializeField] private FloatVal _health;
        private void Awake()
        {
            _stateMachine = new StateMachine();
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();            
        }
        void Start()
        {            
            _stateMachine.SetState(new BeePatrol(transform, _animator, NpcBehaviors.MovingSpeed));
            Play1();
            _health.Value = NpcBehaviors.Health;
        }
        public void GetLavelData(){}

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
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, NpcBehaviors.SearchRadius);
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
                _nextActionTime += NpcBehaviors.PatrollingStandingPeriod;
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
            { _stateMachine.SetState(new BeePatrol(transform, _animator, NpcBehaviors.MovingSpeed)); Play1(); }
        }
        private void MoveFast(Vector3 _playerPosition)
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeMoveFast")
            { _stateMachine.SetState(new BeeMoveFast(transform, _animator, NpcBehaviors.FastMovingSpeed)); Play2(); }
            NPCRotateToTarget(_playerPosition);           
        }
        private void Walk(Vector3 FoodrPos)
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeMove")
            { _stateMachine.SetState(new BeeMove(transform, _animator, NpcBehaviors.MovingSpeed)); Play1(); }
            NPCRotateToTarget(FoodrPos);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
                _isPlayer = true;            
        }
        private void Attack()
        {
            if (_stateMachine.GetCurrentState() != "Game.BeeAttack")
            { _stateMachine.SetState(new BeeAttack(transform, _animator)); Play3(); }
        }
        private void NPCRotateToTarget(Vector3 TargetPosition)
        {
            var lookPos = transform.position - TargetPosition;
            lookPos.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(-lookPos);
        }

        #endregion

        #region Sounds
        private void Play1()
        {
            _audioSource.clip = NpcBehaviors.Sounds[0];
            _audioSource.loop = true;
            _audioSource.Play();
        }
        private void Play2()
        {
            _audioSource.clip = NpcBehaviors.Sounds[1];
            _audioSource.loop = true;
            _audioSource.Play();
        }
        private void Play3()
        {
            _audioSource.clip = NpcBehaviors.Sounds[2];
            _audioSource.loop = true;
            _audioSource.Play();
        }
        #endregion
    }
}
