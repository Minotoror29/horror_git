using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Vector2 _targetPoint;

    public EnemyPatrolState(EnemyController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        DeterminePatrolPoint();
    }

    private void DeterminePatrolPoint()
    {
        _targetPoint = Random.insideUnitCircle * Controller.PatrolRadius + Controller.PatrolCenter;
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if ((_targetPoint - (Vector2)Controller.transform.position).magnitude < 1f)
        {
            DeterminePatrolPoint();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.MoveTowardsTarget(_targetPoint);
    }
}
