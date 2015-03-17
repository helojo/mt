using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FighterManager : FighterBase {

    //private FighterManager fighterManager;

    public List<Vector3> teamPos = new List<Vector3>();//位置列表
    private List<Fighter> fighters { get; set; }//战斗对象
    public GameObject playerTeamObj { get; private set; }
    public TeamMove teamMove { get; set; }
    private GameObject fighterCard;
    private GameObject fighterEffect;
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public FighterManager()
    {
        fighters = new List<Fighter>(new Fighter[BattleGlobal.FighterNumberMax * 2]);
        fighterCard = (GameObject)Resources.Load("Prefabs/hero");
        //fighterEffect = (GameObject)Resources.Load("Prefabs/fightEffect");
        fighterEffect = (GameObject)Resources.Load("Prefabs/fightEffect");
    }

    public override void OnCreateInit()
    {
        BattleData battleData = base.battleData;
        battleData.OnMsgEnter = (Action)Delegate.Combine(battleData.OnMsgEnter, new Action(OnMsgEnter));
        BattleData data2 = base.battleData;
        //data2.OnMsgLeave = (System.Action)Delegate.Combine(data2.OnMsgLeave, new System.Action(this.OnMsgLeave));
        //FighterData data3 = base.fighterData;
        //data3.OnMsgTimeScaleChange = (System.Action)Delegate.Combine(data3.OnMsgTimeScaleChange, new System.Action(this.OnMsgTimeScaleChange));
        //FighterData data4 = base.fighterData;
        //data4.OnMsgGridGameFinishOneBattle = (Action<bool, bool, BattleNormalGameResult>)Delegate.Combine(data4.OnMsgGridGameFinishOneBattle, new Action<bool, bool, BattleNormalGameResult>(this.OnMsgGridGameFinishOneBattle));
        //FighterData data5 = base.fighterData;
        //data5.OnMsgPhaseChange = (System.Action)Delegate.Combine(data5.OnMsgPhaseChange, new System.Action(this.OnMsgPhaseChange));
        ////base.OnCreateInit();
        //Debug.Log("alet");
    }

    private void OnMsgEnter()
    {
        //if (!BattleSceneStarter.G_isTestEnable)
        //{
        //    InitFightersFormBattleDate();
        //}
        //Debug.Log(fighterEffect);
        GameObject obj = (GameObject)Instantiate(fighterEffect);
        Animator effect = obj.GetComponent<Animator>();
        effect.Play("dao");
        //DestroyObject(effect);
        
        //InitFightersFormBattleDate();
        //Debug.Log(effect);
    }

    public void InitFightersFormBattleDate()
    {
        if (base.battleData.attActor != null)
        {
            int posIndex = 0;
            foreach (FighterData actor in base.battleData.attActor)
            {
                AddFighter(actor, posIndex);
                posIndex++;
            }
            //OnFighterInitFinish();
        }
    }


    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 添加参战角色
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="posIndex"></param>
    private void AddFighter(FighterData actor, int posIndex)
    {
        if ((actor != null) && (actor.entry != -1))
        {
            if (actor.isHero)
            {
                createFighter(actor.entry, posIndex, 1f, actor , false);
            }
            else
            {
                //createMonsterFighter(actor.entry, posIndex, actor);
            }
        }
    }

    /// <summary>
    ///  创建怪物
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="pos"></param>
    /// <param name="detailActor"></param>
    /// <param name="serverIdx"></param>
    /// <returns></returns>
    //public GameObject createMonsterFighter(int entry, int pos, FighterData actor, int serverIdx)
    //{
    //    //monster_config _config = ConfigMgr.getInstance().getByEntry<monster_config>(entry);
    //    //if (_config == null)
    //    //{
    //    //    Debug.LogWarning("Can't find monster config ID: " + entry.ToString());
    //    //    _config = ConfigMgr.getInstance().getByEntry<monster_config>(0);
    //    //}
    //    //if (_config != null)
    //    //{
    //    //    return createFighter(_config.card_entry, pos, _config.zoom, actor, serverIdx, _config.type == 2);
    //    //}
    //    //return null;
    //}

    /// <summary>
    ///  创建角色
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="pos"></param>
    /// <param name="scale"></param>
    /// <param name="detailActor"></param>
    /// <param name="serverIdx"></param>
    /// <param name="isBigBoss"></param>
    /// <returns></returns>
    public GameObject createFighter(int entry, int pos, float scale, FighterData actor, bool isBigBoss = false)
    {
        if (entry < 0)
        {
            return null;
        }
        bool flag = pos < BattleGlobal.FighterNumberOneSide;
        GameObject obj2 = (GameObject)Instantiate(fighterCard);
        obj2.name = "fighter " + pos.ToString();
        Fighter fighter = obj2.AddComponent<Fighter>();
        fighter.PosIndex = pos;
        fighter.Init(base.battleData, scale, flag, entry, actor);
        fighters[pos] = fighter;
        UpdateFighterPos(pos);
        return obj2;
    }

    /// <summary>
    /// 更新战斗角色位置
    /// </summary>
    /// <param name="index"></param>
    private void UpdateFighterPos(int index)
    {
        if (fighters[index] != null)
        {
            PlaceFighter(this.fighters[index], index, fighters[index].isPlayer);
        }
    }

    private void PlaceFighter(Fighter fighter, int posIndex, bool isPlayer)
    {
        Vector3 zero = Vector3.zero;
        Quaternion identity = Quaternion.identity;
        if (isPlayer)
        {
            //zero = base.gameObject.GetComponent<BattleCom_ScenePosManager>().GetSceneFighterStartPosByPhase(fighter.GetIndexAtLive());
            //identity = Quaternion.LookRotation(base.gameObject.GetComponent<BattleCom_ScenePosManager>().GetSceneFighterDirByPhase());
        }
        //else
        //{
        //    int monsterIndex = posIndex - BattleGlobal.FighterNumberMax;
        //    if ((monsterIndex >= 0) && (monsterIndex < BattleGlobal.FighterNumberOneSide))
        //    {
        //            zero = base.gameObject.GetComponent<BattleCom_ScenePosManager>().GetSceneFighterEndPosByPhase(monsterIndex + BattleGlobal.FighterNumberOneSide);
        //            identity = Quaternion.LookRotation(-base.gameObject.GetComponent<BattleCom_ScenePosManager>().GetSceneFighterDirByPhase());
        //    }
        //}
        fighter.transform.TransformPoint(zero);
        fighter.transform.rotation = identity;
    }
}
