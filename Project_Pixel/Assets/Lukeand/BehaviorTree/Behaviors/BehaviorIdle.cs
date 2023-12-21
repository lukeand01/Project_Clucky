using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorIdle : Node
{
    public override NodeState Evaluate()
    {
        return NodeState.Success;

        //this does nothing.
        //other than trigger idle animation
    }
}
