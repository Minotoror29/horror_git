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
        base.UpdateLogic();

        if (Controller.FollowTarget == null)
        {
            CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
        }
        else
        {
            if (Controller.TargetDistance > Controller.RunDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidRunState(Controller));
            }
            else if (Controller.TargetDistance <= Controller.StopDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.WalkTowardsTarget();
    }
}
