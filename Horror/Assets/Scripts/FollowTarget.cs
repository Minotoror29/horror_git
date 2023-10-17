using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float stopDistance;
    [SerializeField] private float runDistance;

    [SerializeField] private int priority;

    public float StopDistance { get { return stopDistance; } }
    public float RunDistance { get { return runDistance; } }
    public int Priority { get { return priority; } }
}
