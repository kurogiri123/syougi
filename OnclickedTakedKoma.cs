using UnityEngine;
using System.Collections;

public class OnclickedTakedKoma : MonoBehaviour {
	//持ち駒の処理(実装されてない)


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	Vector3 MasuPosition;
	public GameObject Masu;



	public void OnClickedTakedKoma(){
		//TakeKoma TakeKomaComponent = GameObject.Fine ("ban_main").GetComponent<TakeKoma> ();
		//if( TakeKomaComponent. ==
		CreateMasu ();
	}



	bool CreateMasu(){
		//ban banComponent = GameObject.Find ("ban_main").GetComponent<ban> ();
		for (int x =1; x<=9; x++) {
			for (int y=1; y<=9; y++) {
				if (ban.GetKomaIdArray (x,y) == -1) {
					GameObject MasuClone = (GameObject)Instantiate (Masu, transform.localPosition, transform.rotation);
					MasuClone.transform.SetParent (UnityEngine.GameObject.Find ("ban_main").transform);
					x = ban.ServerX (x);
					y = ban.ServerY (y);
					MasuClone.transform.localPosition = new Vector3 (x, y, 0);
					return true;
				}else{
					//return false;
				}
			}

		}
		return false;
	}






}
