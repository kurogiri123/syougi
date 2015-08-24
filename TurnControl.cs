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
		TurnText = this.gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	static int TurnCount;
	static int WatcherCount;
	static int TurnPlayer;
	static string State;
	Text TurnText;

	public static void TurnInfomation(){
		string url = Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString ();
		UnityEngine.Events.UnityAction<string> Request = SaveTurn;
		Post.GetPost().GET(url,Request);
	}

	public static void SaveTurn(string ReceivedTurn){
		var json = MiniJSON.Json.Deserialize (ReceivedTurn) as Dictionary<string, object>;
		TurnCount = Convert.ToInt32 (json ["turn_count"].ToString ());
		WatcherCount = Convert.ToInt32 (json ["watcher_count"].ToString ());
		TurnPlayer = Convert.ToInt32 (json ["turn_player"].ToString ());
		State = json ["state"].ToString ();
	}

	//public void ChangeTurnText(){
	//	TurnText = 






}
