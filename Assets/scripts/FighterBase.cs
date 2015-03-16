using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FighterBase : MonoBehaviour
{
    public virtual void OnCreateInit()
    {
    }

    public void OnMsgCreateInit(BattleData _gameData)
    {
        this.battleData = _gameData;
        this.OnCreateInit();
    }

    public BattleData battleData { get; private set; }
}

