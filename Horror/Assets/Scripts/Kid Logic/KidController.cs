using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class KidController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private KidState _currentState;

    [Header("Target")]
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float runDistance = 5f;
    private Transform _followTarget;
    private float _targetDistance;

    public List<HideSpot> _hideSpots;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 100f;
    [SerializeField] private float runSpeed = 150f;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 10f;
    [SerializeField] private float staminaDepletionSpeed = 1f;
    [SerializeField] private float staminaRecoverySpeed = 2f;
    [SerializeField] private Image staminaBar;
    private float _currentStamina;

    public float StopDistance { get { return stopDistance; } }
    public float RunDistance { get { return runDistance; } }
    public Transform FollowTarget { get { return _followTarget; } set { _followTarget = value; } }
    public float TargetDistance { get { return _targetDistance; } }
    public List<HideSpot> HideSpots { get { return _hideSpots; } }
    public float MaxStamina { get { return maxStamina; } }
    public float CurrentStamina { get { return _currentStamina; } }

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
        _hideSpots = new();
        _currentStamina = maxStamina;

        KidState startState = new KidShadowState(this);
        ChangeState(startState);
        startState.ChangeSubstate(new KidIdleState(this));
    }

    public void ChangeState(KidState nextState)
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

        if (_followTarget != null)
        {
            _targetDistance = (_followTarget.position - transform.position).magnitude;
        } else
        {
            _targetDistance = -1f;
        }

        UpdateStaminaBar();
    }

    public void UpdatePhysics()
    {
        _currentState.UpdatePhysics();
    }

    public void StopMoving()
    {
        _rb.velocity = Vector2.zero;
    }

    public void WalkTowardsTarget()
    {
        _rb.velocity = walkSpeed * Time.fixedDeltaTime * (_followTarget.position - transform.position).normalized;
    }

    public void RunTowardsTarget()
    {
        _rb.velocity = runSpeed * Time.fixedDeltaTime * (_followTarget.position - transform.position).normalized;
    }

    public void LoseStamina()
    {
        _currentStamina -= Time.deltaTime * staminaDepletionSpeed;

        _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);
    }

    public void RecoverStamina()
    {
        _currentStamina += Time.deltaTime * staminaRecoverySpeed;

        _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);
    }

    private void UpdateStaminaBar()
    {
        float barWidth = _currentStamina / maxStamina;

        staminaBar.transform.localScale = new Vector2(barWidth, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentState.OnTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentState.OnTriggerExit(collision);
    }
}
