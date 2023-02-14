using UnityEngine;
using BehaviourTree;
using Game;

public class TaskDead : Node
{
    private Death _death;
    private Attack _attack;
    private Transform _transform;
    private Animator _animator;

    public TaskDead(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _death = transform.GetComponent<Death>();
        _attack = transform.GetComponent<Attack>();
    }

    public override NodeState Evaluate()
    {        
        if (_death.IsDead)
        {
            Debug.Log("RUNNING Task : Dead on");
            _animator.SetBool("Moving", false);
            _attack.IsAttacking = false;
        }
        
        state = NodeState.RUNNING;
        return state;
    }
}
