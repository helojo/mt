using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

    //private static Action<RealTimeBuffer>
    public int PosIndex;//角色位置
    public bool isPlayer;//是否是玩家
    public BattleData battleData;
    public float Scale;
    public FighterData actor;
    public int entry;//实例id
    private Transform avar;
    private Transform hpBar;

    public int MaxHP = 500;
    public int HP = 500;
    public int attack;
    public int deff;

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
        hpBar = gameObject.transform.FindChild("hp");
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

    void OnGUI()
    {
        //GUI.HorizontalScrollbar(rctBloodBar, 1.0f, 1f, 0.0f, 1.0f);
    }

    public void subHp(int n)
    {
        HP -= n;
        if (HP <= 0)
        {
            //gameObject.SetActive(false);
            //return;
        }
        else
        {
            float percent = 1f * HP / MaxHP;
            //scale
            Vector3 v3 = hpBar.transform.localScale;
            v3.x *= percent;
            hpBar.transform.localScale = v3;
            //position
            v3 = hpBar.transform.localPosition;
            v3.x = (percent - 1) / 2;
            hpBar.transform.localPosition = v3;
            //color
            hpBar.renderer.material.color = new Color(1 - percent, percent, 0, 1);
        }
    }
    private Rect rctBloodBar;
	void Start () {

        //rctBloodBar = new Rect(20, 20, 20, 200);
	}

	void Update () {
	
	}
}
