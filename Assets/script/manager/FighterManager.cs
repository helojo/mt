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

    private Animator effect;
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
        effect = obj.GetComponent<Animator>();
        //effect.Play("dao");
        //DestroyObject(effect);
        
        InitFightersFormBattleDate();
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
            createFighter(actor.entry, posIndex, 1f, actor);
        }
    }

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
    public GameObject createFighter(int entry, int pos, float scale, FighterData actor)
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


    private const float offsetH = 1.6f;
    private const float offsetV = 1.6f;
    private const float offsetD = 3.6f;
    private void PlaceFighter(Fighter fighter, int posIndex, bool isPlayer)
    {
        Vector3 zero = Vector3.zero;
        Quaternion identity = Quaternion.identity;
        Debug.Log(isPlayer);
        
        if (isPlayer)
        {
            //fighter.transform.localScale *= SCALE;
            float x = 0;
            float y = offsetD / 2;
            //y
            if (posIndex > 3)
            {
                y += offsetV;
            }
            if (posIndex % 2 == 0)
            {
                y = -y;
            }
            //x
            switch (posIndex / 2)
            {
                case 0:
                    x = -offsetH / 2;
                    break;
                case 1:
                    x = offsetH / 2;
                    break;
                case 2:
                    x = -offsetH;
                    break;
                case 3:
                    x = 0;
                    break;
                case 4:
                    x = offsetH;
                    break;
            }
            Debug.Log(fighter.name + "_" + x+"_"+y);
            zero = new Vector3(x, y, 0);
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
        //fighter.transform.TransformPoint(zero);
        fighter.transform.Translate(zero);
        //fighter.transform.Translate(x, y, 0);
        fighter.transform.rotation = identity;
    }
}
