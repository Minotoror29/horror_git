using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private EnemyState _currentState;

    [SerializeField] private float walkSpeed = 100f;
    [SerializeField] private float runSpeed = 150f;

    [SerializeField] private float killDistance = 1f;
    [SerializeField] private float eatingTime = 5f;

    [SerializeField] private float patrolRadius = 1f;
    [SerializeField] private float patrolWaitTime = 2f;
    private Vector2 _patrolCenter;

    public float KillDistance { get { return killDistance; } }
    public float EatingTime { get { return eatingTime; } }
    public float PatrolRadius { get { return patrolRadius; } }
    public float PatrolWaitTime { get { return patrolWaitTime; } }
    public Vector2 PatrolCenter { get { return _patrolCenter; } }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateLogic();
    }

    private void FixedUpdate()
    {
        UpdatePhysics();
    }

    public void Initialize()
    {
        _rb = GetComponent<Rigidbody2D>();

        _patrolCenter = transform.position;

        ChangeState(new EnemyPatrolState(this));
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

    public void MoveTowardsTarget(Vector2 target)
    {
        _rb.velocity = walkSpeed * Time.fixedDeltaTime * (target - (Vector2)transform.position).normalized;
    }

    public void RunTowardsTarget(Transform target)
    {
        _rb.velocity = runSpeed * Time.fixedDeltaTime * (target.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentState.OnTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
