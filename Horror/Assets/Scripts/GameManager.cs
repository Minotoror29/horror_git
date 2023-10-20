using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _livingKids = 0;
    private int _kidsNearExit = 0;

    [SerializeField] private Exit exit;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (KidController kid in FindObjectsOfType<KidController>())
        {
            _livingKids++;
            kid.Initialize(this);
        }

        exit.Initialize(this);
    }

    public void AddKidNearExit()
    {
        _kidsNearExit++;
        if (_kidsNearExit == _livingKids)
        {
            Debug.Log("Win");
        }
    }

    public void RemoveKidNearExit()
    {
        _kidsNearExit--;
    }

    public void KidDeath()
    {
        _livingKids--;

        if (_livingKids == 0)
        {
            Debug.Log("Lose");
        }
    }
}
