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
            if (Controller.FollowTargets.Count == 0)
            {
                CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
            }
            else
            {
                if (Controller.TargetDistance <= Controller.FollowTargets[0].StopDistance)
                {
                    CurrentSuperstate.ChangeSubstate(Controller.FollowTargets[0].EnterStopDistance(Controller));
                }
                else if (Controller.TargetDistance <= Controller.FollowTargets[0].RunDistance)
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
