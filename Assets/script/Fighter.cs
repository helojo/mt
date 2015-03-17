using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

    //private static Action<RealTimeBuffer>
    public int PosIndex;//角色位置

    public bool isPlayer;//是否是玩家

    public BattleData battleData;
    public int MaxHP;
    public int HP;
    public float Scale;
    public FighterData actor;
    public int entry;//实例id

    public void Init(BattleData _data, float _scale, bool inNum, int _entry, FighterData _actor)
    {
        battleData = _data;
        //CardEntry = _cardEntry;
        //isPlayer = _isPlayer;
        //MaxHP = actor.maxHp;
        //HP = actor.curHp;
        //Energy = _detailActor.energy;
        //Scale = _scale;
        //actor = _actor;
        //entry = _entry;
        //isBegin = false;
        //obj = (MonoBehaviour.Instantiate(hero, Vector3.zero, Quaternion.identity) as GameObject);
        //obj = (GameObject)Resources.Load("Prefabs/hero");
        //Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
        //obj.transform.parent = this.transform;  
        //Debug.Log(obj);
    }

	void Start () {
	}

	void Update () {
	
	}
}
