using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameState
{
    public LoseState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        GameManager.LoseCanvas.gameObject.SetActive(true);

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
        GameManager.LoseCanvas.gameObject.SetActive(false);
    }
}
