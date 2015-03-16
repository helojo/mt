using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameBase : MonoBehaviour
{
    public virtual void Init()
    {
        battleGameData = new BattleData();
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnLeave()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public BattleData battleGameData { get; set; }
}

