using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    private State _currentSubstate;
    private State _currentSuperstate;

    public State CurrenSubstate { get { return _currentSubstate; } }
    public State CurrentSuperstate { get { return _currentSuperstate; } }

    public abstract void Enter();

    public abstract void Exit();

    public virtual void UpdateLogic()
    {
        _currentSubstate?.UpdateLogic();
    }

    public virtual void UpdatePhysics()
    {
        _currentSubstate?.UpdatePhysics();
    }

    public virtual void OnTriggerEnter(Collider2D collision)
    {
    }

    public virtual void OnTriggerExit(Collider2D collision)
    {
    }

    public void ChangeSubstate(State nextSubstate)
    {
        _currentSubstate?.Exit();
        _currentSubstate = nextSubstate;
        _currentSubstate.SetSuperstate(this);
        _currentSubstate.Enter();
    }

    public void SetSuperstate(State nextSuperstate)
    {
        _currentSuperstate = nextSuperstate;
    }
}
