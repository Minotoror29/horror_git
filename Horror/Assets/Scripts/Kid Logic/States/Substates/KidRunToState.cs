using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRunToState : KidState
{
    private FollowTarget _target;

    public KidRunToState(KidController controller, FollowTarget target) : base(controller)
    {
        _target = target;
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
            Controller.ChangeState(new KidRecoveryState(Controller));
        }
        else
        {
            if (Controller.FollowTargets.Count == 0)
            {
                Controller.ChangeState(new KidIdleState(Controller));
            }
            else
            {
                if (Controller.TargetDistance <= _target.StopDistance)
                {
                    Controller.ChangeState(_target.EnterStopDistance(Controller));
                }
                else if (Controller.TargetDistance <= _target.RunDistance)
                {
                    Controller.ChangeState(new KidWalkToState(Controller, _target));
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunTowardsTarget();
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);

        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy)
        {
            Controller.ChangeState(new KidRunFromState(Controller, enemy));
        }
    }
}
