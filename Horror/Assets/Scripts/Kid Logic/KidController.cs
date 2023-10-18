using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class KidController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private KidState _currentState;

    private List<FollowTarget> _followTargets;
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

    private List<EnemyController> _enemiesInRange;
    private bool _isHidden = false;

    public List<FollowTarget> FollowTargets { get { return _followTargets; } set { _followTargets = value; } }
    public float TargetDistance { get { return _targetDistance; } }
    public float MaxStamina { get { return maxStamina; } }
    public float CurrentStamina { get { return _currentStamina; } }
    public bool IsHidden { get { return _isHidden; } set { _isHidden = value; } }
    public List<EnemyController> EnemiesInRange { get { return _enemiesInRange; } }

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
        _followTargets = new();
        _currentStamina = maxStamina;
        _enemiesInRange = new();

        //KidState startState = new KidDarkState(this);
        //ChangeState(startState);
        //startState.ChangeSubstate(new KidIdleState(this));
        ChangeState(new KidIdleState(this));
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

        _followTargets = _followTargets.OrderBy(target => target.Priority).ToList();

        if (_followTargets.Count > 0)
        {
            _targetDistance = (_followTargets[0].transform.position - transform.position).magnitude;
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

    public void StopMovement()
    {
        _rb.velocity = Vector2.zero;
    }

    public void WalkTowardsTarget()
    {
        _rb.velocity = walkSpeed * Time.fixedDeltaTime * (_followTargets[0].transform.position - transform.position).normalized;
    }

    public void RunTowardsTarget()
    {
        _rb.velocity = runSpeed * Time.fixedDeltaTime * (_followTargets[0].transform.position - transform.position).normalized;
    }

    public void RunFromEnemies()
    {
        Vector3 enemiesVectors = Vector3.zero;
        foreach (EnemyController enemy in _enemiesInRange)
        {
            enemiesVectors += (enemy.transform.position - transform.position).normalized;
        }

        _rb.velocity = runSpeed * Time.fixedDeltaTime * ((transform.position - enemiesVectors.normalized) - transform.position).normalized;
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
        FollowTarget target = collision.GetComponent<FollowTarget>();
        if (target)
        {
            _followTargets.Add(target);
        }

        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy)
        {
            _enemiesInRange.Add(enemy);
        }

        _currentState.OnTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FollowTarget target = collision.GetComponent<FollowTarget>();
        if (target && _followTargets.Contains(target))
        {
            _followTargets.Remove(target);
        }

        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy && _enemiesInRange.Contains(enemy))
        {
            _enemiesInRange.Remove(enemy);
        }

        _currentState.OnTriggerExit(collision);
    }
}
