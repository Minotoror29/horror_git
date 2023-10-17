using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidIdleState : KidState
{
    public KidIdleState(KidController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Controller.StopMoving();
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        if (Controller.TargetDistance > Controller.RunDistance)
        {
            Controller.ChangeState(new KidRunState(Controller));
        } else if (Controller.TargetDistance > Controller.StopDistance)
        {
            Controller.ChangeState(new KidWalkState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
    }
}
