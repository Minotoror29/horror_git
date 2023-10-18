using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidLightState : KidState
{
    public KidLightState(KidController controller) : base(controller)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.GetComponent<EnemyController>())
        {

        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);

        if (collision.GetComponent<PlayerController>())
        {
            Controller.ChangeState(new KidDarkState(Controller));
        }
    }
}
