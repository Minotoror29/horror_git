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
        if (Controller.FollowTarget != null)
        {
            if (Controller.TargetDistance > Controller.RunDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidRunState(Controller));
            }
            else if (Controller.TargetDistance > Controller.StopDistance)
            {
                CurrentSuperstate.ChangeSubstate(new KidWalkState(Controller));
            }
        }
    }
}
