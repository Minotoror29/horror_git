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
        Debug.Log("Walk to " + _target.gameObject.name);
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
            if (!Controller.FollowTargets.Contains(_target))
            {
                Controller.ChangeState(new KidIdleState(Controller));
            }
            else
            {
                if (Controller.TargetDistance(_target.transform.position) > _target.RunDistance)
                {
                    Controller.ChangeState(new KidRunToState(Controller, _target));
                }
                else if (Controller.TargetDistance(_target.transform.position) <= _target.StopDistance)
                {
                    Controller.ChangeState(_target.EnterStopDistance(Controller));
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.WalkTowardsTarget(_target.transform);
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
