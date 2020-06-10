using UnityEngine;
using Common;

namespace Game
{
    public class BeeMove : IState
    {
        private Animator _animator;
        private Transform _transform;
        private float _speed;

        public BeeMove(Transform transform, Animator animator, float speed)
        {
            _transform = transform;
            _animator = animator;
            _speed = speed;
        }
        public void Enter()
        {
            Debug.Log("Bee Move");
        }

        public void Execute()
        {
            _transform.position += _transform.forward * _speed * Time.deltaTime;
            // playing walking animation        
            _animator.SetBool("Move", true);
        }

        public void Exit()
        {
            _animator.SetBool("Move", false);
        }

    }
}
