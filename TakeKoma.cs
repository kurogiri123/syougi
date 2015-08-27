using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text; 
using System.IO; 
using System.Net;
using UnityEngine.UI;
using MiniJSON;
using System;

public class TakeKoma : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EnemyFuCount = 0;
		FuCount = 0;
		EnemyKyoCount = 0;
		KyoCount=0;
		EnemyKeiCount = 0;
		KeiCount = 0;
		EnemyGinCount = 0;
		GinCount = 0;
		EnemyKinCount = 0;
		KinCount = 0;
		EnemyHiCount = 0;
		HiCount = 0;
		EnemyKakuCount =0;
		KakuCount =0;
	}
	// Update is called once per frame
	void Update () {
	}
	public Text EnemyFuText;
	public int EnemyFuCount;
	public Text FuText;
	public int FuCount;
	public Text EnemyKyoText;
	public int EnemyKyoCount;
	public Text KyoText;
	public int KyoCount;
	public Text EnemyKeiText;
	public int EnemyKeiCount;
	public Text KeiText;
	public int KeiCount;
	public Text EnemyGinText;
	public int EnemyGinCount;
	public Text GinText;
	public int GinCount;
	public Text EnemyKinText;
	public int EnemyKinCount;
	public Text KinText;
	public int KinCount;
	public Text EnemyHiText;
	public int EnemyHiCount;
	public Text HiText;
	public int HiCount;
	public Text EnemyKakuText;
	public int EnemyKakuCount;
	public Text KakuText;
	public int KakuCount;





	public void TakeTextCounter(GameObject target){
		ChangeTextCount (target,1, 18, EnemyFuCount, EnemyFuText, FuCount, FuText);
		ChangeTextCount (target,19, 22, EnemyKyoCount, EnemyKyoText, KyoCount, KyoText);
		ChangeTextCount (target,23, 26, EnemyKeiCount, EnemyKeiText, KeiCount, KeiText);
		ChangeTextCount (target, 27, 30, EnemyGinCount, EnemyGinText, GinCount, GinText);
		ChangeTextCount (target, 31, 34, EnemyKinCount, EnemyKinText, KinCount, KinText);
		ChangeTextCount (target, 35, 36, EnemyHiCount, EnemyHiText, HiCount, HiText);
		ChangeTextCount (target, 37, 38, EnemyKakuCount, EnemyKakuText, KakuCount, KakuText);
	}

	public void ChangeTextCount(GameObject target,int StartNum,int EndNum, int EnemyCount, Text EnemyKomaText, int Count, Text KomaText){
		KomaInfo KomaInfoComponent = target.GetComponent<KomaInfo> ();
		if (KomaInfoComponent.MoveId <= EndNum && KomaInfoComponent.MoveId >= StartNum) {
			if (KomaInfoComponent.OwnerId == (int)Login.GetSavedUser ()) {
				EnemyCount = Convert.ToInt32(EnemyKomaText.text);
				EnemyCount += 1;
				EnemyKomaText.text = EnemyCount.ToString();
			//	StateTakedKoma(target, StartNum,EndNum);
			}
			if (KomaInfoComponent.OwnerId != (int)Login.GetSavedUser ()) {
				Count = Convert.ToInt32(KomaText.text);
				Count += 1;
				KomaText.text = Count.ToString();
			//	StateTakedKoma(target, StartNum,EndNum);
			}
		}
	}

	public void InitCount(){
		EnemyFuCount = 0;
		FuCount = 0;
		EnemyKyoCount = 0;
		KyoCount = 0;
		EnemyKeiCount = 0;
		KeiCount = 0;
		EnemyGinCount = 0;
		GinCount = 0;
		EnemyKinCount = 0;
		KinCount = 0;
		EnemyHiCount = 0;
		HiCount = 0;
		EnemyKakuCount = 0;
		KakuCount = 0;
		EnemyFuText.text = "0";
		EnemyKyoText.text = "0";
		EnemyKeiText.text = "0";
		EnemyHiText.text = "0";
		EnemyGinText.text = "0";
		EnemyKinText.text = "0";
		EnemyKakuText.text = "0";
		FuText.text = "0";
		KyoText.text = "0";
		GinText.text = "0";
		KeiText.text = "0";
		KinText.text = "0";
		HiText.text = "0";
		KakuText.text = "0";

	}


	/*public void StateTakedKoma(GameObject target,int StartNum,int EndNum){
		if (StartNum == 1 && EndNum == 18) {//hu
			target.transform.position = new Vector3 (442, (float)320.5, 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
		if (StartNum == 19 && EndNum == 22) {//kyo
			target.transform.position = new Vector3 (442, (float)148.5, 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
		if (StartNum == 23 && EndNum == 26) {//kei
			target.transform.position = new Vector3 (442,(float)191.5 , 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
		if (StartNum == 27 && EndNum == 30) {//gin
			target.transform.position = new Vector3 (442, (float)115.5, 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
		if (StartNum == 31 && EndNum == 34) {//kin
			target.transform.position = new Vector3 (442, (float)72.5, 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
		if (StartNum == 35 && EndNum == 36) {//hi
			target.transform.position = new Vector3 (442,(float) 234.5, 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
		if (StartNum == 37 && EndNum == 38) {//kaku
			target.transform.position = new Vector3 (442, (float)277.5, 0);
			target.transform.Rotate (new Vector3 (0, 0, 180), Space.Self);
		}
	}*/

	public static int[] TakedMyKomaArray = new int[38];
	public static int[] TakedEnemyKomaArray = new int[38];
	public static void InitTakedArray(){
		for (var x = 0; x <=37; x ++) {
			TakedMyKomaArray [x] = 0;
			TakedEnemyKomaArray [x] = 0;
		}
	}


	/*public static int TakeEnemyKoma(int x,int y){
		if (ban.GetKomaIdArray [x, y] != -1) {

		}
	}*/


}
