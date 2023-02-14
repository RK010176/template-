using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Repeater : Decorator
{    
    //public Repeater(Node child) : base(child) { }
    public override NodeState Evaluate()
    {                
        return NodeState.RUNNING;
    }
}
