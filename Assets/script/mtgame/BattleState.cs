using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BattleState : MonoBehaviour
{
    private static BattleState _instance;
    private bool isEntered;
    public State state = State.walk;

    public enum State
    {
        walk, fight, win, fail
    }


    public static BattleState GetInstance()
    {
        if (_instance == null)
        {
            GameObject target = new GameObject("StateInstance");
            DontDestroyOnLoad(target);
            _instance = target.AddComponent<BattleState>();
            _instance.Init();
        }
        return _instance;
    }

    private void Init()
    {
        CurGame = base.gameObject.AddComponent<GameMain>();
        CurGame.Init();
    }

    public void OnEnter()
    {
        if (CurGame != null)
        {
            CurGame.OnEnter();
        }
        isEntered = true;
    }

    public void OnLeave()
    {
        if (CurGame != null)
        {
            CurGame.OnLeave();
        }
        isEntered = false;
    }

    public GameBase CurGame { get; set; }

}

