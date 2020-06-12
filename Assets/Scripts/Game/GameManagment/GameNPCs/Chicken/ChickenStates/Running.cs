using UnityEngine;
using Common;

namespace Game
{
    public class Running : IState
    {
        private Animator _animator;
        private Transform _transform;
        private float _speed;
        public Running(Transform transform, Animator animator, float speed)
        {
            _transform = transform;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            //Debug.Log("Chicken Running");
        }

        public void Execute()
        {
            _transform.position += _transform.forward * _speed * Time.deltaTime;

            _animator.SetBool("Run", true);
        }

        public void Exit()
        {
            _animator.SetBool("Run", false);
        }

    }
}