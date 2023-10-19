using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float _waitTimer;

    public EnemyIdleState(EnemyController controller) : base(controller)
    {
        _waitTimer = Controller.PatrolWaitTime;
    }

    public override void Enter()
    {
        Controller.StopMovement();
    }

    public override void Exit()
    {
        
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_waitTimer > 0f)
        {
            _waitTimer -= Time.deltaTime;
        } else
        {
            if (Controller.CanPatrol || (Controller.PatrolCenter - (Vector2)Controller.transform.position).magnitude > 0.1f)
            {
                Controller.ChangeState(new EnemyPatrolState(Controller));
            }
        }
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
            } else if (Controller.IsAttractedByLight)
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
