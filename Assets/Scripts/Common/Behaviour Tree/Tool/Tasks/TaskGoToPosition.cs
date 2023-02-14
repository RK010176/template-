using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGoToPosition : Node
{
    private Transform _transform;
    private Animator _animator;
    private float _attackRange;
    private float _goToSpeed;

    public TaskGoToPosition(Transform transform, float attackRange, float goToSpeed)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _attackRange = attackRange;
        _goToSpeed = goToSpeed;
    }
    public override NodeState Evaluate()
    {
        Vector3 pos = (Vector3)GetData("mousePos");        

        if (Vector3.Distance(_transform.position, pos) > _attackRange)
        {
            Debug.Log("RUNNING Task : Go To Position ");

            //_transform.LookAt(pos);
            RotateToPosition(pos);
            // playing Moving animation        
            _animator.SetBool("Moving", true);
            _animator.SetFloat("AnimationSpeed", _goToSpeed);//1
            _animator.SetFloat("Velocity Z", _goToSpeed);//3            
        }
        else
            ClearData("mousePos");

        state = NodeState.RUNNING;
        return state;
    }

    // rotate to position 
    private void RotateToPosition(Vector3 pos)
    {
        Vector3 direction = pos - _transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_transform.rotation, lookRotation, Time.deltaTime * _goToSpeed).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}
