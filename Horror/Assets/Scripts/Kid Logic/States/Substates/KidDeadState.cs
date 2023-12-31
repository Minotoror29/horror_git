using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidDeadState : KidState
{
    public KidDeadState(KidController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Controller.StopMovement();
        Controller.gameObject.SetActive(false);

        Controller.GameManager.KidDeath(Controller);
    }

    public override void Exit()
    {
    }
}
