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
            Controller.ChangeState(new EnemyPatrolState(Controller));
        }
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.GetComponent<KidController>())
        {
            Controller.ChangeState(new EnemyChasingState(Controller, collision.transform));
        }
    }
}
