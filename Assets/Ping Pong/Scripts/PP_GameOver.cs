using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PP_GameOver : MonoBehaviour {
	public hyper1 h;
	public UIManagerPP uiManager;
	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(uiManager.playerWon){

			text.text = "GAME OVER!\nPLAYER WON!";
			h.Stop_data_log();
		} else if(uiManager.enemyWon){
			text.text = "GAME OVER!\nENEMY WON!";
			h.Stop_data_log();
		}
	}
}
