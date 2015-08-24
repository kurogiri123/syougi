﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class KomaInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}

	public int MoveId;
	public int Posx;
	public int Posy;
	public string Promote;
	static int GetId;

	public void UpdatePiece(){
		string url = Post.ipaddr + Post.Port + "/plays/update";
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic ["play_id"] = Login.GetSavedPlay().ToString();
		dic ["user_id"] = Login.GetSavedUser().ToString();
		dic ["move_id"] = MoveId.ToString();
		dic ["posx"] = Posx.ToString();
		dic ["posy"] = Posy.ToString();
		dic ["promote"] = Promote;
		dic ["get_id"] = GetId.ToString();
		//UnityEngine.Events.UnityAction<string> Request = ReceivedUpdatePiece;
		//Post.GetPost().POST (url, dic, Request);
	}

	public void ReceivedUpdatePiece(Dictionary<string, object> ReceivedUpdate, int Id){
		MoveId = Id;
		Posx = Convert.ToInt32(ReceivedUpdate["posx"].ToString());
		Posy = Convert.ToInt32(ReceivedUpdate["posy"].ToString());
		Promote = ReceivedUpdate["promote"].ToString();
		GetId = Convert.ToInt32(ReceivedUpdate["get_id"].ToString());
	}
}
