using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class Post : MonoBehaviour {
	
	public static Post CallPost;

	public static string ipaddr = "http://192.168.3.83";
	public static string Port = ":3000";

	public WWW POST(string url, Dictionary<string,string> post, UnityEngine.Events.UnityAction<string> Request)
	{
		WWWForm form = new WWWForm();
		foreach(KeyValuePair<string,string> post_arg in post)
		{
			form.AddField(post_arg.Key, post_arg.Value);
		}
		WWW www = new WWW(url, form);
		
		StartCoroutine(WaitForRequest(Request ,www));
		return www; 
	}
	public WWW GET(string url, UnityEngine.Events.UnityAction<string> Request)
	{
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (Request,www));
		return www;
	}


	private IEnumerator WaitForRequest(UnityEngine.Events.UnityAction<string> Request, WWW www) {
		yield return www;
		// check for errors
		if (www.error == null) {
			Request (www.text);
			Debug.Log ("WWW Ok!: " + www.text);
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}

	void Start(){
		DontDestroyOnLoad (transform.gameObject);
		DontDestroyOnLoad (this);
		CallPost = this;
	}

	public static Post GetPost(){
		return CallPost;
	}

	//for use next scene

	/*public static string MainUrl(){
		Dictionary<URL,string> data = new Dictionary<URL,string>();
		data ["MainUrl"] = ipaddr;
		return MainUrl;
	}*/
}
