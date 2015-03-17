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
        //英雄
        teamPos.Add(new Vector3(-2, -1.2f, 0));
        teamPos.Add(new Vector3(0, -1.2f, 0));
        teamPos.Add(new Vector3(2, -1.2f, 0));
        teamPos.Add(new Vector3(-2, -3.6f, 0));
        teamPos.Add(new Vector3(0, -3.6f, 0));
        teamPos.Add(new Vector3(2, -3.6f, 0));
        //怪物
        teamPos.Add(new Vector3(-2, 1.2f, 0));
        teamPos.Add(new Vector3(0, 1.2f, 0));
        teamPos.Add(new Vector3(2, 1.2f, 0));
        teamPos.Add(new Vector3(-2, 3.6f, 0));
        teamPos.Add(new Vector3(0, 3.6f, 0));
        teamPos.Add(new Vector3(2, 3.6f, 0));
        
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

    private float timer = 0f;
    public const float attackDistance = 1f;
    void Update()
    {
        //if (BackgroundManager.state == BackgroundManager.State.fight)
        //{
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = attackDistance;
                Debug.Log("攻击一回");
            }
        //}
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


    private const float offsetH = 3f;
    private const float offsetV = 1.6f;
    private const float offsetD = 3.6f;
    private void PlaceFighter(Fighter fighter, int posIndex, bool isPlayer)
    {
        Vector3 zero = Vector3.zero;
        Quaternion identity = Quaternion.identity;
        Debug.Log(isPlayer);
        zero = teamPos[posIndex];
        fighter.transform.Translate(zero);
        fighter.transform.rotation = identity;
    }
}
