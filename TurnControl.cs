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
	}
	
	// Update is called once per frame
	void Update () {
	}

	static int TurnCount;
	static int WatcherCount;
	static int TurnPlayer;
	static string State;

	public void TurnInfomation(Dictionary<string, object> ReceivedTurn){
		//Post.GET (Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString ());
		TurnCount= Convert.ToInt32(ReceivedTurn["turn_count"].ToString());
		WatcherCount = Convert.ToInt32(ReceivedTurn["watcher_count"].ToString());
		TurnPlayer = Convert.ToInt32(ReceivedTurn ["turn_player"].ToString());
		State = ReceivedTurn ["state"].ToString ();
	}




}
