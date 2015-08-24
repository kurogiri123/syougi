using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EXIT : MonoBehaviour {

	public void Exit()
	{

		string url = Post.ipaddr + Post.Port + "/users/logout";
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic ["play_id"] = Login.GetSavedPlay().ToString();
		dic ["user_id"] = Login.GetSavedUser().ToString();
		UnityEngine.Events.UnityAction<string> Request = LogoutId;
		Post.GetPost().POST (url, dic, Request);

	}
	public void LogoutId(string ReceivedId){
		Application.LoadLevel(0);
	}

}
