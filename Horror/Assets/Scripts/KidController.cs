using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KidController : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private Transform followTarget;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float runDistance = 5f;
    private float _targetDistance;

    [SerializeField] private float walkSpeed = 100f;
    [SerializeField] private float runSpeed = 150f;
    private float _currentMovementSpeed;

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
    }

    public void UpdateLogic()
    {
        _targetDistance = (followTarget.position - transform.position).magnitude;

        if (_targetDistance <= stopDistance)
        {
            _currentMovementSpeed = 0f;
        } else if (_targetDistance > stopDistance && _targetDistance < runDistance)
        {
            _currentMovementSpeed = walkSpeed;
        } else if (_targetDistance >= runDistance)
        {
            _currentMovementSpeed = runSpeed;
        }
    }

    public void UpdatePhysics()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        _rb.velocity = _currentMovementSpeed * Time.fixedDeltaTime * (followTarget.position - transform.position).normalized;
    }
}
