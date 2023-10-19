using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    private KidController _kid;

    public EnemyChasingState(EnemyController controller, KidController kid) : base(controller)
    {
        _kid = kid;
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

        if (_kid.IsHidden && !Controller.CanEatInBushes)
        {
            Controller.ChangeState(new EnemyIdleState(Controller));
        } else
        {
            if ((_kid.transform.position - Controller.transform.position).magnitude < Controller.KillDistance)
            {
                _kid.ChangeState(new KidDeadState(_kid));
                Controller.ChangeState(new EnemyEatingState(Controller));
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunTowardsTarget(_kid.transform);
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);

        PlayerController player = collision.GetComponent<PlayerController>();
        if (player && Controller.IsAfraidOfLight)
        {
            Controller.ChangeState(new EnemyRunFromState(Controller, player.transform));
        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);

        KidController kid = collision.GetComponent<KidController>();
        if (kid)
        {
            if (kid == _kid)
            {
                Controller.ChangeState(new EnemyIdleState(Controller));
            }
        }
    }
}
