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
        this.fighterData = _gameData;
        this.OnCreateInit();
    }

    public BattleData fighterData { get; private set; }
}

