using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRunState : KidState
{
    public KidRunState(KidController controller) : base(controller)
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
        if (Controller.TargetDistance <= Controller.StopDistance)
        {
            Controller.ChangeState(new KidIdleState(Controller));
        } else if (Controller.TargetDistance <= Controller.RunDistance)
        {
            Controller.ChangeState(new KidWalkState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
        Controller.RunTowardsTarget();
    }
}
