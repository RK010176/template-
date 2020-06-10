using UnityEngine;
using Common;

namespace Game
{
    public class Standing : IState
    {
        private Animator _animator;
        private Transform _go;
        private float _speed;
        public Standing(Transform go, Animator animator, float speed)
        {
            _go = go;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            //Debug.Log("Chicken Standing");
        }

        public void Execute()
        {
            _animator.SetBool("Turn Head", true);
        }

        public void Exit()
        {
            _animator.SetBool("Turn Head", false);
        }
    }
}