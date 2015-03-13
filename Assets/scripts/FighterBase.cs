using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FighterBase : MonoBehaviour
{
    public virtual void OnCreateInit()
    {
    }

    public void OnMsgCreateInit(FighterData _gameData)
    {
        this.fighterData = _gameData;
        this.OnCreateInit();
    }

    public FighterData fighterData { get; private set; }
}

