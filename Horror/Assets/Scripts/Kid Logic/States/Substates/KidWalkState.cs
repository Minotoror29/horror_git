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

        if (Controller.FollowTargets.Count == 0)
        {
            CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
        }
        else
        {
            if (Controller.TargetDistance > Controller.FollowTargets[0].RunDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidRunState(Controller));
            }
            else if (Controller.TargetDistance <= Controller.FollowTargets[0].StopDistance)
            {
                CurrentSuperstate.ChangeSubstate(Controller.FollowTargets[0].EnterStopDistance(Controller));
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.WalkTowardsTarget();
    }
}
