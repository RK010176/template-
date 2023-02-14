using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using RPGCharacterAnims;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator _animator;
    private float _speed;
    private float _turn;
    private float _nextActionTime = 0;
    private bool _stroll = true;
    private float _standTime;
    private float _patrolTime;


    public TaskPatrol(Transform transform, float speed, float turn, float standTime, float patrolTime)// constructor
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _speed = speed;
        _turn = turn;
        _standTime = standTime;
        _patrolTime = patrolTime;
    }
    
    public override NodeState Evaluate()
    {
        //Debug.Log("RUNNING Task : Patrol");
        
        if (Time.time > _nextActionTime)
        {
            if (_stroll) // patrol
            {
                RandomTurn();
                _nextActionTime += _standTime; 
            }
            if (!_stroll) // stand
                _nextActionTime += _patrolTime;
            _stroll = !_stroll;
        }

        if (_stroll)
        {
            // moving forward
            _transform.position += _transform.forward * _speed * Time.deltaTime;
            _transform.Rotate(Vector3.up, _turn * Time.deltaTime, Space.Self);

            // playing walking animation        
            _animator.SetBool("Moving", true);
            _animator.SetFloat("AnimationSpeed", _speed);//1
            _animator.SetFloat("Velocity Z", _speed);//3
        }
        else 
        {
            _animator.SetBool("Moving", false);
        }
        
        state = NodeState.RUNNING;
        return state;
    }
    
    public void RandomTurn()
    {
        _turn = Random.Range(-50, 50) / 1f;
    }
    public void RandomPatrolTime()
    {
        _speed = Random.Range(3, 10) / 1f;
    }

    

}
