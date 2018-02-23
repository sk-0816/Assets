using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersinfo : MonoBehaviour {
	//キャラクターの情報、仮のDB
	//時間があったらやる


//キャラクターの名前
	public string[] YGname;
	public string[] HTname;
	public string[] Cname;
	public string[] SHname;
	public string[] Lname;
	public string[] Fname;

//キャラクターの能力(野手)
	public int[][] YGabillity= new int[9][];
	public int[][] HTabillity= new int[9][];
	public int[][] Cabillity= new int[9][];
	public int[][] SHabillity= new int[9][];
	public int[][] Labillity= new int[9][];
	public int[][] Fabillity= new int[9][];

//キャラクターの能力(投手)
	public int[][] YGpitcher= new int[3][];
	public int[][] HTpitcher= new int[3][];
	public int[][] Cpitcher= new int[3][];
	public int[][] SHpitcher= new int[3][];
	public int[][] Lpitcher= new int[3][];
	public int[][] Fpitcher= new int[3][];

	public
	// Use this for initialization
	void Start () {

		YGname =  new string [9]{"あ","あ","あ","あ","あ","あ","あ","あ","あ"};
		HTname =  new string [9]{"あ","あ","あ","あ","あ","あ","あ","あ","あ"};
		Cname =  new string [9]{"あ","あ","あ","あ","あ","あ","あ","あ","あ"};
		SHname =  new string [9]{"あ","あ","あ","あ","あ","あ","あ","あ","あ"};
		Lname =  new string [9]{"あ","あ","あ","あ","あ","あ","あ","あ","あ"};
		Fname =  new string [9]{"あ","あ","あ","あ","あ","あ","あ","あ","あ"};


		//野手
		//meet , power , speed , deffence , arm , position , batting number
/*
meet ミート 1~10
power 飛ばす力　1~10
speed 足の速さ 1~10
defence　守備　1~10
arm　肩の強さ　1~10
position 守備位置 1=投手,2=捕手,3=一塁手,4=二塁手,5=三塁手,6=遊撃手,7=左翼手,8=中堅手,9=右翼手
batting number　打順
 */
		YGabillity[0] = new int[7]{7, 6, 8, 9, 8, 8, 1};
		YGabillity[1] = new int[7]{6, 3, 9, 8, 4, 4, 2};
		YGabillity[2] = new int[7]{9, 6, 7, 9, 7, 6, 3};
		YGabillity[3] = new int[7]{5, 10, 4, 6, 7, 7, 4};
		YGabillity[4] = new int[7]{7, 7, 2, 6, 5, 3, 5};
		YGabillity[5] = new int[7]{9, 5, 4, 5, 7, 5, 6};
		YGabillity[6] = new int[7]{7, 6, 7, 8, 7, 9, 7};
		YGabillity[7] = new int[7]{2, 2, 4, 7, 10, 2, 8};
		YGabillity[8] = new int[7]{1, 1, 5, 8, 7, 1, 9};


		HTabillity[0] = new int[7]{7, 6, 8, 9, 8, 8, 1};
		HTabillity[1] = new int[7]{6, 3, 9, 8, 4, 4, 2};
		HTabillity[2] = new int[7]{9, 6, 7, 9, 7, 6, 3};
		HTabillity[3] = new int[7]{5, 10, 4, 6, 7, 7, 4};
		HTabillity[4] = new int[7]{7, 7, 2, 6, 5, 3, 5};
		HTabillity[5] = new int[7]{9, 5, 4, 5, 7, 5, 6};
		HTabillity[6] = new int[7]{7, 6, 7, 8, 7, 9, 7};
		HTabillity[7] = new int[7]{2, 2, 4, 7, 10, 2, 8};
		HTabillity[8] = new int[7]{1, 1, 5, 8, 7, 1, 9};


		Cabillity[0] = new int[7]{7, 6, 8, 9, 8, 8, 1};
		Cabillity[1] = new int[7]{6, 3, 9, 8, 4, 4, 2};
		Cabillity[2] = new int[7]{9, 6, 7, 9, 7, 6, 3};
		Cabillity[3] = new int[7]{5, 10, 4, 6, 7, 7, 4};
		Cabillity[4] = new int[7]{7, 7, 2, 6, 5, 3, 5};
		Cabillity[5] = new int[7]{9, 5, 4, 5, 7, 5, 6};
		Cabillity[6] = new int[7]{7, 6, 7, 8, 7, 9, 7};
		Cabillity[7] = new int[7]{2, 2, 4, 7, 10, 2, 8};
		Cabillity[8] = new int[7]{1, 1, 5, 8, 7, 1, 9};


		SHabillity[0] = new int[7]{7, 6, 8, 9, 8, 8, 1};
		SHabillity[1] = new int[7]{6, 3, 9, 8, 4, 4, 2};
		SHabillity[2] = new int[7]{9, 6, 7, 9, 7, 6, 3};
		SHabillity[3] = new int[7]{5, 10, 4, 6, 7, 7, 4};
		SHabillity[4] = new int[7]{7, 7, 2, 6, 5, 3, 5};
		SHabillity[5] = new int[7]{9, 5, 4, 5, 7, 5, 6};
		SHabillity[6] = new int[7]{7, 6, 7, 8, 7, 9, 7};
		SHabillity[7] = new int[7]{2, 2, 4, 7, 10, 2, 8};
		SHabillity[8] = new int[7]{1, 1, 5, 8, 7, 1, 9};


		Labillity[0] = new int[7]{7, 6, 8, 9, 8, 8, 1};
		Labillity[1] = new int[7]{6, 3, 9, 8, 4, 4, 2};
		Labillity[2] = new int[7]{9, 6, 7, 9, 7, 6, 3};
		Labillity[3] = new int[7]{5, 10, 4, 6, 7, 7, 4};
		Labillity[4] = new int[7]{7, 7, 2, 6, 5, 3, 5};
		Labillity[5] = new int[7]{9, 5, 4, 5, 7, 5, 6};
		Labillity[6] = new int[7]{7, 6, 7, 8, 7, 9, 7};
		Labillity[7] = new int[7]{2, 2, 4, 7, 10, 2, 8};
		Labillity[8] = new int[7]{1, 1, 5, 8, 7, 1, 9};


		Fabillity[0] = new int[7]{7, 6, 8, 9, 8, 8, 1};
		Fabillity[1] = new int[7]{6, 3, 9, 8, 4, 4, 2};
		Fabillity[2] = new int[7]{9, 6, 7, 9, 7, 6, 3};
		Fabillity[3] = new int[7]{5, 10, 4, 6, 7, 7, 4};
		Fabillity[4] = new int[7]{7, 7, 2, 6, 5, 3, 5};
		Fabillity[5] = new int[7]{9, 5, 4, 5, 7, 5, 6};
		Fabillity[6] = new int[7]{7, 6, 7, 8, 7, 9, 7};
		Fabillity[7] = new int[7]{2, 2, 4, 7, 10, 2, 8};
		Fabillity[8] = new int[7]{1, 1, 5, 8, 7, 1, 9};

		//投手
		//maxspeed ,control ,stamina , ←変化量, ←変化球のspeed, ↙︎変化量, ↙︎変化球のspeed, ↓変化量, ↓変化球のspeed, ↘︎変化量, ↘︎変化球のspeed , →変化量, →変化球のspeed　,hand , role
/*
speed　ストレートのmaxの速さ
control コントロール　1 ~ 10
stamina　体力 1 ~ 10
変化量  0 ~ 9
変化球speed  そのまま
hand 利き手(0= 右 ,1= 左)
role 役割　(0=先発　, 1=救援)
*/
		YGpitcher[0] = new int[15]{155,9,9 ,8,140 ,7,120 ,7,142 ,6,142 ,5,148 ,0 ,0};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
