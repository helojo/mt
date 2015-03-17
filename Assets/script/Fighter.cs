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
    private Transform avar;
    public void Init(BattleData _data, float _scale, bool _isPlayer, int _entry, FighterData _actor)
    {
        battleData = _data;
        //CardEntry = _cardEntry;
        isPlayer = _isPlayer;
        //MaxHP = actor.maxHp;
        //HP = actor.curHp;
        //Scale = _scale;
        //actor = _actor;
        //entry = _entry;
        //isBegin = false;
        //Debug.Log(obj);
        //Texture txr = (Texture)Resources.Load("hero/hero1");

        avar = gameObject.transform.FindChild("avar");
        SpriteRenderer spr = avar.GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("hero/" + _entry);
        //Debug.Log("hero/" + _entry);
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        spr.sprite = sp;
        //avar = tx;
        //avar.renderer.material.mainTexture = txr;
        //avar.renderer.merial.mainTexture = txr;
    }

	void Start () {
	}

	void Update () {
	
	}
}
