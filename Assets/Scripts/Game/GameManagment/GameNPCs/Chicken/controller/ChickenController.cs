using UnityEngine;
using Common;

namespace Game
{

    // TODO: abstract Controller
    public class ChickenController : MonoBehaviour,INpc
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
        private void Start()
        {                    
            Patrol(); // Chicken Strolling around on start
            _health.Value = NpcBehaviors.Health;
        }
        public void GetLavelData(){}
        
        private Vector3 _playerPosition;
        private Vector3 _foodPosition;
        private float _playerDistance;
        private float _foodDistance;
        private float _nextActionTime = 0.0f;
        private bool _stroll = false;
        private bool _isFood = false;
        private string _state;

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

            ChickenBrain(_playerPosition, _foodPosition);
            _foodPosition = Vector3.zero;
            _playerPosition = Vector3.zero;
            Vector3Utility.Clear();

            if (Time.time > _nextActionTime)
            {
                _nextActionTime += NpcBehaviors.PatrollingStandingPeriod;
                _stroll = !_stroll;
            }
        }
        private void ChickenBrain(Vector3 _playerPosition, Vector3 FoodrPos)
        {            
            _playerDistance = (transform.position - _playerPosition).magnitude;

            if (_playerPosition == Vector3.zero)// -> no player found
            {
                if (_isFood) // -> on top of Food            
                    EatOrStand();
                else // look for food
                {
                    _foodDistance = (transform.position - FoodrPos).magnitude;
                    if (_foodDistance < NpcBehaviors.SearchRadius) // found food location                
                        Walk(FoodrPos);
                    else // no food to be found -> stroll...                
                        PatrolOrStand();
                }
            }
            else // -> player found 
            {
                if (_playerDistance < 2 && _playerPosition != Vector3.zero)
                    Run(_playerPosition);
            }
        }

        #region States functions
        private void Patrol()
        {_stateMachine.SetState(new Patrol(transform, _animator, NpcBehaviors.MovingSpeed));Play1();}
        private void Run(Vector3 _playerPosition)
        {
            if (_state != "Game.Running")
            {_stateMachine.SetState(new Running(transform, _animator, NpcBehaviors.FastMovingSpeed));Play2();}
            NPCRotateToTarget(_playerPosition);
            _isFood = false;            
        }
        private void PatrolOrStand()
        {
            if (_state != "Game.Patrol" && !_stroll)
            { _stateMachine.SetState(new Patrol(transform, _animator, NpcBehaviors.MovingSpeed)); Play1(); }
            if (_state != "Game.Standing" && _stroll)
            { _stateMachine.SetState(new Standing(transform, _animator, NpcBehaviors.MovingSpeed)); Play1(); }            
        }
        private void Walk(Vector3 FoodrPos)
        {
            if (_state != "Game.Walking")
            {_stateMachine.SetState(new Walking(transform, _animator, NpcBehaviors.FastMovingSpeed));Play1();}
            NPCRotateToTarget(FoodrPos);            
        }
        private void EatOrStand()
        {
            if (_state != "Game.Eating" && !_stroll)
            { _stateMachine.SetState(new Eating(transform, _animator, NpcBehaviors.MovingSpeed)); Play1(); }
            if (_state != "Game.Standing" && _stroll)
            { _stateMachine.SetState(new Standing(transform, _animator, NpcBehaviors.MovingSpeed)); Play1(); }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Food")
                _isFood = true;
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
            _audioSource.loop = false;
            _audioSource.Play();
        }
        private void Play2()
        {
            _audioSource.clip = NpcBehaviors.Sounds[1];
            _audioSource.loop = true;
            _audioSource.Play();
        }
        #endregion

    }

}