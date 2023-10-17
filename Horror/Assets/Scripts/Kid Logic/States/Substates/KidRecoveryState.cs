using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRecoveryState : KidState
{
    public KidRecoveryState(KidController controller) : base(controller)
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

        if (Controller.CurrentStamina >= Controller.MaxStamina)
        {
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
                else if (Controller.TargetDistance > Controller.StopDistance)
                {
                    CurrentSuperstate.ChangeSubstate(new KidWalkState(Controller));
                }
                else if (Controller.TargetDistance <= Controller.StopDistance)
                {
                    CurrentSuperstate.ChangeSubstate(new KidIdleState(Controller));
                }
            }
        }
    }
}
