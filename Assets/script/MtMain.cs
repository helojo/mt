using UnityEngine;
using System.Collections;

public class MtMain : MonoBehaviour {

	
    bool isStartGame;

    // Use this for initialization
	void Start () {
        Debug.Log("GameStart");
	}

    void OnGUI()
    {
        if (!isStartGame)
        {
            if (GUI.Button(new Rect((Screen.width-80)/2, (Screen.height-30)/2, 80, 30), "开始战斗"))
            {
                isStartGame = true;
                Debug.Log("[MtMain] 点击开始战斗按钮");
                start();
            }
        }
    }

    void start()
    {
        BattleState.GetInstance().OnEnter();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
