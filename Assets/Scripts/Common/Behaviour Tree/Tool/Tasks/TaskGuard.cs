using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskGuard : Node
{

    private Transform _transform;
    private Animator _animator;
    private float _attackRange;
    private float _goToSpeed;
    private Vector3 _pos;
    public TaskGuard(Transform transform, float attackRange, float goToSpeed)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _attackRange = attackRange;
        _goToSpeed = goToSpeed;
    }
    public override NodeState Evaluate()
    {
        if (GetData("guardPos") != null)
        {
            _pos = (Vector3)GetData("guardPos");

            if (Vector3.Distance(_transform.position, _pos) > _attackRange)
            {
                //Debug.Log("RUNNING Task : Go To Position ");

                _transform.LookAt(_pos);
                // playing Moving animation        
                _animator.SetBool("Moving", true);
                _animator.SetFloat("AnimationSpeed", _goToSpeed);//1
                _animator.SetFloat("Velocity Z", _goToSpeed);//3            
            }
        }
        
        state = NodeState.RUNNING;
        return state;
    }


}
