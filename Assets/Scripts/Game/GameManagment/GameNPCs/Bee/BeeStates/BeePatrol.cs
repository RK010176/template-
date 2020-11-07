using UnityEngine;
using Common;


namespace Game
{
    public class BeePatrol : IState
    {
        private Animator _animator;
        private Transform _transform;
        private float _speed;

        public BeePatrol(Transform transform, Animator animator, float speed)
        {
            _transform = transform;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            //Debug.Log("Bee Patrol");
        }

        public void Execute()
        {
            // moving forward
            _transform.position += _transform.forward * _speed * Time.deltaTime;
            _transform.Rotate(Vector3.up, 10 * Time.deltaTime, Space.Self);
            // playing walking animation        
            _animator.SetBool("Idle", true);

        }

        public void Exit()
        {
            _animator.SetBool("Idle", false);
        }
    }

}