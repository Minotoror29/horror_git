using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAfraidState : KidState
{
    private EnemyController _enemy;

    public KidAfraidState(KidController controller, EnemyController enemy) : base(controller)
    {
        _enemy = enemy;
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

        if ((_enemy.transform.position - Controller.transform.position).magnitude < _enemy.KillDistance)
        {
            Controller.ChangeState(new KidDeadState(Controller));
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Controller.RunFromTarget(_enemy.transform);
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);

        if (collision.GetComponent<EnemyController>() == _enemy)
        {

        }
    }
}
