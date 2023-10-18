using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidWalkToState : KidState
{
    private FollowTarget _target;

    public KidWalkToState(KidController controller, FollowTarget target) : base(controller)
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

        if (Controller.EnemiesInRange.Count > 0)
        {
            Controller.ChangeState(new KidRunFromState(Controller, Controller.EnemiesInRange[0]));
        }
        else
        {
            if (Controller.FollowTargets.Count == 0)
            {
                Controller.ChangeState(new KidIdleState(Controller));
            }
            else
            {
                if (Controller.TargetDistance > Controller.FollowTargets[0].RunDistance)
                {
                    Controller.ChangeState(new KidRunToState(Controller, _target));
                }
                else if (Controller.TargetDistance <= Controller.FollowTargets[0].StopDistance)
                {
                    Controller.ChangeState(Controller.FollowTargets[0].EnterStopDistance(Controller));
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.WalkTowardsTarget();
    }

    //public override void OnTriggerEnter(Collider2D collision)
    //{
    //    base.OnTriggerEnter(collision);

    //    EnemyController enemy = collision.GetComponent<EnemyController>();
    //    if (enemy)
    //    {
    //        Controller.ChangeState(new KidRunFromState(Controller, enemy));
    //    }
    //}
}
