using BehaviourTree;
using UnityEngine;

internal class CheckIsWorkPlaceRange : Node
{
    private int _layerMask;
    private Transform _transform;
    private float _findRange;
    private string _rName;
        
    public CheckIsWorkPlaceRange(Transform transform, float findRange, int layerMask , string resourceName)
    {
        _transform = transform;
        _findRange = findRange;
        _layerMask = layerMask;
        _rName = resourceName;
    }
    

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _findRange, _layerMask);
        
        object t = GetData(_rName);
        if (t == null)
        {
            if (colliders.Length > 0)
            {
                parent.parent.SetData(_rName, colliders[0].transform);

                //Debug.Log("SUCCESS Check : "+ _rName + " In  Range ");
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
        else
        {// we have target in data but not in range -> clear it
            if (colliders.Length == 0)
            {
                ClearData(_rName);
                state = NodeState.FAILURE;
                return state;
            }
        }
         
        // we have _rName in data and in range
        state = NodeState.SUCCESS;
        return state;       
    }


}
