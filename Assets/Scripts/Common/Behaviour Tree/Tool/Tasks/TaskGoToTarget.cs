using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskGoToTarget : Node
{

    private Transform _transform;
    private Animator _animator;
    private float _range;
    private float _goToSpeed;
    private string _name;

    public TaskGoToTarget(Transform transform, float attackRange, float goToSpeed, string targetName = "target")
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _range = attackRange;
        _goToSpeed = goToSpeed;
        _name = targetName;
    }
    
    public override NodeState Evaluate()
    {        
        Transform target = (Transform)GetData(_name);
        
        if (Vector3.Distance(_transform.position, target.position) > _range)
        {
            //Debug.Log("RUNNING Task : Go To Target ");

            //_transform.LookAt(target);
            RotateToPosition(target.position);
            // playing Moving animation        
            _animator.SetBool("Moving", true);
            _animator.SetFloat("AnimationSpeed", _goToSpeed);//1
            _animator.SetFloat("Velocity Z", _goToSpeed);//3            
        }
                    
        state = NodeState.RUNNING;
        return state;
    }

    // rotate to position 
    private void RotateToPosition(Vector3 pos)
    {
        Vector3 direction = pos - _transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
