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
        Controller.StopMovement();
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
            Controller.ChangeState(new KidIdleState(Controller));
        }
    }
}
