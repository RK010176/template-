using UnityEngine;
using Common;

namespace Game
{

    public class Walking : IState
    {
        private Animator _animator;
        private Transform _transform;
        private float _speed;

        public Walking(Transform transform, Animator animator, float speed)
        {
            _transform = transform;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            //Debug.Log("Chicken Walking");
        }

        public void Execute()
        {
            // moving forward
            _transform.position += _transform.forward * _speed * Time.deltaTime;
            // playing walking animation
            _animator.SetBool("Walk", true);
        }

        public void Exit()
        {
            _animator.SetBool("Walk", false);
        }

    }
}