using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {
//試合の主な情報、状態、試合進行を管理する場所,mainのスクリプト

	public string mode = "pitching";//playerがどちら側を操作しているか batting or pitching
	public string ballresult = "nohit";//nohit,hit,faul,fly,homerun,entytletwobase
	public string ballstate;//ballstateを取りに行きたい。
	public string runchoice = "";//runchoiceを取りに行きたい。
	public string omoteura = "表";//今が裏か表か
	public string gamestate = "pitch";//カウント判定の状態 pitch→judge→pitch... or pitch→hit→conversion→pitch...
	public bool DeffenceRunmode = false;//守備走塁の状態になったらtrue,
	public int inning = 1;//今のイニング数　表裏はmodeからわかる。
	int lastinning = 3;//イニングの終わり


	public string inningpoint;//scoreboardのinningpointの入れ物
	public string selectmyteam;//敵として選択したチーム
	public string selectcputeam;//味方として選択したチーム

	int inningpnt;
	public int myscore = 0;//playerのsccore
	public int cpuscore = 0;//cpuのsccore
	public int howhit = 0;//ヒットの中でもツーベースなのかスリーベースなのかなどを判定する。
	public int nowbatter;//今の打者が何番目の打者なのか
	public bool batterchange;//バッター交代の時のタイミング

	public GameObject strikezone;
	public GameObject Inning;//今は何回か
	public GameObject scoreboard;//スコアボード
	public GameObject runner3;//ランナー3
  public GameObject MainCam;
  public GameObject BatterCam;
  public GameObject PitchingCam;
  public GameObject inningresultCam;
  public GameObject NotconcernedpitchCam;
  public GameObject NotconcernedpitchCamera;

	Text text;//inningのtextが入る




	// Use this for initialization
	void Start () {

		if(Random.Range(0,1+1)　>= 2/*0.5f*/){//先攻後攻を乱数で決める
			//後攻
			MainCam.SetActive(false);
			BatterCam.SetActive(false);
			PitchingCam.SetActive(true);
			inningresultCam.SetActive(false);
			NotconcernedpitchCam.SetActive(false);
			NotconcernedpitchCamera.SetActive(false);
			mode = "pitching";
		}else{
			//先攻
			MainCam.SetActive(false);
			BatterCam.SetActive(true);
			PitchingCam.SetActive(false);
			inningresultCam.SetActive(false);
			NotconcernedpitchCam.SetActive(false);
			NotconcernedpitchCamera.SetActive(false);
			mode = "batting";
		}

	}
		void OnCollisionEnter(Collision collision){
			if(collision.gameObject.name == "Runner3"){//ランナーが本塁に生還
					runner3.transform.position = new Vector3( -390, -50, 45);
					if(mode == "batting"){
						myscore += 1;

						//1inningごとの特典計算。文字列を数値にして計算し数値を文字列へして返す。
						inningpnt = int.Parse(scoreboard.GetComponent<scoreboard>().inningpoint);
						inningpnt +=　1;
						inningpoint = inningpnt.ToString(inningpoint);
						scoreboard.GetComponent<scoreboard>().inningpoint = inningpoint;

					}if(mode == "pitching"){
						cpuscore += 1;
						inningpnt = int.Parse(scoreboard.GetComponent<scoreboard>().inningpoint);
						inningpnt +=　1;
						inningpoint = inningpnt.ToString(inningpoint);
						scoreboard.GetComponent<scoreboard>().inningpoint = inningpoint;

					}
		}
}
	// Update is called once per frame
	void Update () {
		if(mode == "batting"){

		}else if(mode == "pitching"){

		}
		if(ballstate == "homerun"){
			runchoice = "run";
			batterchange = true;
		}if(ballstate == "hit"){
			runchoice = "run";
			batterchange = true;
		}if(ballstate == "2Bhit"){
			runchoice = "run";
			batterchange = true;
		}if(ballstate == "3Bhit"){
			runchoice = "run";
			batterchange = true;
		}
		if(strikezone.GetComponent<strikezone>() .outcount	== 3){
			Mode();
			strikezone.GetComponent<strikezone>() .outcount = 0;
		}
		if(batterchange == true){
			Batterchange();
			batterchange = false;
		}
	}

	void Mode (){//攻守交代、ゲーム終了を行う。
		//攻守交代
		//if(strikezone.GetComponent<strikezone>() .outcount	> 2){//3out change

			switch(mode){
				case "batting":
					MainCam.SetActive(false);
					BatterCam.SetActive(false);
					PitchingCam.SetActive(true);
					mode = "pithcing";
					scoreboard.GetComponent<scoreboard>().inningpoint = "0";
					if(omoteura == "表"){
						omoteura = "裏";
					}else if(omoteura == "裏"){
						omoteura = "表";
						inning += 1;
					}
					break;
				case "pitching":
					MainCam.SetActive(false);
					PitchingCam.SetActive(false);
					BatterCam.SetActive(true);
					mode = "batting";
					scoreboard.GetComponent<scoreboard>().inningpoint = "0";
					if(omoteura == "表"){
						omoteura = "裏";
					}else if(omoteura == "裏"){
						omoteura = "表";
						inning += 1;
					}
					break;
			}
			text = Inning.GetComponent<Text>();
			text.text = inning.ToString()+"回"+omoteura;
		//}
		//ゲーム終了時　＆　勝敗判定
		if(inning == lastinning){　//ゲームが終わる
			if(myscore > cpuscore){
				//プレイヤーの勝ち
			}if(myscore < cpuscore){
				//プレイヤーの負け
			}else{
				//引き分け
			}
		}
	}


	void Batterchange(){
		nowbatter += 1;
	}
}