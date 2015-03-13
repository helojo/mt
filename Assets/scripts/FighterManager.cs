using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FighterManager : FighterBase {

    //private FighterManager fighterManager;

    public List<Vector3> teamPos = new List<Vector3>();
    private List<Fighter> fighters { get; set; }//战斗对象
    public GameObject playerTeamObj { get; private set; }
    public TeamMove teamMove { get; set; }

    /// <summary>
    /// 战斗管理器
    /// </summary>
    public FighterManager()
    {
        fighters = new List<Fighter>(new Fighter[BattleGlobal.FighterNumberMax * 2]);
    }

    public override void OnCreateInit()
    {
        FighterData fighterData = base.fighterData;
        fighterData.OnMsgEnter = (System.Action)Delegate.Combine(fighterData.OnMsgEnter, new System.Action(OnMsgEnter));
        FighterData data2 = base.fighterData;
        //data2.OnMsgLeave = (System.Action)Delegate.Combine(data2.OnMsgLeave, new System.Action(this.OnMsgLeave));
        //FighterData data3 = base.fighterData;
        //data3.OnMsgTimeScaleChange = (System.Action)Delegate.Combine(data3.OnMsgTimeScaleChange, new System.Action(this.OnMsgTimeScaleChange));
        //FighterData data4 = base.fighterData;
        //data4.OnMsgGridGameFinishOneBattle = (Action<bool, bool, BattleNormalGameResult>)Delegate.Combine(data4.OnMsgGridGameFinishOneBattle, new Action<bool, bool, BattleNormalGameResult>(this.OnMsgGridGameFinishOneBattle));
        //FighterData data5 = base.fighterData;
        //data5.OnMsgPhaseChange = (System.Action)Delegate.Combine(data5.OnMsgPhaseChange, new System.Action(this.OnMsgPhaseChange));
        ////base.OnCreateInit();
    }

    private void OnMsgEnter()
    {
        //if (!BattleSceneStarter.G_isTestEnable)
        //{
        //    this.InitFightersFormBattleDate();
        //}
    }

    void Start()
    {
    }

    void Update()
    {

    }
}
