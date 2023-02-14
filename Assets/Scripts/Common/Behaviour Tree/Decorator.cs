using BehaviourTree;
using System.Collections.Generic;

public class Decorator : Node
{

    public Node child { get; set; }
    public Decorator() : base() { }    
    //public Decorator(Node child) : base(child) { }
    

    

}
