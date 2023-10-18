using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : FollowTarget
{
    private Rigidbody2D _rb;
    private PlayerControls _playerControls;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 150f;
    private Vector2 _movementDirection;

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

        _playerControls = new PlayerControls();
        _playerControls.InGame.Enable();
    }

    public void UpdateLogic()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        _movementDirection = _playerControls.InGame.Movement.ReadValue<Vector2>();
    }

    public void UpdatePhysics()
    {
        Move();
    }

    private void Move()
    {
        _rb.velocity = _movementDirection * Time.fixedDeltaTime * movementSpeed;
    }

    public override KidState EnterStopDistance(KidController kid)
    {
        return new KidIdleState(kid);
    }
}
