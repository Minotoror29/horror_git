using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidHiddenState : KidState
{
    private HideSpot _hideSpot;

    public KidHiddenState(KidController controller, HideSpot hideSpot) : base(controller)
    {
        _hideSpot = hideSpot;
    }

    public override void Enter()
    {
        Controller.IsHidden = true;
        Controller.StopMovement();
        Controller.transform.position = _hideSpot.transform.position;
    }

    public override void Exit()
    {
        Controller.IsHidden = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Controller.RecoverStamina();

        if (Controller.FollowTargets[0] != _hideSpot && Controller.EnemiesInRange.Count == 0)
        {
            Controller.ChangeState(new KidIdleState(Controller));
        }
    }
}
