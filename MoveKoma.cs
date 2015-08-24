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
	}
	//-----------------------------------------------マスを生成します-----------------------------------------------
	bool CreateMasu(){
		int posx = ban.LocalX ((int)MasuPosition.x);
		int posy = ban.LocalY ((int)MasuPosition.y);
		if (CheckPosition (posx, posy) == true) {
			GameObject MasuClone = (GameObject)Instantiate (Masu, transform.localPosition, transform.rotation);
			MasuClone.transform.SetParent (UnityEngine.Object.FindObjectOfType<Canvas> ().transform);
			posx = ban.ServerX(posx);
			posy = ban.ServerY(posy);
			MasuClone.transform.localPosition = new Vector3(posx,posy,0);
			Debug.Log("create masu");
			return true;
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
		MasuPosition = transform.localPosition + new Vector3 (0, 43, 0);
		CreateMasu ();
	}
	public void OnClickedKaku(){
		target = this.gameObject;
		DestroyMasu ();
		for (int i = 1; i<=10; i++) {
			MasuPosition = transform.localPosition + new Vector3 (43, 43, 0)*i;
			CreateMasu ();
			MasuPosition = transform.localPosition + new Vector3 (-43, -43, 0)*i;
			CreateMasu ();
			MasuPosition = transform.localPosition + new Vector3 (-43, 43, 0)*i;
			CreateMasu ();
			MasuPosition = transform.localPosition + new Vector3 (43, -43, 0)*i;
			CreateMasu ();
		}
	}
	public void OnClickedKyosya(){
		target = this.gameObject;
		DestroyMasu ();
		for (int i =1; i<=10; i++) {
			MasuPosition = transform.localPosition + new Vector3 (0, 43, 0) * i;
			CreateMasu ();
		}
	}

	public void moveClicked(){
		Move = transform.position;
		target.transform.position = Move;
		DestroyMasu ();
	}
	//----------------------------------------------------------味方-----------------------------------------------
	//----------------------------------------------------------敵---------------------------------------------
	public void EnemyOnClickedHu() {
		target = this.gameObject;
		DestroyMasu ();
		MasuPosition = transform.localPosition + new Vector3 (0, -43, 0);
		CreateMasu ();
	}
	public void EnemyOnClickedKaku(){
		target = this.gameObject;
		DestroyMasu ();
		for (int i = 1; i<=10; i++) {
			MasuPosition = transform.localPosition + new Vector3 (43, 43, 0)*i;
			CreateMasu ();
			MasuPosition = transform.localPosition + new Vector3 (-43, -43, 0)*i;
			CreateMasu ();
			MasuPosition = transform.localPosition + new Vector3 (-43, 43, 0)*i;
			CreateMasu ();
			MasuPosition = transform.localPosition + new Vector3 (43, -43, 0)*i;
			CreateMasu ();
		}
	}
	public void EnemyOnClickedKyosya(){
		target = this.gameObject;
		DestroyMasu ();
		for (int i =1; i<=10; i++) {
			MasuPosition = transform.localPosition + new Vector3 (0, -43, 0) * i;
			CreateMasu ();
		}
	}

	public void EnemyMasuClicked(){
		DestroyMasu ();
	}
	//-------------------------------------------------------敵------------------------------------------------

	//-----------------------------------------------------駒の移動制限------------------------------------------------
	public void PiecesInformation()
	{
		string url = Post.ipaddr + Post.Port + "/plays/" + Login.GetSavedPlay ().ToString () +"/pieces";
		UnityEngine.Events.UnityAction<string> Request = CheckOwner;
		Post.GetPost ().GET (url, Request);
	}

	public static void CheckOwner(string json){
		for (int i =1; i<=40; i++) {
			PieceName = json [i].ToString ();
			var piece = (Dictionary<string,object>)json [i];
			bool CheckOwner = Convert.ToInt32 (piece ["owner"]) == Login.GetSavedUser ();
			int posx = Convert.ToInt32(piece["posx"]);
			int posy = Convert.ToInt32(piece["posy"]);
			ban.SetKomaIdArray(posx,posy,i);
		}
	}
}