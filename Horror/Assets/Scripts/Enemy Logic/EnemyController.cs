using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private EnemyState _currentState;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 100f;
    [SerializeField] private float runSpeed = 150f;
    [SerializeField] private bool isAfraidOfLight = false;
    [SerializeField] private bool isAttractedByLight = false;

    [Header("Chasing")]
    [SerializeField] private float killDistance = 1f;
    [SerializeField] private float eatingTime = 5f;
    [SerializeField] private bool canEatInBushes = false;
    private List<KidController> _kidsInRange;

    [Header("Patrol")]
    [SerializeField] private bool canPatrol = true;
    [SerializeField] private float patrolRadius = 1f;
    [SerializeField] private float patrolWaitTime = 2f;
    private Vector2 _patrolCenter;

    public bool IsAfraidOfLight { get { return isAfraidOfLight; } }
    public bool IsAttractedByLight { get { return isAttractedByLight; } }
    public float KillDistance { get { return killDistance; } }
    public float EatingTime { get { return eatingTime; } }
    public bool CanEatInBushes { get { return canEatInBushes; } }
    public List<KidController> KidsInRange { get { return _kidsInRange; } }
    public bool CanPatrol { get { return canPatrol; } }
    public float PatrolRadius { get { return patrolRadius; } }
    public float PatrolWaitTime { get { return patrolWaitTime; } }
    public Vector2 PatrolCenter { get { return _patrolCenter; } }

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();

        _kidsInRange = new();
        _patrolCenter = transform.position;

        ChangeState(new EnemyIdleState(this));
    }

    public void ChangeState(EnemyState nextState)
    {
        State substate = _currentState?.CurrenSubstate;
        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
        if (substate != null)
        {
            _currentState.ChangeSubstate(substate);
        }
    }

    public void UpdateLogic()
    {
        _currentState.UpdateLogic();
    }

    public void UpdatePhysics()
    {
        _currentState.UpdatePhysics();
    }

    public void StopMovement()
    {
        _rb.velocity = Vector3.zero;
    }

    public void WalkTowardsTarget(Vector2 target)
    {
        _rb.velocity = walkSpeed * Time.fixedDeltaTime * (target - (Vector2)transform.position).normalized;
    }

    public void RunTowardsTarget(Transform target)
    {
        _rb.velocity = runSpeed * Time.fixedDeltaTime * (target.position - transform.position).normalized;
    }

    public void RunFromTarget(Transform target)
    {
        _rb.velocity = runSpeed * Time.fixedDeltaTime * -(target.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KidController kid = collision.GetComponent<KidController>();
        if (kid)
        {
            _kidsInRange.Add(kid);
        }

        _currentState.OnTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        KidController kid = collision.GetComponent<KidController>();
        if (_kidsInRange.Contains(kid))
        {
            _kidsInRange.Remove(kid);
        }

        _currentState.OnTriggerExit(collision);
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmoCenter;

        if (_patrolCenter == Vector2.zero)
        {
            gizmoCenter = transform.position;
        } else
        {
            gizmoCenter = _patrolCenter;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gizmoCenter, patrolRadius);
    }
}
