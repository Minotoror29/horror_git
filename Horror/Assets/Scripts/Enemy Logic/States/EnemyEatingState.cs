using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEatingState : EnemyState
{
    private float _eatingTimer;

    public EnemyEatingState(EnemyController controller) : base(controller)
    {
        _eatingTimer = Controller.EatingTime;
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

        if (_eatingTimer > 0)
        {
            _eatingTimer -= Time.deltaTime;
        } else
        {
            Controller.ChangeState(new EnemyPatrolState(Controller));
        }
    }
}
