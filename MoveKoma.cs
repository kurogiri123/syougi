using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class MoveKoma : MonoBehaviour {


	private static GameObject target;
	public GameObject Masu;
	private MoveKoma Control;
	private Vector3 MasuPosition;
	private Vector3 Move;


	static string PieceName;

	bool CheckPosition(int posx,int posy){
		return (posx <= 9 && posx>=1 && posy <=9 && posy >=1);
	}

	void Start () {
		ban.InitKomaIdArray ();
		PiecesInformation ();
	}
	//-----------------------------------------------マスを生成します-----------------------------------------------
	bool CreateMasu(){
		int posx = ban.LocalX ((int)MasuPosition.x);
		int posy = ban.LocalY ((int)MasuPosition.y);
		if (CheckPosition (posx, posy) == true) {
			if(ban.GetKomaIdArray(posx, posy) == -1){ 
				GameObject MasuClone = (GameObject)Instantiate (Masu, transform.localPosition, transform.rotation);
				MasuClone.transform.SetParent (UnityEngine.Object.FindObjectOfType<Canvas> ().transform);
				posx = ban.ServerX(posx);
				posy = ban.ServerY(posy);
				MasuClone.transform.localPosition = new Vector3(posx,posy,0);
				Debug.Log("create masu");
				return true;
			}else {
				return false;
			}
		} else {
			Debug.Log("create fail");
			return false;
		}
	}
	//--------------------------------------------マスを生成します------------------------------------------------
	//--------------------------------------------生成されたマスを消去します----------------------------------------
	void DestroyMasu(){
		GameObject[] ObjectMasu = GameObject.FindGameObjectsWithTag ("masu");
		foreach (GameObject masu in ObjectMasu) {
			Destroy (masu);
		}

	}
	//--------------------------------------------生成されたマスを消去します----------------------------------------

	//駒の動きとマスの配置
	//-----------------------------------------------------------味方---------------------------------------------
	public void OnClickedHu() {
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, 1,1);
	}
	public void OnClickedKaku(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (1, 1,10);
		MasuPositionControl (-1, -1,10);
		MasuPositionControl (-1, 1,10);
		MasuPositionControl (1, -1,10);
	}
	public void OnClickedKyosya(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, 1,10);
	}
	public void OnClickedHisya(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, 1, 10);
		MasuPositionControl (1, 0, 10);
		MasuPositionControl (-1, 0, 10);
		MasuPositionControl (0, -1, 10);
	}
	public void OnClickedOh(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, 1, 1);
		MasuPositionControl (1, 0, 1);
		MasuPositionControl (-1, 0, 1);
		MasuPositionControl (0, -1, 1);
		MasuPositionControl (1, -1, 1);
		MasuPositionControl (-1, -1, 1);
		MasuPositionControl (1, 1, 1);
		MasuPositionControl (-1, 1, 1);
	}
	public void OnClickedKin(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, 1, 1);
		MasuPositionControl (1, 0, 1);
		MasuPositionControl (-1, 0, 1);
		MasuPositionControl (0, -1, 1);
		MasuPositionControl (1, 1, 1);
		MasuPositionControl (-1, 1, 1);
	}
	public void OnClickedGin(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, 1, 1);
		MasuPositionControl (1, -1, 1);
		MasuPositionControl (-1, -1, 1);
		MasuPositionControl (1, 1, 1);
		MasuPositionControl (-1, 1, 1);
	}
	public void OnClickedKei(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (1, 2, 1);
		MasuPositionControl (-1, 2, 1);
	}

	public void moveClicked(){
		Move = transform.position;
		target.transform.position = Move;
		DestroyMasu ();
		PiecesInformation ();
		KomaInfo.UpdatePiece ();
	}
	//----------------------------------------------------------味方-----------------------------------------------
	//----------------------------------------------------------敵---------------------------------------------
	public void EnemyOnClickedHu() {
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, -1,1);
	}
	/*public void EnemyOnClickedKaku(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (1, 1,10);
		MasuPositionControl (-1, -1,10);
		MasuPositionControl (-1, 1,10);
		MasuPositionControl (1, -1, 10);
	}*/
	public void EnemyOnClickedKyosya(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, -1,10);
	}
	public void EnemyOnClickedKin(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, -1, 1);
		MasuPositionControl (1, 0, 1);
		MasuPositionControl (-1, 0, 1);
		MasuPositionControl (0, 1, 1);
		MasuPositionControl (1, -1, 1);
		MasuPositionControl (-1, -1, 1);
	}
	public void EnemyOnClickedGin(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (0, -1, 1);
		MasuPositionControl (1, -1, 1);
		MasuPositionControl (-1, -1, 1);
		MasuPositionControl (1, 1, 1);
		MasuPositionControl (-1, 1, 1);
	}
	public void EnemyOnClickedKei(){
		target = this.gameObject;
		DestroyMasu ();
		MasuPositionControl (1, -2, 1);
		MasuPositionControl (-1, -2, 1);
	}

	public void EnemyMasuClicked(){
		DestroyMasu ();
	}
	//-------------------------------------------------------敵-------------------------------------------------------
	//-------------------------------------------------------マスの生成場所制御----------------------------------------------
	public void MasuPositionControl(int x,int y, int c){//x, y に0 , 1 , -1　を入れることによって,マスの生成位置を制御できる ,i は繰り返す回数
		for (int i=1; i<=c; i++) {
			MasuPosition = transform.localPosition + new Vector3 (43*x, 43*y, 0) * i;
			if (CreateMasu() == false) {
				break;
			}
		}
	}

	//-----------------------------------------------------駒の移動制限------------------------------------------------
	public void PiecesInformation()
	{
		string url = Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString () +"/pieces";
		UnityEngine.Events.UnityAction<string> Request = CheckIdInfo;
		Post.GetPost ().GET (url, Request);
	}

	public static void CheckIdInfo(string ReceivedPieceName){
		var json = MiniJSON.Json.Deserialize (ReceivedPieceName) as Dictionary<string, object>;
		ban.InitKomaIdArray ();
		for (int i =1; i<=40; i++) {
			PieceName = json [i.ToString()].ToString();
			var piece = (Dictionary<string,object>)json [i.ToString()];
			bool CheckOwner = Convert.ToInt32 (piece ["owner"]) == Login.GetSavedUser ();
			int posx = Convert.ToInt32(piece["posx"]);
			int posy = Convert.ToInt32(piece["posy"]);
			ban.SetKomaIdArray(posx,posy,i);
		}
	}
	//---------------------------------------------------駒の動いた情報を送る----------------------------------------------
	/*public void UpdatePiece(){
		string url = Post.ipaddr + Post.Port + "/plays/update";
		Dictionary<string,string> dic = new Dictionary<string, string> ();
		dic ["play_id"] = Login.GetSavedPlay();
		dic ["user_id"] = Login.GetSavedUser();
		dic["move_id"] = 
		UnityEngine.Events.UnityAction<string> Request = SaveId;
		Post.GetPost ().POST (url, dic, Request);
	}*/

	//if (bool CheckKeima = Convert.ToInt32 (piece ["name"]) == "keima"){
}