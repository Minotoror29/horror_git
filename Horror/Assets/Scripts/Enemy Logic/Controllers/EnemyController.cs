using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private EnemyState _currentState;

    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private float patrolRadius = 1f;
    private Vector2 _patrolCenter;

    public float PatrolRadius { get { return patrolRadius; } }
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

    public void MoveTowardsTarget(Vector2 target)
    {
        _rb.velocity = movementSpeed * Time.fixedDeltaTime * (target - (Vector2)transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_patrolCenter, patrolRadius);
    }
}
