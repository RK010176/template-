using UnityEngine;
using Common;

namespace Game
{
    public class BeeAttack : IState
    {
        private Animator _animator;
        private Transform _transform;        
        public BeeAttack(Transform transform, Animator animator)
        {
            _transform = transform;
            _animator = animator;
        }

        public void Enter()
        {
            //Debug.Log("Bee Attacking");
        }

        public void Execute()
        {
            _animator.SetBool("Attack", true);            
        }

        public void Exit()
        {
            _animator.SetBool("Attack", false);
        }
    }
}