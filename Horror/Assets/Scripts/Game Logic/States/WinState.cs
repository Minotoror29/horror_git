using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : GameState
{
    public WinState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        GameManager.WindDisplay.gameObject.SetActive(true);
        GameManager.WindDisplay.Initialize(GameManager.LivingKids.Count);

        GameManager.Player.StopMovement();
        foreach (KidController kid in GameManager.LivingKids)
        {
            kid.StopMovement();
        }
        foreach (EnemyController enemy in GameManager.Enemies)
        {
            enemy.StopMovement();
        }
    }

    public override void Exit()
    {
        GameManager.WindDisplay.gameObject.SetActive(false);
    }
}
