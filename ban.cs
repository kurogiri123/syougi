using UnityEngine;
using System.Collections;

public class ban : MonoBehaviour {

	private static Vector3 LocalStandardPosition = new Vector3(49,183,0);//盤の1*1のglobal position
	private static Vector3 ServerStandardPosition = new Vector3 (1, 1, 0);//serverのglobal position
//--------------------------------------------serverとglobal positionの座標変換-------------------------------
	public static int LocalX(int PLX){//PLX = Pieces Local X
		int Lx = (int)LocalStandardPosition.x;
		Lx = Lx - PLX;
		Lx = (Lx / 43) + 1;
		return Lx;//Lxはサーバーのx値になっている
	}

	public static int LocalY(int PLY){//PLY = Pieces Local Y
		int Ly = (int)LocalStandardPosition.y;
		Ly = Ly - PLY;
		Ly = (Ly / 43) + 1;
		return Ly;
	}

	public static int ServerX(int PSX){//PSX = Pieces Server X
		int Sx = (int)ServerStandardPosition.x;
		Sx = Sx - PSX;
		Sx = (Sx * 43) + (int)LocalStandardPosition.x;
		return Sx;
	}

	public static int ServerY(int PSY){//PSY = Pieces Server Y
		int Sy = (int)ServerStandardPosition.y;
		Sy = Sy - PSY;
		Sy = (Sy * 43) + (int)LocalStandardPosition.y;
		return Sy;
	}
//--------------------------------------------serverとglobal positionの座標変換-------------------------------
//盤の9*9の配列
	public static int[,] KomaIdArray = new int [9,9];

	public static void SetKomaIdArray (int posx, int posy, int Id){
		KomaIdArray [posx - 1] [posy - 1] = Id;
	}
}

