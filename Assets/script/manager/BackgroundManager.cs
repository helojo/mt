using UnityEngine;
using System.Collections;
using System;

public class BackgroundManager : BattleBase{
    //public GameObject objBg1 = null;
    //public GameObject objBg2 = null;
    //private Texture txr = null;

    //public static State state = State.walk;

    //public enum State{
    //    walk, appear, fight, win, fail
    //}
    //private float speed = -1.2f;
    //private float y = 0;//gameObj.y
    //public const float maxWalk = 4;
    //public float walk = maxWalk;
    //public float walkPercent = 0;
    //private const float H = 12.8f;//objBg1.h

    public GameObject bg1;
    public GameObject bg2;

    public override void OnCreateInit()
    {
        BattleData battleData = base.battleData;
        battleData.OnMsgEnter = (Action)Delegate.Combine(battleData.OnMsgEnter, new Action(OnMsgEnter));

        GameObject basP = Resources.Load("Prefabs/backGround") as GameObject;
        Texture2D texture2d = (Texture2D)Resources.Load("bg");
        Sprite sp = Sprite.Create(texture2d, new Rect(0,0,texture2d.width,texture2d.height), new Vector2(0.5f, 0.5f));
        bg1 = (GameObject)Instantiate(basP);
        SpriteRenderer spr1 = bg1.GetComponent<SpriteRenderer>();
        spr1.sprite = sp;
        bg2 = (GameObject)Instantiate(basP);
        SpriteRenderer spr2 = bg2.GetComponent<SpriteRenderer>();
        spr2.sprite = sp;
        bg2.transform.Translate(0, 8, 0);
    }

    private void OnMsgEnter()
    {

    }

	void Start () {
        //txr = FighterHelper.GetTextureById("1", FightConfig.BG_PATH);
        //state = State.walk;
	}
	
	void Update () {
        //objBg1.renderer.material.mainTexture = txr;
        //objBg2.renderer.material.mainTexture = txr;
        //switch (state) {
        //case State.walk:
        //    moveBg();
        //    break;
        //}
	}
	private void moveBg(){
        //float move = speed * Time.deltaTime;
        //walk += move;
        //if(walk<=0){
        //    move -= walk;
        //    state = State.fight;
        //    walk = maxWalk;
        //}
        //walkPercent = walk / maxWalk;
        //y += move;
        //if(y<=-H){
        //    y = 0;
        //}
        //Vector3 v3 = gameObject.transform.localPosition;
        //v3.y = y;
        //gameObject.transform.localPosition = v3;
	}
}
