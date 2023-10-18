using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    private Transform _target;

    public EnemyChasingState(EnemyController controller, Transform target) : base(controller)
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

        if ((_target.position - Controller.transform.position).magnitude < Controller.KillDistance)
        {
            Controller.ChangeState(new EnemyEatingState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunTowardsTarget(_target);
    }
}
