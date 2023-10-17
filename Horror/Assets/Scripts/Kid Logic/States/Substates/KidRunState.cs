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
        base.UpdateLogic();

        Controller.LoseStamina();

        CheckTransitions();
    }

    private void CheckTransitions()
    {
        if (Controller.CurrentStamina <= 0)
        {
            CurrentSuperstate.ChangeSubstate(new KidRecoveryState(Controller));
        }
        else
        {
            if (Controller.FollowTarget == null)
            {
                CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
            }
            else
            {
                if (Controller.TargetDistance <= Controller.StopDistance)
                {
                    CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
                }
                else if (Controller.TargetDistance <= Controller.RunDistance)
                {
                    CurrentSuperstate.ChangeSubstate(new KidWalkState(Controller));
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunTowardsTarget();
    }
}
