using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BattleData
{
    public List<FighterData> attActor;//角色信息
    public List<FighterData> defActor;
    public Action OnMsgEnter;
    public void Init()
    {
        attActor = new List<FighterData>();
        defActor = new List<FighterData>();
        //////////////////////////////////////////////////////////////////////////
        //测试数据
        for (int i = 0; i < 3; i++)
        {
            FighterData f1 = new FighterData();
            f1.entity = i;
            f1.maxHp = 100;
            f1.curHp = 100;
            attActor.Add(f1);
        }
        for (int i = 0; i < 3; i++)
        {
            FighterData f1 = new FighterData();
            f1.entity = FighterManager.FighterNumberOneSide + i;
            f1.maxHp = 100;
            f1.curHp = 100;
            defActor.Add(f1);
        }
        //////////////////////////////////////////////////////////////////////////
    }
    public GameObject gameMainObj { get; set; }
}

