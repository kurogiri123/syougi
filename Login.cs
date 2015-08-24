using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class Login : MonoBehaviour {

	static int SavePlay;
	static int SaveUser;
	public InputField ID;
	public InputField ROOM;

	// Use this for initialization
	public void LOGIN()
	{
		string url = Post.ipaddr + Post.Port + "/users/login";
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic ["name"] = ID.text;
		dic ["room_no"] = ROOM.text;
		UnityEngine.Events.UnityAction<string> Request = SaveId;
		Post.GetPost().POST (url, dic, Request);
	}
	public void SaveId(string ReceivedId){
		var json = MiniJSON.Json.Deserialize (ReceivedId) as Dictionary<string, object>;
		SavePlay = Convert.ToInt32(json["play_id"].ToString());//save play_id
		SaveUser = Convert.ToInt32(json["user_id"].ToString());//save user_id
		Application.LoadLevel (1);
	}

	public static int GetSavedPlay(){
		return SavePlay;
	}
	public static int GetSavedUser(){
		return SaveUser;
	}
}
