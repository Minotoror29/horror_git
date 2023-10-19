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

        if ((_targetPoint - (Vector2)Controller.transform.position).magnitude < 0.1f)
        {
            Controller.ChangeState(new EnemyIdleState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.WalkTowardsTarget(_targetPoint);
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);

        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            if (Controller.IsAfraidOfLight)
            {
                Controller.ChangeState(new EnemyRunFromState(Controller, player.transform));
            }
            else if (Controller.IsAttractedByLight)
            {
                Controller.ChangeState(new EnemyWalkToState(Controller, player.transform));
            }
        }

        KidController kid = collision.GetComponent<KidController>();
        if (kid)
        {
            Controller.ChangeState(new EnemyChasingState(Controller, kid));
        }
    }
}
