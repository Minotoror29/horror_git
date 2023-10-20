using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState
{
    public PlayState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        GameManager.Player.UpdateLogic();
        foreach (KidController kid in GameManager.LivingKids)
        {
            kid.UpdateLogic();
        }
        foreach (EnemyController enemy in GameManager.Enemies)
        {
            enemy.UpdateLogic();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        GameManager.Player.UpdatePhysics();
        foreach (KidController kid in GameManager.LivingKids)
        {
            kid.UpdatePhysics();
        }
        foreach (EnemyController enemy in GameManager.Enemies)
        {
            enemy.UpdatePhysics();
        }
    }
}
