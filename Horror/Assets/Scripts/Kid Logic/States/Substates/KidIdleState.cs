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
        base.UpdateLogic();

        Controller.RecoverStamina();

        CheckTransitions();
    }

    private void CheckTransitions()
    {
        if (Controller.FollowTargets.Count > 0)
        {
            if (Controller.TargetDistance > Controller.FollowTargets[0].RunDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidRunState(Controller));
            }
            else if (Controller.TargetDistance > Controller.FollowTargets[0].StopDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidWalkState(Controller));
            }
        }
    }
}
