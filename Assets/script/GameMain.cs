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
        base.battleGameData.attActor = new List<FighterData>();
        base.battleGameData.defActor = new List<FighterData>();

        //////////////////////////////////////////////////////////////////////////
        //测试数据
        for (int i = 0; i < 3; i++)
        {
            FighterData f1 = new FighterData();
            f1.entry = i;
            f1.maxHp = 100;
            f1.curHp = 100;
            base.battleGameData.attActor.Add(f1);
        }
        for (int i = 0; i < 3; i++)
        {
            FighterData f1 = new FighterData();
            f1.entry = BattleGlobal.FighterNumberOneSide+i;
            f1.maxHp = 100;
            f1.curHp = 100;
            base.battleGameData.defActor.Add(f1);
        }
        //////////////////////////////////////////////////////////////////////////

        base.battleGameData.OnMsgEnter();
    }

    public override void OnLeave()
    {
        //base.OnLeave();
        //if (base.battleGameData.OnMsgLeave != null)
        //{
        //    base.battleGameData.OnMsgLeave();
        //}
    }

}

