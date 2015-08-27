using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class StateSetting : MonoBehaviour {
	public GameObject hu;
	public GameObject Enemyhu;
	public GameObject kaku;
	public GameObject Enemykaku;
	public GameObject hisya;
	public GameObject Enemyhisya;
	public GameObject kyosya;
	public GameObject Enemykyosya;
	public GameObject keima;
	public GameObject Enemykeima;
	public GameObject gin;
	public GameObject Enemygin;
	public GameObject kin;
	public GameObject Enemykin;
	public GameObject kyoku;
	public GameObject ou;




	int CreatedPiece = 0;

	void Start(){
		StateInformation ();
		TakeKoma.InitTakedArray ();
	}
	float time = 1.0f;
	float StartTextTimer = 120f;
	void Update(){
		time = time - Time.deltaTime;
		if (time <= 0f) {
			state ();
			StateInformation ();
			time = 1.0f;

		}

		if (StateText.text == "GAME START"){
			if( CreatedPiece == 0){
				PlayerInformation ();
			}
		}
		if (StateText.text == "GAME START") {
			StartTextTimer -= 1f;
			if (StartTextTimer == 0){
				StateText.color = new Color (1f, 1f, 1f, 0f);
			}
		}
		if (SavedWinner != null) {
			winner ();
		}
	}
	static string SavedState = "waiting";
	static string SavedWinner = null;
	static string SavedPieces=null;
	static string PieceId;
	public static string SavedPlayer;


	public WWW GET(string url, string StateType)
	{
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (www, StateType));
		return www;
	}

	private IEnumerator WaitForRequest(WWW www, string StateType)
	{
		yield return www;
		// check for errors
		if (www.error == null) {
			var json = MiniJSON.Json.Deserialize (www.text) as Dictionary<string, object>;
			if(StateType == "state"){
				SavedState = json["state"].ToString();
			}
			if(StateType == "winner"){
				SavedWinner = json["winner"].ToString();
				Debug.Log (json["winner"]);
			}
			if(StateType == "users"){
				var last_player = (Dictionary<string,object>) json["last_player"];
				SavedPlayer = last_player["user_id"].ToString();
				PiecesInformation();
				TurnControl.RotateBan();
				UnityEngine.Events.UnityAction Request = TurnControl.RotateBan;
			}
			//----------------------------------------------------------駒配置-------------------------------------------------

			if(StateType == "pieces"){
				for(int i =1;i<=40;i++){
					PieceId = json[i.ToString()].ToString();
					var piece = (Dictionary<string,object>) json[i.ToString()];
					GameObject KomaCheck = null;
					bool CheckOwner = Convert.ToInt32 (piece["owner"]) == Login.GetSavedUser();
					/*if((int)Login.GetSavedUser() == Convert.ToInt32(SavedPlayer)){
						CheckOwner = Convert.ToInt32 (piece["owner"]) != Login.GetSavedUser();
                    }*/
					if(piece["name"].ToString() == "fu"){
						KomaCheck = hu;
						if(CheckOwner == false){
							KomaCheck = Enemyhu;
						}
					}
					if(piece["name"].ToString() == "kyosha"){
						KomaCheck = kyosya;
						if(CheckOwner == false){
							KomaCheck = Enemykyosya;
						}
					}
					if(piece["name"].ToString() == "keima"){
						KomaCheck = keima;
						if(CheckOwner == false){
							KomaCheck = Enemykeima;
						}
					}
					if(piece["name"].ToString() == "gin"){
						KomaCheck = gin;
						if(CheckOwner == false){
							KomaCheck = Enemygin;
						}
					}
					if(piece["name"].ToString() == "kin"){
						KomaCheck = kin;
						if(CheckOwner == false){
							KomaCheck = Enemykin;
						}
					}
					if(piece["name"].ToString() == "hisha"){
						KomaCheck = hisya;
						if(CheckOwner == false){
							KomaCheck = Enemyhisya;
						}
					}
					if(piece["name"].ToString() == "kaku"){
						KomaCheck = kaku;
						if(CheckOwner == false){
							KomaCheck = Enemykaku;
						}
					}
					if(piece["name"].ToString() == "oh"){
						KomaCheck = kyoku;
						if(CheckOwner == false){
							KomaCheck = ou;
						}
					}
					if( CreatedPiece == 0){
						int posx = ban.ServerX(Convert.ToInt32(piece["posx"]));
						int posy = ban.ServerY(Convert.ToInt32(piece["posy"]));
						GameObject Clone = (GameObject)Instantiate (KomaCheck,transform.position,transform.rotation);
						Clone.transform.SetParent (UnityEngine.GameObject.Find("ban_main").transform);
						Clone.transform.localPosition = new Vector3(posx,posy,1);
						//TurnControl.TurnInfomation();
						KomaInfo KomaInfoComponent = Clone.GetComponent<KomaInfo>();
						KomaInfoComponent.ReceivedUpdatePiece(piece,i);
						KomaInfo.KomaArray[i-1] = Clone;
						if((int)Login.GetSavedUser() == Convert.ToInt32(SavedPlayer)){
							Clone.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
						}
					}
				}
				CreatedPiece = 1;
			}
			//----------------------------------------------------------------駒配置-----------------------------------------------
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}


	public static string GetSavedState(){
		return SavedState;
	}
	public static string GetSavedWinner(){
		return SavedWinner;
	}
	public static string GetSavedPieces(){
		return SavedPieces;
	}
	public static string GetSavedPlayer(){
		return SavedPlayer;
	}

	//----------------------------------------------------------状態, 勝負, 駒, プレイヤー 拾得-----------------------------------------
	public void StateInformation(){
		GET (Post.ipaddr + Post.Port +"/plays/" + Login.GetSavedPlay ().ToString () + "/state", "state");
		WinnerInformation();
		TurnControl.TurnInfomation();
	}
	public void WinnerInformation(){
			GET (Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString () + "/winner", "winner");
			Debug.Log ("check win");
	}
	public void PiecesInformation(){
		GET (Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString () + "/pieces", "pieces");
	}
	public void PlayerInformation(){
		GET (Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString () + "/users", "users");
	}
	//----------------------------------------------------------状態, 勝負, 駒, プレイヤー 拾得------------------------------------------
	//ゲームの状態判定
	public Text StateText;
	void state(){
		if (GetSavedState () != "waiting") {
			if(StateText.text != "GAME START"){
				StateText.text = "GAME START";
				TurnControl.TurnInfomation();
			
			}
		}
	}

	//勝負判定
	public Text DefeatText;
	void winner(){
		if (GetSavedWinner () != Login.GetSavedUser ().ToString ()) {
			DefeatText.text = "LOSE";
			DefeatText.color = new Color (1f, 1f, 1f, 1f);
			Debug.Log ("lose");
		}
		if (GetSavedWinner () == Login.GetSavedUser ().ToString ()) {
			DefeatText.color = new Color (1f, 1f, 1f, 1f);
			Debug.Log ("win");
		}
	}
	

}
