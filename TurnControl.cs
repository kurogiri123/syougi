using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class TurnControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//TurnText = this.gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		TurnText.text = "ターン：" + TurnCountText();
	}

	static int TurnCount=0;
	static int WatcherCount;
	public static int TurnPlayer;
	//static string State;
	public Text TurnText;
	public static int BanState = 0;
	public Text WhosTurn;
	


	public static void TurnInfomation(){
		string url = Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString ();
		UnityEngine.Events.UnityAction<string> Request = SaveTurn;
		Post.GetPost().GET(url,Request);
		MoveKoma.PiecesInformation ();
		TurnControl TurncontrolComponent = GameObject.Find ("TurnText").GetComponent<TurnControl> ();
		TurncontrolComponent.ChangeWhosTurn ();
		//MoveKoma MoveKomaComponent = GameObject.Find ("state").GetComponent<MoveKoma> ();
	//	MoveKomaComponent.DestroyMasu ();
	}

	public static void SaveTurn(string ReceivedTurn){
		var json = MiniJSON.Json.Deserialize (ReceivedTurn) as Dictionary<string, object>;
		TurnCount = Convert.ToInt32 (json ["turn_count"].ToString ());
		WatcherCount = Convert.ToInt32 (json ["watcher_count"].ToString ());
		TurnPlayer = Convert.ToInt32 (json ["turn_player"].ToString ());
		//State = json ["state"].ToString ();
		//WaitForYourTurn ();
		Debug.Log (TurnCount);
		Debug.Log (TurnPlayer);
	}

	public static int TurnCountText(){
		return TurnCount;
	}
	public static void RotateBan(){
		//UnityEngine.Events.UnityAction<string> Request = Save;
		if ((int)Login.GetSavedUser () == Convert.ToInt32 (StateSetting.SavedPlayer)) {
			if(BanState ==0){
				GameObject.Find ("ban_main").transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
				Debug.Log ("rotate ban");
				BanState =1;
			}
		}
	}
	public void ChangeWhosTurn(){
		if (TurnPlayer == Login.GetSavedUser ()) {
			WhosTurn.text = "あなたのターンです";
		}
		else if(TurnPlayer != Login.GetSavedUser ()) {
			WhosTurn.text = "相手のターンです";
		} else {
			WhosTurn.text = "";
		}
	}









}
