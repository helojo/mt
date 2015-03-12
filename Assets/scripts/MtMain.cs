using UnityEngine;
using System.Collections;

public class MtMain : MonoBehaviour {

	// Use this for initialization
    bool isStartGame;

	void Start () {
        Debug.Log("GameStarted");
	}

    void OnGUI()
    {
        if (!isStartGame)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 15, 80, 30), "开始战斗"))
            {
                isStartGame = true;
                Debug.Log("开始战斗了");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
