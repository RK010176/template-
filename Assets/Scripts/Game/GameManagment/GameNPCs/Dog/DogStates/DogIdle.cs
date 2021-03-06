﻿using UnityEngine;
using Common;


namespace Game
{
    public class DogIdle : IState
    {
        private Animator _animator;
        private Transform _go;
        private float _speed;

        public DogIdle(Transform go, Animator animator, float speed)
        {
            _go = go;
            _animator = animator;
            _speed = speed;
        }

        public void Enter()
        {
            Debug.Log("Dog Idle");
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