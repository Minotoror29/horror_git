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
        Controller.FollowTarget = FindNearestHideSpot();
    }

    private Transform FindNearestHideSpot()
    {
        if (Controller.HideSpots.Count > 0)
        {
            float shortestDistance = 0f;
            HideSpot nearestSpot = null;

            for (int i = 0; i < Controller.HideSpots.Count; i++)
            {
                float distance = (Controller.HideSpots[i].transform.position - Controller.transform.position).magnitude;

                if (i == 0)
                {
                    shortestDistance = distance;
                    nearestSpot = Controller.HideSpots[i];
                }
                else
                {
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSpot = Controller.HideSpots[i];
                    }
                }
            }

            return nearestSpot.transform;
        } else
        {
            return null;
        }
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
