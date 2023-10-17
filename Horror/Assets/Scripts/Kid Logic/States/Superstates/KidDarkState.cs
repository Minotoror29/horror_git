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
        //Controller.FollowTargets = FindNearestHideSpot();
    }

    private FollowTarget FindNearestHideSpot()
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

            Controller.HideSpots.Clear();
            return nearestSpot.GetComponent<FollowTarget>();
        } else
        {
            return null;
        }
    }

    public override void Exit()
    {
    }

    //public override void OnTriggerEnter(Collider2D collision)
    //{
    //    base.OnTriggerEnter(collision);

    //    if (collision.GetComponent<PlayerController>())
    //    {
    //        Controller.FollowTarget = collision.GetComponent<FollowTarget>();
    //        Controller.ChangeState(new KidLightState(Controller));
    //    }
    //}
}
