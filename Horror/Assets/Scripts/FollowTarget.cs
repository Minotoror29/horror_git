using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowTarget : MonoBehaviour
{
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float runDistance = 2.5f;

    [SerializeField] private int priority = 0;

    public float StopDistance { get { return stopDistance; } }
    public float RunDistance { get { return runDistance; } }
    public int Priority { get { return priority; } }

    public abstract KidState EnterStopDistance(KidController kid);
}
