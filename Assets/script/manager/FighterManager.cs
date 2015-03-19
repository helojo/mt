using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FighterManager : BattleBase {

    public List<Vector3> teamPos = new List<Vector3>();//位置列表
    public List<Vector3> attrackPos = new List<Vector3>();//攻击站位列表

    private List<Fighter> fighters { get; set; }//战斗对象
    public GameObject playerTeamObj { get; private set; }

    private GameObject fighterCard;
    private GameObject fighterEffect;
    private Animator effect;

    private float timer = 0f;
    public const float attackDistance = 1f;

    private bool inFighing;

    private int curFighter = 0;
    private int curDeffi;

    public static readonly int FighterNumberMax = 12;
    public static readonly int FighterNumberOneSide = 6;
    /// <summary>
    /// 战斗管理
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

        fighters = new List<Fighter>(new Fighter[FighterNumberMax]);
        fighterCard = (GameObject)Resources.Load("Prefabs/hero");
        fighterEffect = (GameObject)Resources.Load("Prefabs/fightEffect");
    }

    public override void OnCreateInit()
    {
        BattleData battleData = base.battleData;
        battleData.OnMsgEnter = (Action)Delegate.Combine(battleData.OnMsgEnter, new Action(OnMsgEnter));
    }

    public IEnumerator OnBackgroundMoveEnd()
    {
        List<Fighter> monsters = GetMonsterFighters();
        foreach (Fighter f in monsters)
        {
            iTween.FadeTo(f.gameObject, 1, 1);
        }
        yield return new WaitForSeconds(3.0f);
        BattleState.GetInstance().state = BattleState.State.fight;
    }

    private void OnMsgEnter()
    {
        GameObject obj = (GameObject)Instantiate(fighterEffect);
        effect = obj.GetComponent<Animator>();
        InitFightersFormBattleDate();
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
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (BattleState.GetInstance().state == BattleState.State.fight)
        {
            if (!inFighing)
            {
                //Debug.Log(GetMonsterFighters().Count);
                //Debug.Log(GetPlayerFighters().Count);

                if (GetMonsterFighters().Count <= 0)
                {
                    BattleState.GetInstance().state = BattleState.State.win;
                    return;
                }
                if (GetPlayerFighters().Count <= 0)
                {
                    BattleState.GetInstance().state = BattleState.State.fail;
                    return;
                }

                if (curFighter == FighterNumberMax - 1)
                    curFighter = 0;
                if (curFighter < FighterNumberOneSide)
                    curDeffi = FighterNumberOneSide + curFighter;
                else
                    curDeffi = curFighter - FighterNumberOneSide;
                while (fighters[curDeffi] == null || fighters[curDeffi].isDead)
                {
                    curDeffi++;
                    if (curDeffi == FighterNumberMax)
                        curDeffi = 0;
                }
                Debug.Log("attackter：" + curFighter);
                attrack(curFighter, curDeffi);
                curFighter++;
            }
        }
    }

    void OnGUI()
    {
        if (BattleState.GetInstance().state == BattleState.State.win)
            GUI.Window(0, new Rect((Screen.width - 240) / 2, (Screen.height - 100) / 2, 240, 100), WinContain, "战斗胜利");
        if (BattleState.GetInstance().state == BattleState.State.fail)
            GUI.Window(0, new Rect((Screen.width - 240) / 2, (Screen.height - 100) / 2, 240, 100), FailContain, "战斗失败");
    }

    private void WinContain(int windowID) 
    {
        GUI.Label(new Rect(10, 30, 200, 80), "恭喜您得到屠龙刀");
    }

    private void FailContain(int windowID)
    {
        GUI.Label(new Rect(10, 30, 200, 80), "请从新挑战");
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
        effect.transform.position = attrackPos[curDeffi];
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
        if ((actor != null) && (actor.entity != -1))
        {
            createFighter(actor.entity, posIndex, 1f, actor);
        }
    }

    public GameObject createFighter(int entry, int pos, float scale, FighterData actor)
    {
        if (entry < 0)
        {
            return null;
        }
        bool flag = pos < FighterNumberOneSide;
        GameObject obj2 = (GameObject)Instantiate(fighterCard);
        obj2.name = "fighter " + pos.ToString();
        Fighter fighter = obj2.AddComponent<Fighter>();
        fighter.PosIndex = pos;
        fighter.Init(base.battleData, flag, entry, actor);
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
            PlaceFighter(this.fighters[index], index);
        }
    }

    /// <summary>
    /// 根据posIndex设置角色站位
    /// </summary>
    /// <param name="fighter"></param>
    /// <param name="posIndex"></param>
    /// <param name="isPlayer"></param>
    private void PlaceFighter(Fighter fighter, int posIndex)
    {
        Vector3 zero = Vector3.zero;
        Quaternion identity = Quaternion.identity;
        zero = teamPos[posIndex];
        if (posIndex >= FighterNumberOneSide)
        {
            //fighter.gameObject.SetActive(false);
            iTween.FadeTo(fighter.gameObject, 0, 0);
        }
        fighter.transform.Translate(zero);
        fighter.transform.rotation = identity;
        
    }

    /// <summary>
    /// 敌方阵营角色列表
    /// </summary>
    /// <returns></returns>
    public List<Fighter> GetMonsterFighters()
    {
        List<Fighter> list = new List<Fighter>();
        for (int i = FighterNumberOneSide; i < FighterNumberMax; i++)
        {
            if (fighters[i] != null && !fighters[i].isDead)
            {
                list.Add(fighters[i]);
            }
        }
        return list;
    }

    /// <summary>
    /// 友方阵营角色列表
    /// </summary>
    /// <returns></returns>
    public List<Fighter> GetPlayerFighters()
    {
        List<Fighter> list = new List<Fighter>();
        for (int i = 0; i < FighterNumberOneSide; i++)
        {
            if (fighters[i] != null && !fighters[i].isDead)
            {
                list.Add(fighters[i]);
            }
        }
        return list;
    }

    /// <summary>
    ///  移除战斗角色
    /// </summary>
    /// <param name="index"></param>
    public void DestoryFighter(int index)
    {
        Fighter obj = fighters[index];
        Destroy(obj.gameObject);
        fighters[index] = null;
    }
}
