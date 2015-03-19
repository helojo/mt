using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameMain : GameBase
{
    public GameObject gameMainObj;
    private bool isStarted;

    public override void Init()
    {
        base.Init();
        BattleData data = new BattleData();
        base.battleGameData = data;
        base.battleGameData.Init();
        gameMainObj = new GameObject("GameMain");
        UnityEngine.Object.DontDestroyOnLoad(gameMainObj);
        base.battleGameData.gameMainObj = gameMainObj;
        base.battleGameData.gameMainObj.AddComponent<BackgroundManager>();
        base.battleGameData.gameMainObj.AddComponent<FighterManager>();
        base.battleGameData.gameMainObj.SendMessage("OnMsgCreateInit", base.battleGameData);
    }

    public override void OnEnter()
    {
        Debug.Log("[GameMain] 进入游戏");
        
        base.battleGameData.OnMsgEnter();
    }

}

