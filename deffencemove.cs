using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deffencemove : MonoBehaviour {
//守備の動きの管理、アニメーターの管理
	public GameObject game;//game.cs
	public GameObject pitchball;//pitchball.cs
	public GameObject hitjudge;//hitjudge.cs
	public GameObject baserun;//baserun.cs
	public GameObject Throwing;//throwing.cs
	public GameObject strikezone;//strikezone.cs

	public GameObject first;//1Bglove
	public GameObject second;//2Bglove
	public GameObject third;//3Bglove
	public GameObject SS;//SSglove
	public GameObject center;//centerglove
	public GameObject right;//rightglove
	public GameObject left;//leftglove

	public GameObject holder;//ボールを追って持った人

	public GameObject base1;//1塁
	public GameObject base2;//2塁
	public GameObject base3;//3塁
	public GameObject Homebase;//本塁

	public GameObject runner0;
	public GameObject runner1;
	public GameObject runner2;
	public GameObject runner3;

	public GameObject First;//first
	public GameObject	Second;//second
	public GameObject Third;//third
	public GameObject Ss;//SS

	public Animator animatorfirst;//first
	public Animator animatorsecond;//second
	public Animator animatorthird;//third
	public Animator animatorSS;//SS

	public bool firstchase;//1Bがボールを追うかの判断基準
	public bool secondchase;//2Bがボールを追うかの判断基準
	public bool thirdchase;//3Bがボールを追うかの判断基準
	public bool SSchase;//SSがボールを追うかの判断基準
	public bool centerchase;//centerがボールを追うかの判断基準
	public bool rightchase;//rightがボールを追うかの判断基準
	public bool leftchase;//leftがボールを追うかの判断基準

	public string ballDropPoint;//打球の落下地点がfairかfaulかを調べる

	SphereCollider firstCollider;//1Bの守備範囲
	SphereCollider secondCollider;//2Bの守備範囲
	SphereCollider SSCollider;//SSの守備範囲
	SphereCollider thirdCollider;//3Bの守備範囲
	SphereCollider rightCollider;//守備範囲
	SphereCollider centerCollider;//守備範囲
	SphereCollider leftCollider;//守備範囲
	public int BoundNum;//バウンドした回数

	public bool state1B;//守備で内野手が塁に着いているか着いていないか
	public bool state2B;
	public bool state3B;
	public bool stateHB;

	public string base1judge;//その塁がsafeかoutかまだ審議中か
	public string base2judge;
	public string base3judge;
	public string homebasejudge;

	public	GameObject going0base;//走者が向かっている塁
	public	GameObject going1base;//				"					
	public	GameObject going2base;//				"					
	public	GameObject going3base;//				"					

	public int[] throwchoice;//投げる塁の選択肢(走者が何人いて向かっている塁の数) 0は投げる必要なし 1は投げれる選択肢
	public bool throwjudge;//送球判断するタイミング
	public int throwpoint1B;//ボールを投げる塁の優先度を判断するための評価点　高ければ高いほど優先度が高い
	public int throwpoint2B;//						"									
	public int throwpoint3B;//						"									
	public int throwpointHB;//						"									

	public bool runmode;//走塁守備のシミュレーションが行われる枯れないか


//評価の観点 15(min) ~ 100(max)
//・ランナーからそのランナーが向かっている塁への距離 近いほど評価は高い
//ランナーからの距離が一番近い = 35 ,	"	二番目に近い = 25 ,	"	三番目に近い = 20,	"	一番遠い = 20
//・それぞれの塁の価値　HB > 3B > 2B > 1B　(2OUTの場合どこをアウトにしても状況は同じため関係ない)
//HB = 40 , 3B = 28 , 2B = 25, 1B = 15　評価点
//・ボールを取った野手から それぞれの塁への距離
// 一番近い塁 = 25, 二番目に近い塁 = 15, 三番目に近い塁 = 10, 一番遠い塁 = 0

//なお、評価点が同じ場合　,重要度の高い塁へ投げる
//例) 2塁と 3塁が同じ評価点なら3塁へ投げる

	public string throwing;//nocatch → hit → hold → throw
												 //nocatch → hit →　hold
	// Use this for initialization
	void Start () {
		runmode = false;
		BoundNum = 0;
		throwing = "nocatch";

		animatorfirst = First.GetComponent<Animator>();
		animatorsecond = Second.GetComponent<Animator>();
		animatorthird = Third.GetComponent<Animator>();
		animatorSS = Ss.GetComponent<Animator>();

		throwchoice = new int[4]{0,0,0,0};

	}

	// Update is called once per frame
	void Update () {
		if(throwing != "hit" && throwing != "nocatch"){//誰かが持った時ボールを持った時に判断
			throwjudge = true;
			if(throwjudge == true){//一度だけ判断
				Throwing.GetComponent<throwing>().catchtiming = true;
				Throwjudge();//送球判断
				throwjudge = false;
			}
		}
		DeffenceAnimation();//守備のAnimator
		if(pitchball.GetComponent<pitchball>().ballstate == "catch"){//守備が終わったら
			ballDropPoint = "nohit";
			BoundNum = 0;
			//判定バウンド回数リセット
		}

		//投げる時までに守備位置に戻る
		if(pitchball.GetComponent<pitchball>().ballstate == "pitched"){
			//色々な初期化処理
			base1judge = "nojudge";
			base2judge = "nojudge";
			base3judge = "nojudge";
			homebasejudge = "nojudge";
			if(runmode == true){
				strikezone.GetComponent<strikezone>().strikecount = 0;
				strikezone.GetComponent<strikezone>().ballcount = 0;
			}


			if(firstchase == true){
				first.transform.position = new Vector3(365,50,105);//元の位置に戻る
				first.transform.rotation = Quaternion.Euler(0, 220, 0);//元の角度に戻る
				firstchase = false;
			}
			if(secondchase == true){
				second.transform.position = new Vector3(195,50,405);
				second.transform.rotation = Quaternion.Euler(0, 200, 0);
				secondchase = false;
			}
			if(SSchase == true){
				SS.transform.position = new Vector3(-195,50,405);
				SS.transform.rotation = Quaternion.Euler(0, -200, 0);
				SSchase = false;
			}
			if(thirdchase == true){
				third.transform.position = new Vector3(-365,50,105);
				third.transform.rotation = Quaternion.Euler(0, -220, 0);
				thirdchase = false;
			}
			if(centerchase == true){
				center.transform.position = new Vector3(0,50,1200);
				center.transform.rotation = Quaternion.Euler(0, 180, 0);
				centerchase = false;
			}
			if(rightchase == true){
				right.transform.position = new Vector3(800,50,800);
				right.transform.rotation = Quaternion.Euler(0, 210, 0);
				rightchase = false;
			}
			if(leftchase == true){
				left.transform.position = new Vector3(-800,50,800);
				left.transform.rotation = Quaternion.Euler(0, -200, 0);
				leftchase = false;
			}
		}
	}
	void Throwjudge(){//ボールを渡すべき塁を判断判断
		switch(throwing){//throwingの結果からholderが誰かを入れる
			case "1Bhold":
				holder = first;
				break;
			case "2Bhold":
				holder = second;
				break;
			case "SShold":
				holder = SS;
				break;
			case "3Bhold":
				holder = third;
				break;
			case "lefthold":
				holder = left;
				break;
			case "centerhold":
				holder = center;
				break;
			case "righthold":
				holder = right;
				break;
		}
		//まずランナーがいるかどうかで計算していく
		if(baserun.GetComponent<baserun>().runner0treadbase != "fly"||baserun.GetComponent<baserun>().runner0intention == "stop"){//打者(走者)がフライアウトにならなければ
			switch(baserun.GetComponent<baserun>().runner0treadbase){//一塁がどこまで踏んだかを
				case "Batterbox":
					going0base = base1;
					break;
				case "1B":
					if(baserun.GetComponent<baserun>().runner0intention == "toNext"){
						going0base = base2;//1Bまで踏んでいて次の塁を狙っているのなら投げるべき塁は二塁
					}if(baserun.GetComponent<baserun>().runner0intention == "return"){
						going0base = base1;//1Bまで踏んでいて帰塁しているのなら投げるべき塁は二塁
					}
					break;
				case "2B":
					if(baserun.GetComponent<baserun>().runner0intention == "toNext"){
						going0base = base3;
					}if(baserun.GetComponent<baserun>().runner0intention == "return"){
						going1base = base2;
					}
					break;
				case "3B":
					if(baserun.GetComponent<baserun>().runner0intention == "toNext"){
						going0base = Homebase;
					}if(baserun.GetComponent<baserun>().runner0intention == "return"){
						going0base = base3;
					}
					break;
			}
			float runner0dis = Vector3.Distance(runner0.transform.position, going0base.transform.position);
			float holderdis0 = Vector3.Distance(holder.transform.position,going0base.transform.position);

		}else{

		}
		if(baserun.GetComponent<baserun>().runnerposition[0] == 1||baserun.GetComponent<baserun>().runner1intention == "stop"){//ランナーがいる時
			switch(baserun.GetComponent<baserun>().runner1treadbase){
				case "1B":
					if(baserun.GetComponent<baserun>().runner1intention == "toNext"){
						going1base = base2;
						throwchoice[1] = 1;
					}if(baserun.GetComponent<baserun>().runner1intention == "return"){
						going1base = base1;
						throwchoice[0] = 1;
					}
					break;
				case "2B":
					if(baserun.GetComponent<baserun>().runner1intention == "toNext"){
						going1base = base3;
						throwchoice[2] = 1;
					}if(baserun.GetComponent<baserun>().runner1intention == "return"){
						going1base = base2;
						throwchoice[1] = 1;
					}
					break;
				case "3B":
					if(baserun.GetComponent<baserun>().runner1intention == "toNext"){
						going1base = Homebase;
						throwchoice[3] = 1;
					}if(baserun.GetComponent<baserun>().runner1intention == "return"){
						going1base = base3;
						throwchoice[2] = 1;
					}
					break;
			}
			float runner1dis = Vector3.Distance(runner1.transform.position,going1base.transform.position);
			float holderdis1 = Vector3.Distance(holder.transform.position,going1base.transform.position);
		}else{
			
		}
		if(baserun.GetComponent<baserun>().runnerposition[1] == 1||baserun.GetComponent<baserun>().runner2intention == "stop"){//ランナーがいる時
			switch(baserun.GetComponent<baserun>().runner2treadbase){
				case "2B":
					if(baserun.GetComponent<baserun>().runner2intention == "toNext"){
						going2base = base3;
						throwchoice[2] = 1;
					}if(baserun.GetComponent<baserun>().runner2intention == "return"){
						going2base = base2;
						throwchoice[1] = 1;
					}
					break;
				case "3B":
					if(baserun.GetComponent<baserun>().runner2intention == "toNext"){
						going2base = Homebase;
						throwchoice[3] = 1;
					}if(baserun.GetComponent<baserun>().runner2intention == "return"){
						going2base = base3;
						throwchoice[2] = 1;
					}
					break;
			}
			float runner2dis = Vector3.Distance(runner2.transform.position,going2base.transform.position);
			float holderdis2 = Vector3.Distance(holder.transform.position,going2base.transform.position);
		}else{

		}
		if(baserun.GetComponent<baserun>().runnerposition[2] == 1||baserun.GetComponent<baserun>().runner3intention == "stop"){//ランナーがいる時
			switch(baserun.GetComponent<baserun>().runner3treadbase){
				case "2B":
					if(baserun.GetComponent<baserun>().runner3intention == "toNext"){
						going3base = base3;
						throwchoice[2] = 1;
					}if(baserun.GetComponent<baserun>().runner3intention == "return"){
						going3base = base2;
						throwchoice[1] = 1;
					}
					break;
				case "3B":
					if(baserun.GetComponent<baserun>().runner3intention == "toNext"){
						going3base = Homebase;
						throwchoice[3] = 1;
					}if(baserun.GetComponent<baserun>().runner3intention == "return"){
						going3base = base3;
						throwchoice[2] = 1;
					}
					break;
			}
			float runner3dis = Vector3.Distance(runner3.transform.position,going3base.transform.position);
			float holderdis3 = Vector3.Distance(holder.transform.position,going3base.transform.position);
		}else{
		}
		//throwchoiceは投げることのできる選択肢 [0]は1塁,[1]は2塁,[2]は3塁[3]は本塁
		//数値が0(その塁に向かう走者なし)なら投げる必要がなく選択肢に入らない
		//数値が高ければ高いほど優先度が高い
		//どの塁が0なのか優先度が高いのかを判断できるように変えてはいけない

		//元々の塁ごとの価値評価
		//先の塁ほど得点につながりやすいため優先度が高い
		if(throwchoice[0] == 1){
			throwpoint1B = 14;
		}else{
			throwpoint1B = 0;
		}if(throwchoice[1] == 1){
			throwpoint2B = 24;
		}else{
			throwpoint2B = 0;
		}if(throwchoice[2] == 1){
			throwpoint3B = 27;
		}else{
			throwpoint3B = 0;
		}if(throwchoice[3] == 1){
			throwpointHB = 39;
		}else{
			throwpointHB = 0;
		}

		int[] sortrundis = throwchoice;//向かっている塁への距離が短い順に並び替えるための配列
		int[] sortholddis = throwchoice;//ボールをとった野手から各塁への距離順を並び替えるための配列


		//優先度の足し算をする
		if(throwchoice[0] == 1){
			throwchoice[0] += throwpoint1B;
		}if(throwchoice[1] == 1){
			throwchoice[1] += throwpoint2B;
		}if(throwchoice[2] == 1){
			throwchoice[2] += throwpoint3B;
		}if(throwchoice[3] == 1){
			throwchoice[3] += throwpointHB;
		}

		//これを大きい順に並べ(throwchoiceで0の数値の場所には投げない)
		//Throwing.GetComponent.<throwing>().DirectionofThrowingを一番優先度の高いものにする。


		//最後に次に送球するときのことも考え一度投げたところは投げないようにthrowchoiceの値を0にする。

		//...走者から向かっている塁までの距離が近い順に並び替える　関係中
		//...取った野手から塁までの距離　関係大

	}
	void DeffenceAnimation(){
		if(firstchase == true){
			animatorfirst.SetBool("move", true);
		}else{
			animatorfirst.SetBool("move", false);
		}
		if(secondchase == true){
			animatorsecond.SetBool("move", true);
		}else{
			animatorsecond.SetBool("move", false);
		}
		if(SSchase == true){
			animatorSS.SetBool("move", true);
		}else{
			animatorSS.SetBool("move", false);
		}
		if(thirdchase == true){
			animatorthird.SetBool("move", true);
		}else{
			animatorthird.SetBool("move", false);
		}
	}

	void OnTriggerStay(Collider collider){
		if(throwing == "hit"){
			if(collider.gameObject.name == "deffence_right"){
				rightchase = true;
			}
			if(collider.gameObject.name == "deffence_cen"){
				centerchase = true;
			}
			if(collider.gameObject.name == "deffence_left"){
				leftchase = true;
			}
		}
	}
	void OnTriggerExit(Collider collider){
		if(collider.gameObject.name == "deffence_right"){
			rightchase = false;
		}
		if(collider.gameObject.name == "deffence_cen"){
			centerchase = false;
		}
		if(collider.gameObject.name == "deffence_left"){
			leftchase = false;
		}
	}
}