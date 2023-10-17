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
        Controller.RecoverStamina();

        if (Controller.CurrentStamina >= Controller.MaxStamina)
        {
            if (Controller.TargetDistance > Controller.RunDistance)
            {
                Controller.ChangeState(new KidRunState(Controller));
            } else if (Controller.TargetDistance > Controller.StopDistance)
            {
                Controller.ChangeState(new KidWalkState(Controller));
            } else if (Controller.TargetDistance <= Controller.StopDistance)
            {
                Controller.ChangeState(new KidIdleState(Controller));
            }
        }
    }

    public override void UpdatePhysics()
    {
    }
}
