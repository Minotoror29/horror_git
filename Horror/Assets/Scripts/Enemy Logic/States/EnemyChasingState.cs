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

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunTowardsTarget(_target);
    }
}
