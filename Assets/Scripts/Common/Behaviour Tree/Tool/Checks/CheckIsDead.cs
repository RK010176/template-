using UnityEngine;
using BehaviourTree;
using Game;

public class CheckIsDead : Node
{
    private Death _death;
    private Transform _transform;

    public CheckIsDead(Transform transform)
    {
        _transform = transform;
        _death = transform.GetComponent<Death>();
    }

    public override NodeState Evaluate()
    {        
        if (_death.IsDead)
        {
            //Debug.Log("SUCCESS Check : Dead on");
            state = NodeState.SUCCESS;
            return state;
        }        
        state = NodeState.FAILURE;
        return state;
    }
}
