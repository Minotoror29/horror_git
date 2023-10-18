using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidDarkState : KidState
{
    public KidDarkState(KidController controller) : base(controller)
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

        if (collision.GetComponent<PlayerController>())
        {
            Controller.ChangeState(new KidLightState(Controller));
        }
    }
}
