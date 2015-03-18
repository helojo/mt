using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FighterManager : BattleBase {

    //private FighterManager fighterManager;

    public List<Vector3> teamPos = new List<Vector3>();//位置列表
    public List<Vector3> attrackPos = new List<Vector3>();//攻击站位列表

    private List<Fighter> fighters { get; set; }//战斗对象
    public GameObject playerTeamObj { get; private set; }
    //public TeamMove teamMove { get; set; }
    private GameObject fighterCard;
    private GameObject fighterEffect;

    private Animator effect;
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public FighterManager()
    {
        //英雄
        teamPos.Add(new Vector3(-2, -3.6f, 0));
        teamPos.Add(new Vector3(0, -3.6f, 0));
        teamPos.Add(new Vector3(2, -3.6f, 0));
        teamPos.Add(new Vector3(-2, -1.2f, 0));
        teamPos.Add(new Vector3(0, -1.2f, 0));
        teamPos.Add(new Vector3(2, -1.2f, 0));
        
        //怪物
        teamPos.Add(new Vector3(-2, 3.6f, 0));
        teamPos.Add(new Vector3(0, 3.6f, 0));
        teamPos.Add(new Vector3(2, 3.6f, 0));
        teamPos.Add(new Vector3(-2, 1.2f, 0));
        teamPos.Add(new Vector3(0, 1.2f, 0));
        teamPos.Add(new Vector3(2, 1.2f, 0));
        

        attrackPos.Add(new Vector3(-2, -1.2f, 0));
        attrackPos.Add(new Vector3(0, -1.2f, 0));
        attrackPos.Add(new Vector3(2, -1.2f, 0));
        attrackPos.Add(new Vector3(-2, -2.6f, 0));
        attrackPos.Add(new Vector3(0, -2.6f, 0));
        attrackPos.Add(new Vector3(2, -2.6f, 0));

        attrackPos.Add(new Vector3(-2, 1.2f, 0));
        attrackPos.Add(new Vector3(0, 1.2f, 0));
        attrackPos.Add(new Vector3(2, 1.2f, 0));
        attrackPos.Add(new Vector3(-2, 1.2f, 0));
        attrackPos.Add(new Vector3(0, 1.2f, 0));
        attrackPos.Add(new Vector3(2, 1.2f, 0));


        fighters = new List<Fighter>(new Fighter[BattleGlobal.FighterNumberMax * 2]);
        fighterCard = (GameObject)Resources.Load("Prefabs/hero");
        //fighterEffect = (GameObject)Resources.Load("Prefabs/fightEffect");
        fighterEffect = (GameObject)Resources.Load("Prefabs/fightEffect");
    }

    public override void OnCreateInit()
    {
        BattleData battleData = base.battleData;
        battleData.OnMsgEnter = (Action)Delegate.Combine(battleData.OnMsgEnter, new Action(OnMsgEnter));
        //BattleData data2 = base.battleData;
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

    public IEnumerator OnBackgroundMoveEnd()
    {
        if (!monstersShow)
        {
            List<Fighter> monsters = GetMonsterFighters();
            foreach (Fighter f in monsters)
            {
                iTween.FadeTo(f.gameObject, 1, 1);
            }
            yield return new WaitForSeconds(3.0f);
            monstersShow = true;
        }
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
            posIndex = 6;
            foreach (FighterData actor in base.battleData.defActor)
            {
                AddFighter(actor, posIndex);
                posIndex++;
            }
            OnFighterInitFinish();
        }
    }

    private void OnFighterInitFinish()
    {
        List<Fighter> monsters = GetMonsterFighters();
        foreach (Fighter f in monsters)
        {
            //f.gameObject.SetActive(false);
            iTween.FadeTo(f.gameObject, 0,0);
        }
    }


    void Start()
    {
        
    }

    private float timer = 0f;
    public const float attackDistance = 1f;

    private bool inFighing;

    private int curFighter=0;
    private int curDeffi;

    private bool monstersShow;

    void Update()
    {
        //if (BackgroundManager.state == BackgroundManager.State.fight)
        //{
        if(BackgroundManager.state == BackgroundManager.State.fight)
        {
            if (monstersShow && !inFighing)
            {
                Debug.Log(GetMonsterFighters().Count);
                Debug.Log(GetPlayerFighters().Count);
                if (GetMonsterFighters().Count > 0 && GetPlayerFighters().Count > 0)
                {
                    if (curFighter == BattleGlobal.FighterNumberMax - 1)
                        curFighter = 0;
                    if (curFighter < BattleGlobal.FighterNumberOneSide)
                        curDeffi = BattleGlobal.FighterNumberOneSide + curFighter;
                    else
                        curDeffi = curFighter - BattleGlobal.FighterNumberOneSide;

                    Debug.Log("当前攻击者：" + curFighter);
                    attrack(curFighter, curDeffi);
                    curFighter++;
                }
            }
        }
    }


    private bool gameOver;

    void OnGUI()
    {
        if (gameOver)
            GUI.Window(0, new Rect((Screen.width - 240) / 2, (Screen.height - 100) / 2, 240, 100), WindowContain, "战斗胜利");
    }

    void WindowContain(int windowID) 
    {
        GUI.Label(new Rect(10, 30, 200, 80), "恭喜您得到屠龙刀");
        //GUI.Label(Rect(10, 10, 100, 20), "Hello World!");
    }

    private void attrack(int sp, int dp)
    {
        Fighter fight = fighters[sp];
        if (fight == null)
        {
            inFighing = false;
            return;
        }
        inFighing = true;
        Hashtable args = new Hashtable();
        args.Add("position", attrackPos[dp]);
        args.Add("oncomplete", "moveToEnd");
        args.Add("oncompleteparams", sp);
        args.Add("oncompletetarget", gameObject);
        iTween.MoveTo(fight.gameObject, args);
    }

    private void moveToEnd(int pos)
    {
        Fighter f = fighters[pos];
        effect.Play("dao");
        fighters[curDeffi].subHp(30);
        Hashtable args = new Hashtable();
        args.Add("position", teamPos[pos]);
        args.Add("oncomplete", "moveBackEnd");
        args.Add("oncompleteparams", pos);
        args.Add("oncompletetarget", gameObject);
        args.Add("delay", 0.1);
        iTween.MoveTo(fighters[pos].gameObject, args);
    }

    private void moveBackEnd(int pos)
    {
        inFighing=false;
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
        //Debug.Log(pos + "_" + fighter.ToString());
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

    public List<Fighter> GetMonsterFighters()
    {
        List<Fighter> list = new List<Fighter>();
        for (int i = BattleGlobal.FighterNumberOneSide; i < BattleGlobal.FighterNumberMax; i++)
        {
            if (fighters[i] != null && !fighters[i].isDead)
            {
                list.Add(fighters[i]);
            }
        }
        return list;
    }

    public List<Fighter> GetPlayerFighters()
    {
        List<Fighter> list = new List<Fighter>();
        for (int i = 0; i < BattleGlobal.FighterNumberOneSide; i++)
        {
            if (fighters[i] != null && !fighters[i].isDead)
            {
                list.Add(fighters[i]);
            }
        }
        return list;
    }

    public void DestoryFighter(int index)
    {
        Fighter obj = fighters[index];
        //obj.OnDestory();
        Destroy(obj.gameObject);
        fighters[index] = null;
    }
}
