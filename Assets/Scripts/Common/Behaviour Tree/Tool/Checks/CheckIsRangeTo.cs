using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using System;
using System.Linq;
using Game;

public class CheckIsRangeTo : Node
{
    private int _layerMask;
    private Transform _transform;
    private float _findRange;
    private Collider[] res = new Collider[] { };
    int count;
        
    public CheckIsRangeTo(Transform transform, float findRange, int layerMask)
    {
        _transform = transform;
        _findRange = findRange;
        _layerMask = layerMask;
    }

    public override NodeState Evaluate()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _findRange, _layerMask);


        //TODO: high potential for a spike
        //change Linq use to for loops 

        colliders = Array.FindAll(colliders, i => i.gameObject.tag != _transform.tag).ToArray(); // exclude same group characters & "Neutral"             
        colliders = Array.FindAll(colliders, i => i.gameObject.GetComponent<Death>() &&    // characters only 
                                                   !i.gameObject.GetComponent<Death>().IsDead && // exclude dead characters
                                                    i.isTrigger // exclude triggers
                                                    ).ToArray();
        
        // consider Presence Map instead of Physics.OverlapSphere 
        // https://forum.unity.com/threads/efficient-way-to-scan-surrounding-with-overlapsphere-with-large-number-of-objects.1182625/

        #region opt
        //count = colliders.Length;
        //for (int i = 0; i < count; i++)
        //{
        //    if ((colliders[i].isTrigger) &&
        //        (colliders[i].tag != _transform.tag) && 
        //        (!colliders[i].GetComponent<Death>().IsDead))
        //    {
        //        Array.Resize(ref res, res.Length + 1);
        //        res[res.GetUpperBound(0)] = colliders[i];                                
        //    }
        //}

        //List<Collider> collist = colliders.ToList();
        //foreach (var item in collist)
        //{
        //    if ((!item.isTrigger) ||
        //        (item.tag == _transform.tag) || 
        //        (item.GetComponent<Death>().IsDead))
        //        collist.Remove(item);
        //}
        #endregion

        object t = GetData("target");
        if (t == null)
        {
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                Debug.Log("SUCCESS Check : Enemy spotted In FOV Range ");
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
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
        }

        // we have target in data and in range        
        state = NodeState.SUCCESS;
        return state;

    }


}
