using UnityEngine;
using System.Collections;
using System;

public class BackgroundManager : BattleBase{

    private float speed = -1.2f;
    private float y = 0;
    public const float maxWalk = 4;
    public float walk = maxWalk;
    public float walkPercent = 0;
    private const float H = 12.8f;

    public GameObject bg1;
    public GameObject bg2;

    private bool moveEnd;

    public override void OnCreateInit()
    {
        BattleData battleData = base.battleData;
        battleData.OnMsgEnter = (Action)Delegate.Combine(battleData.OnMsgEnter, new Action(OnMsgEnter));

        GameObject basP = Resources.Load("Prefabs/backGround") as GameObject;
        Texture2D texture2d = (Texture2D)Resources.Load("bg");
        Sprite sp = Sprite.Create(texture2d, new Rect(0,0,texture2d.width,texture2d.height), new Vector2(0.5f, 0.5f));
        bg1 = (GameObject)Instantiate(basP);
        bg1.transform.parent = gameObject.transform;
        SpriteRenderer spr1 = bg1.GetComponent<SpriteRenderer>();
        spr1.sprite = sp;
        bg2 = (GameObject)Instantiate(basP);
        bg2.transform.parent = gameObject.transform;
        SpriteRenderer spr2 = bg2.GetComponent<SpriteRenderer>();
        spr2.sprite = sp;
        bg2.transform.Translate(0, 8, 0);
        BattleState.GetInstance().state = BattleState.State.walk;
    }

    private void OnMsgEnter()
    {

    }

	void Start () {

	}
	
	void Update () {
        if (BattleState.GetInstance().state == BattleState.State.walk && !moveEnd)
        {
            moveBg();
        }
	}
	private void moveBg(){
        float move = speed * Time.deltaTime;
        walk += move;
        if (walk <= 0)
        {
            move -= walk;
            moveEnd = true;
            walk = maxWalk;
            base.battleData.gameMainObj.SendMessage("OnBackgroundMoveEnd");
        }
        walkPercent = walk / maxWalk;
        y += move;
        if (y <= -H)
        {
            y = 0;
        }
        Vector3 v3 = gameObject.transform.localPosition;
        v3.y = y;
        gameObject.transform.localPosition = v3;
	}
}
