using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class KidController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private KidState _currentState;

    [Header("Target")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float runDistance = 5f;
    private float _targetDistance;

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
    public float TargetDistance { get { return _targetDistance; } }
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

        _currentStamina = maxStamina;

        ChangeState(new KidIdleState(this));
    }

    public void ChangeState(KidState nextState)
    {
        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    public void UpdateLogic()
    {
        _currentState.UpdateLogic();

        _targetDistance = (followTarget.position - transform.position).magnitude;

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
        _rb.velocity = walkSpeed * Time.fixedDeltaTime * (followTarget.position - transform.position).normalized;
    }

    public void RunTowardsTarget()
    {
        _rb.velocity = runSpeed * Time.fixedDeltaTime * (followTarget.position - transform.position).normalized;
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
}
