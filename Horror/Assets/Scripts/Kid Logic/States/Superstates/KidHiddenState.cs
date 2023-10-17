using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidHiddenState : KidState
{
    private HideSpot _hideSpot;

    public KidHiddenState(KidController controller, HideSpot hideSpot) : base(controller)
    {
        _hideSpot = hideSpot;
    }

    public override void Enter()
    {
        Controller.transform.position = _hideSpot.transform.position;
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
