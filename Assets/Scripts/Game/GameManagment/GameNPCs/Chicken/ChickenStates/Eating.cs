using UnityEngine;
using Common;

namespace Game
{
    public class Eating : IState
    {
        private Animator _animator;
        private Transform _go;
        private float _speed;
        public Eating(Transform go, Animator animator, float speed)
        {
            _go = go;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            //Debug.Log("Chicken Eating");
        }

        public void Execute()
        {
            _animator.SetBool("Eat", true);
        }

        public void Exit()
        {
            _animator.SetBool("Eat", false);
        }
    }
}