using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using System;
using System.Linq;
using Game;

public class CheckInRange : Node
{
    private Transform _transform;
    private Animator _animator;
    private Attack _attack;
    private float _range;
    private string _name;
    public CheckInRange(Transform transform, float range, string targetName = "target")
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _attack = transform.GetComponent<Attack>();
        _range = range;
        _name = targetName;
    }    

    public override NodeState Evaluate()
    {                
        object t = GetData(_name);
        if (t == null)
        {            
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position , target.position) <= _range)
        {
            //Debug.Log("SUCCESS Check : Enemy Is In Attack Range ");
            if (_name == "target") _attack.IsAttacking = true;
            RotateToPosition(target.position);
            _animator.SetBool("Moving", false);
            state = NodeState.SUCCESS;//->will set the Sequence to SUCCESS
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }


    // rotate to position 
    private void RotateToPosition(Vector3 pos)
    {
        Vector3 direction = pos - _transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}
