using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsInPlayerRange : Node
{
    private Transform _transform;    
    private float _range;
    private Transform _player;

    public CheckIsInPlayerRange(Transform transform, float range, Transform player)
    {
        _transform = transform;        
        _range = range;
        _player = player;
    }

    public override NodeState Evaluate()
    {
        if (_player == null)
        {
            //Debug.Log("FAILURE Check : "+_name+" Target is null ");
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(_transform.position, _player.position) >= _range)
        {
            Debug.unityLogger.Log("CheckIsInPlayerRange", "SUCCESS Check : NOT in Player Range ");

            state = NodeState.SUCCESS;//->will set the Sequence to SUCCESS
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}