using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpot : FollowTarget
{
    public override KidState EnterStopDistance(KidController kid)
    {
        return new KidHiddenState(kid, this);
    }
}
