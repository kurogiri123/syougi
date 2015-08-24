using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EXIT : MonoBehaviour {

	public Text DefeatText;
	public void Exit()
	{
		string url = Post.ipaddr + Post.Port + "/users/logout";
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic ["play_id"] = Login.GetSavedPlay().ToString();
		dic ["user_id"] = Login.GetSavedUser().ToString();
		UnityEngine.Events.UnityAction<string> Request = LogoutId;
		Post.GetPost().POST (url, dic, Request);
		DefeatText.color = new Color (1f, 1f, 1f, 0f);

	}
	public void LogoutId(string ReceivedId){
		Application.LoadLevel(0);
	}

}
