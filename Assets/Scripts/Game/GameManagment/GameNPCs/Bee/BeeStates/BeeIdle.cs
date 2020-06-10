using UnityEngine;
using Common;


namespace Game
{
    public class BeeIdle : IState
    {
        private Animator _animator;
        private Transform _go;
        private float _speed;

        public BeeIdle(Transform go, Animator animator, float speed)
        {
            _go = go;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            Debug.Log("Bee Idle");
        }

        public void Execute()
        {
            _animator.SetBool("Idle", true);
        }

        public void Exit()
        {
            _animator.SetBool("Idle", false);
        }
    }
}