using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameManager _gameManager;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<KidController>())
        {
            _gameManager.AddKidNearExit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _gameManager.RemoveKidNearExit();
    }
}
