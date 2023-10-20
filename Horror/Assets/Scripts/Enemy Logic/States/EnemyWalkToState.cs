using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkToState : EnemyState
{
    private Transform _target;

    public EnemyWalkToState(EnemyController controller, Transform target) : base(controller)
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

        if (Controller.KidsInRange.Count > 0)
        {
            foreach (KidController kid in Controller.KidsInRange)
            {
                if (!kid.IsHidden)
                {
                    Controller.ChangeState(new EnemyChasingState(Controller, Controller.KidsInRange[0]));
                    break;
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.WalkTowardsTarget(_target.position);
    }

    //public override void OnTriggerEnter(Collider2D collision)
    //{
    //    base.OnTriggerEnter(collision);

    //    KidController kid = collision.GetComponent<KidController>();
    //    if (kid)
    //    {
    //        Controller.ChangeState(new EnemyChasingState(Controller, kid));
    //    }
    //}

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);

        if (collision.GetComponent<PlayerController>())
        {
            Controller.ChangeState(new EnemyIdleState(Controller));
        }
    }
}
