using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Game
{
    public class Controller : MonoBehaviour, INpc
    {
        public NpcBehaviors NpcBehaviors { get; set; }
        protected StateMachine _stateMachine;
        protected Animator _animator;
        protected AudioSource _audioSource;
        protected AudioManager _audioManager;
        [SerializeField] protected FloatVal _health;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _audioSource = GetComponent<AudioSource>();            
            _animator = GetComponent<Animator>();
            _audioManager = new AudioManager();                        
        }
        private void Start()
        {
            _audioManager.Sounds = NpcBehaviors.Sounds;
            _audioManager.AudioSource = _audioSource;
            _health.Value = NpcBehaviors.Health;
            Patrol(); //Patrolling around on start
        }
        protected virtual void Patrol()
        {
            _stateMachine.SetState(new Patrol(transform, _animator, NpcBehaviors.MovingSpeed));
        }

        protected Vector3 _playerPosition;
        protected Vector3 _foodPosition;
        protected float _playerDistance;
        protected float _foodDistance;
        protected float _nextActionTime = 0.0f;
        protected bool _stroll = false;        
        protected string _state;
        private void Update()
        {
            _state = _stateMachine.GetCurrentState();
            _stateMachine.Execute();

            // search for food and player
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, NpcBehaviors.SearchRadius);
            for (int i = 0; i < hitObjects.Length; i++)
            {
                if (hitObjects[i].CompareTag("Player"))
                    _playerPosition = hitObjects[i].gameObject.transform.position;
                if (hitObjects[i].CompareTag("Food"))
                    _foodPosition = Vector3Utility.GetClosest(transform.position, hitObjects[i].gameObject.transform.position);
            }

            Brain(_playerPosition);
            Brain(_playerPosition, _foodPosition);
            _foodPosition = Vector3.zero;
            _playerPosition = Vector3.zero;
            Vector3Utility.Clear();

            //TODO: D Inversion extract to timer class  from base 
            if (Time.time > _nextActionTime)
            {
                _nextActionTime += NpcBehaviors.PatrollingStandingPeriod;
                _stroll = !_stroll;
            }
        }
        protected virtual void Brain(Vector3 _playerPosition) { }
        protected virtual void Brain(Vector3 _playerPosition, Vector3 FoodPo) { }
        protected virtual void NPCRotateToTarget(Vector3 TargetPosition)
        {
            var lookPos = transform.position - TargetPosition;
            lookPos.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(-lookPos);
        }
    }
}