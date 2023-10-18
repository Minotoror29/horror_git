using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRunFromState : KidState
{
    private EnemyController _enemy;

    public KidRunFromState(KidController controller, EnemyController enemy) : base(controller)
    {
        _enemy = enemy;
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

        if (Controller.CurrentStamina <= 0)
        {
            Controller.ChangeState(new KidRecoveryState(Controller));
        }

        if (Controller.FollowTargets.Count == 1)
        {
            if (Controller.FollowTargets[0].Priority == 1)
            {
                Controller.ChangeState(new KidRunToState(Controller, Controller.FollowTargets[0]));
            }
        } else if (Controller.FollowTargets.Count > 1)
        {
            if (Controller.FollowTargets[0].Priority == 1)
            {
                Controller.ChangeState(new KidRunToState(Controller, Controller.FollowTargets[0]));
            } else
            {
                Controller.ChangeState(new KidRunToState(Controller, Controller.FollowTargets[1]));
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunFrom(_enemy.transform);
    }
}
