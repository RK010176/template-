﻿using UnityEngine;
using Common;

namespace Game
{
    public class Attacking : IState
    {
        private Animator _animator;
        private Transform _transform;
        private FloatVal _speed;
        public Attacking(Transform transform, Animator animator, FloatVal speed)
        {
            _transform = transform;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            //Debug.Log("Attacking");
        }

        public void Execute()
        {
            _transform.position += _transform.forward * _speed.Value * Time.deltaTime;

            _animator.SetBool("Eat", true);
        }

        public void Exit()
        {
            _animator.SetBool("Eat", false);
        }
    }
}