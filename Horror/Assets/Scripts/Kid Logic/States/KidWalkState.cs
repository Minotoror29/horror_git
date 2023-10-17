using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidWalkState : KidState
{
    public KidWalkState(KidController controller) : base(controller)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (Controller.TargetDistance > Controller.RunDistance)
        {
            Controller.ChangeState(new KidRunState(Controller));
        } else if (Controller.TargetDistance <= Controller.StopDistance)
        {
            Controller.ChangeState(new KidIdleState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
        Controller.WalkTowardsTarget();
    }
}
