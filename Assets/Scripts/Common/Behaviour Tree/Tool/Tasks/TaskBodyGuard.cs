using UnityEngine;
using BehaviourTree;

public class TaskBodyGuard : Node
{
    private Transform _transform;
    private Animator _animator;
    private float _attackRange;
    private float _goToSpeed;
    private Vector3 _pos = Vector3.zero;
    private Transform _player;    
    public TaskBodyGuard(Transform transform, float attackRange, float goToSpeed, Transform player)
    {
        _transform = transform;
        _animator = transform.GetComponentInChildren<Animator>();
        _attackRange = attackRange;
        _goToSpeed = goToSpeed;
        _player = player;
    }
    public override NodeState Evaluate()
    {
        if (_player != null)
        {
            //if (_pos == Vector3.zero)
                _pos = _player.position; //GetRandomPos(_player.position, 3);

            if (Vector3.Distance(_transform.position, _pos) > _attackRange+1)
            {
                Debug.unityLogger.Log("CheckIsInPlayerRange", "moving to Player Range");

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


    // return position in a radius around a target 
    public Vector3 GetRandomPos(Vector3 targetPos, float radius)
    {
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * radius;
        randomPos = new Vector3(randomPos.x, 0, randomPos.z);

        Debug.unityLogger.Log("randomPos", "called - ");

        return randomPos += targetPos;
    }


}
