using BehaviourTree;
using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckMousePosition : Node
{
    
    public CheckMousePosition(){}

    public override NodeState Evaluate()
    {
        
        object t = GetData("mousePos");
        if (t != null)
        {
            //Debug.Log("SUCCESS Check : Enemy spotted In FOV Range ");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

}
