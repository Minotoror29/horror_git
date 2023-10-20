using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _currentState;

    private PlayerController _player;

    private List<KidController> _livingKids;
    private int _kidsNearExit = 0;

    private List<EnemyController> _enemies;

    [SerializeField] private Exit exit;

    [SerializeField] private WinDisplay winDisplay;
    [SerializeField] private Canvas loseCanvas;

    public PlayerController Player { get { return _player; } }
    public List<KidController> LivingKids { get { return _livingKids; } }
    public List<EnemyController> Enemies { get { return _enemies; } }
    public WinDisplay WindDisplay { get { return winDisplay; } }
    public Canvas LoseCanvas { get { return loseCanvas; } }

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
        _player = FindObjectOfType<PlayerController>();
        _player.Initialize();

        _livingKids = new();
        foreach (KidController kid in FindObjectsOfType<KidController>())
        {
            _livingKids.Add(kid);
            kid.Initialize(this);
        }

        _enemies = new();
        foreach (EnemyController enemy in FindObjectsOfType<EnemyController>())
        {
            _enemies.Add(enemy);
            enemy.Initialize();
        }

        exit.Initialize(this);

        ChangeState(new PlayState(this));
    }

    public void ChangeState(GameState nextState)
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

    private void UpdateLogic()
    {
        _currentState.UpdateLogic();
    }

    private void UpdatePhysics()
    {
        _currentState.UpdatePhysics();
    }

    public void AddKidNearExit()
    {
        _kidsNearExit++;
        if (_kidsNearExit == _livingKids.Count)
        {
            ChangeState(new WinState(this));
        }
    }

    public void RemoveKidNearExit()
    {
        _kidsNearExit--;
    }

    public void KidDeath(KidController kid)
    {
        _livingKids.Remove(kid);

        if (_livingKids.Count == 0)
        {
            ChangeState(new LoseState(this));
        }
    }
}
