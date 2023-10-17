using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KidState : State
{
    private KidController _controller;

    public KidController Controller { get { return _controller; } }

    public KidState(KidController controller)
    {
        _controller = controller;
    }
}
