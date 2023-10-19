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
            if (Controller.EnemiesInRange.Count > 0 && _target.Priority != 1)
            {
                Controller.ChangeState(new KidRunFromState(Controller, Controller.EnemiesInRange[0]));
            }
            else
            {
                if (!Controller.FollowTargets.Contains(_target))
                {
                    Controller.ChangeState(new KidIdleState(Controller));
                }
                else
                {
                    if (Controller.TargetDistance(_target.transform.position) <= _target.StopDistance)
                    {
                        Controller.ChangeState(_target.EnterStopDistance(Controller));
                    }
                    else if (Controller.TargetDistance(_target.transform.position) <= _target.RunDistance)
                    {
                        Controller.ChangeState(new KidWalkToState(Controller, _target));
                    }
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunTowardsTarget(_target.transform);
    }
}
