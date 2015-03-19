using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

    public int PosIndex;
    public bool isPlayer;
    public BattleData battleData;
    public FighterData actor;
    public int entity;//实例id
    private Transform avar;
    private Transform hpBar;

    public int MaxHP = 10;
    public int HP = 10;
    public int attack;
    public int deff;

    public bool isDead;

    public void Init(BattleData _data, bool _isPlayer, int _entity, FighterData _actor)
    {
        battleData = _data;
        isPlayer = _isPlayer;
        MaxHP = _actor.maxHp;
        HP = _actor.curHp;
        actor = _actor;
        entity = _entity;
        hpBar = gameObject.transform.FindChild("hp");
        avar = gameObject.transform.FindChild("avar");
        SpriteRenderer spr = avar.GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("hero/" + _entity);
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        spr.sprite = sp;
    }

    public void subHp(int n)
    {
        HP -= n;
        if (HP <= 0)
        {
            OnDead();
            return;
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


	void Start () {

	}

	void Update () {
	
	}

    private void OnDead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
