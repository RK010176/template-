using BehaviourTree;
using RPGCharacterAnims;
using UnityEngine;

internal class TaskWork : Node
{
    private Transform _transform;
    private Controls _controls;
    public TaskWork(Transform transform)
    {
        _transform = transform;
        _controls = transform.GetComponent<Controls>();
    }

    public override NodeState Evaluate()
    {
        
        _controls.attack = true;
        Debug.Log("RUNNING Task : Work");
        state = NodeState.RUNNING;
        return state;
    }

}