﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameMain : GameBase
{
    public GameObject gameMainObj;
    private bool isStarted;

    //public void DoBattle(int point, CombatCrewData combatData, List<MonsterDrop> dropInfoes)
    //{
    //    BattleSecurityManager.Instance.Init();
    //    BattleStaticEntry.IsInBattle = true;
    //    base.battleGameData.attActor = combatData.attacker.actor;
    //    base.battleGameData.defActor = combatData.defenderList;
    //    base.battleGameData.drops = dropInfoes;
    //    base.battleGameData.randomSeed = combatData.seed;
    //    StartBattle();
    //    GUIMgr.Instance.ExitModelGUI("SelectHeroPanel");
    //}

    public override void Init()
    {
        base.Init();
        BattleData data = new BattleData();
        base.battleGameData = data;
        base.battleGameData.Init();
        gameMainObj = new GameObject("GameMain");
        UnityEngine.Object.DontDestroyOnLoad(gameMainObj);
        base.battleGameData.gameMainObj = gameMainObj;
        //base.battleGameData.battleComObject.AddComponent<BattleCom_CameraManager>().SetEnable(false);
        //base.battleGameData.battleComObject.AddComponent<BattleGridGameMapControl>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_TestManager>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_ScenePosManager>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_UIControl>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_StoryControl>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_PlayerControl>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_Runtime>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_PhaseManager>();
        //base.battleGameData.battleComObject.AddComponent<BattleCom_CameraManager>();
        base.battleGameData.gameMainObj.AddComponent<FighterManager>();
        base.battleGameData.gameMainObj.SendMessage("OnMsgCreateInit", base.battleGameData);
    }

    public override void OnEnter()
    {
        Debug.Log("[GameMain] 进入游戏");
        //gameMainObj.GetComponent<BattleCom_ScenePosManager>().InitSceneInfo();
        //gameMainObj.GetComponent<BattleCom_CameraManager>().InitBindCamera();
        //gameMainObj.GetComponent<BattleCom_CameraManager>().SetEnable(false);
        //base.battleGameData.attActor = null;
        //base.battleGameData.defActor = null;
        //base.battleGameData.drops = null;
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

    private void StartBattle()
    {
        //<StartBattle>c__AnonStoreyF1 yf = new <StartBattle>c__AnonStoreyF1();
        //isStarted = true;
        //base.battleGameData.CurBattlePhase = 0;
        //base.battleGameData.phaseNumber = 1;
        //base.battleGameData.startPhase = 0;
        //base.battleGameData.startAnim = string.Empty;
        //base.battleGameData.timeScale_ShowTime = 1f;
        //yf.isBigBoss = false;
        //if (base.battleGameData.defActor.Count > 0)
        //{
        //    base.battleGameData.defActor[0].actor.ForEach(new Action<CombatDetailActor>(yf.<>m__6D));
        //}
        //base.battleGameData.IsBossBattle = yf.isBigBoss;
        //if (yf.isBigBoss)
        //{
        //    Debug.Log("????BOOS");
        //    base.battleGameData.cameraShotType = ShotState.CloseUp1;
        //}
        //else
        //{
        //    Debug.Log("??????");
        //    base.battleGameData.cameraShotType = ShotState.Battle;
        //}
        //(base.battleGameData.battleComObject.GetComponent<BattleCom_ScenePosManager>().impl as BattleCom_ScenePosImplGrid).SetGridBattleType(yf.isBigBoss);
        //base.battleGameData.isAutoEnable = true;
        //BattleGlobal.SetShowTimeScale(1f);
        //battleComObject.GetComponent<BattleCom_FighterManager>().InitPlayerTeam();
        //base.battleGameData.battleComObject.GetComponent<BattleCom_Runtime>().CacheResources();
        //base.battleGameData.battleComObject.GetComponent<BattleCom_CameraManager>().GetCurCamera().Reset();
        //base.battleGameData.battleComObject.GetComponent<BattleCom_PhaseManager>().BeginPhaseStarting();
    }

    //[CompilerGenerated]
    //private sealed class <StartBattle>c__AnonStoreyF1
    //{
    //    internal bool isBigBoss;

    //    internal void <>m__6D(CombatDetailActor obj)
    //    {
    //        if (!obj.isCard)
    //        {
    //            monster_config _config = ConfigMgr.getInstance().getByEntry<monster_config>(obj.entry);
    //            if ((_config != null) && (_config.type == 2))
    //            {
    //                isBigBoss = true;
    //            }
    //        }
    //    }
    //}
}

