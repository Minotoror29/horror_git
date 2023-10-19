using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidIdleState : KidState
{
    public KidIdleState(KidController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        Controller.StopMovement();
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Controller.RecoverStamina();

        CheckTransitions();
    }

    private void CheckTransitions()
    {
        if (Controller.EnemiesInRange.Count > 0)
        {
            Controller.ChangeState(new KidRunFromState(Controller, Controller.EnemiesInRange[0]));
        }
        else
        {
            if (Controller.FollowTargets.Count > 0)
            {
                if (Controller.TargetDistance(Controller.FollowTargets[0].transform.position) > Controller.FollowTargets[0].RunDistance)
                {
                    Controller.ChangeState(new KidRunToState(Controller, Controller.FollowTargets[0]));
                }
                else if (Controller.TargetDistance(Controller.FollowTargets[0].transform.position) > Controller.FollowTargets[0].StopDistance)
                {
                    Controller.ChangeState(new KidWalkToState(Controller, Controller.FollowTargets[0]));
                }
            }
        }
    }

    //public override void OnTriggerEnter(Collider2D collision)
    //{
    //    base.OnTriggerEnter(collision);

    //    EnemyController enemy = collision.GetComponent<EnemyController>();
    //    if (enemy)
    //    {
    //        Controller.ChangeState(new KidRunFromState(Controller, enemy));
    //    }
    //}
}
