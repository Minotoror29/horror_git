using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunFromState : EnemyState
{
    private Transform _target;

    public EnemyRunFromState(EnemyController controller, Transform target) : base(controller)
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

        Controller.RunFromTarget(_target);
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);

        if (collision.GetComponent<PlayerController>())
        {
            Controller.ChangeState(new EnemyIdleState(Controller));
        }
    }
}
