using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidShadowState : KidState
{
    public KidShadowState(KidController controller) : base(controller)
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
            Controller.FollowTarget = collision.transform;
            Controller.ChangeState(new KidLightState(Controller));
        }
    }
}
