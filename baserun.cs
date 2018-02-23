using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baserun : MonoBehaviour {
	//各類それぞれの当たり判定感知,残塁状況把握

	public GameObject runner0;//打者が走る
	public GameObject runner1;//1塁走者
	public GameObject runner2;//2塁走者
	public GameObject runner3;//3塁走者
	public GameObject batter;//打者

	public GameObject runner0Nextbase;//走者(打者)の次の塁
	public GameObject runner0Rearbase; //走者(打者)の前の塁
	public GameObject runner1Nextbase;//1塁走者の次の塁
	public GameObject runner1Rearbase; //1塁走者の前の塁
	public GameObject runner2Nextbase;//2塁走者の次の塁
	public GameObject runner2Rearbase; //2塁走者の前の塁

	public string runner0treadbase;//走者(打者)が最後に踏んだ塁
	public string runner1treadbase;//1塁走者が最後に踏んだ塁
	public string runner2treadbase;//2塁走者が最後に踏んだ塁
	public string runner3treadbase;//3塁走者が最後に踏んだ塁
	//3塁走者はホームベースか3塁だけなのでいらない

	public GameObject base1;
	public GameObject base2;
	public GameObject base3;
	public GameObject Homebase;

	public GameObject pitchball;//pitchball.cs
	public GameObject game;//game.cs
	public GameObject strikezone;//strikezone.cs
	public GameObject deffencemove;//deffencemove.cs
	public GameObject MainCam;//maincam
	public GameObject BatterCam;//battercam

	public bool hittim;//当たった時(一回だけ実行させる)
	public bool basestate;//塁上の状況を見て動かす(一回だけ実行させる)
	public bool advanceorback;//進むか戻るかの判断(1回だけ実行させる)

	public string runner0intention;//走者(打者)
	public string runner1intention;//1塁走者が走るか "toNext"なら進塁 "return"なら帰塁	 "stop"なら残塁
	public string runner2intention;//2塁走者が走るか "toNext"なら進塁 "return"なら帰塁　"stop"なら残塁
	public string runner3intention;//3塁走者が走るか "toNext"なら進塁 "return"なら帰塁　"stop"なら残塁

	public int run1pos;//1塁にいた走者が走っている時に通過した場所
	public int run2pos;//2塁にいた走者が走っている時に通過した場所
	public int run3pos;//3塁にいた走者が走っている時に通過した場所

	public int y;//catchに戻るための延期時間のための変数

	public float runner0speed;//走者(打者)の速さ
	public float runner1speed;//1塁走者の速さ
	public float runner2speed;//2塁走者の速さ
	public float runner3speed;//3塁走者の速さ

	public string gettobase = "not gettobase";//走者が塁に着いたか

	public int[] runnerposition;//走者のいる塁 走者がいるなら1いないなら0で{(1塁に走者はいるか)、(2塁に走者はいるか),(3塁に走者はいるか)}

	// Use this for initialization
	void Start (){
		//runner1が二塁に着くとrunner1は元の位置に戻り、runner2となる。
		runnerposition = new int[3]{0,0,0};//走者なしの状態から始める {(1塁の走者がいるかどうか),(1塁の走者がいるかどうか),(1塁の走者がいるかどうか)}
		runner0speed = 100f;
		runner1speed = 100f;
		runner2speed = 100f;
		runner3speed = 100f;
		hittim = false;

		runner1intention = "stop";
		runner2intention = "stop";
		runner3intention = "stop";
		runner0treadbase = "Batterbox";
	}

	// Update is called once per frame
	void Update (){
		if(hittim == true){//ballstateが"hit"になった一回だけ呼び出したい
			if(pitchball.GetComponent<pitchball>().ballstate == "hit"){
				MainCam.SetActive(true);
				BatterCam.SetActive(false);
				runstart();
				hittim = false;
			}
		}
		if(runner0treadbase == "fly"){//フライアウトで1OUTがついてrunner0が消える
			runner0treadbase = "Batterbox";
			strikezone.GetComponent<strikezone>().outcount += 1;
			runner0.SetActive(false);
		}

				if(pitchball.GetComponent<pitchball>().ballstate == "catch"){
					MainCam.SetActive(false);
					BatterCam.SetActive(true);
					batter.SetActive(true);
				}if(pitchball.GetComponent<pitchball>().ballstate == "pitched"){
			runnersets();//走者を塁に置き直す
				advanceorback = true;
				hittim = true;
				basestate = true;
			//アニメーション開始
		}if(deffencemove.GetComponent<deffencemove>().throwing == "hit"||deffencemove.GetComponent<deffencemove>().throwing == "catch"||deffencemove.GetComponent<deffencemove>().throwing == "throw"){
			runner0start();
			if(pitchball.GetComponent<pitchball>().ballstate == "hit"){
				//走者が走り出し進塁をする
				if(basestate == true){//一回だけ
					Basestate();//塁上の走者を確認しその塁に走者がいたら走らせるようにする
					basestate = false;
				}
				if(deffencemove.GetComponent<deffencemove>().BoundNum > 0){//ボールが地面についたら走らせる
					
					if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){
						if(advanceorback == true){
							AdvanceorBack();//前の塁に戻るか次の塁に進むかの判断をする
							advanceorback = false;
						}
					}else{
						Run();//走らせる、動かせる
					}
				}
			}
			if(runner0intention == "stop"&& runner1intention == "stop" && runner2intention == "stop" && runner3intention == "stop"){
			//終わりの条件　全てが止まったら
			y += 1;
			if(pitchball.GetComponent<pitchball>().ballstate != "catch"){
				if(y >= 50){
					pitchball.GetComponent<pitchball>().ballstate = "catch";
					y = 0;
					//投球モーションに入るように
				}
			}
			}
		}
	}
	void runstart(){
			//打者が走りだす。
			runner0.SetActive(true);//走者(打者)を見えるようにする
			batter.SetActive(false);//打者を一旦見えなくする　スクリプトもfalseになってしまう
			runner0intention = "toNext";//走者(打者)が走り始める
			runner0treadbase = "Batterbox";
			runner0.transform.LookAt(base1.transform);
			Debug.Log("一回だけしか見えない");
	}
	void runner0start(){
		if(runner0intention != "stop"){//止まってない時
			if(runner0intention == "toNext"){//進塁 
				switch(runner0treadbase){
					case "Batterbox":
						runner0.transform.LookAt(base1.transform);
						//Debug.Log("1塁に向かって走る");
						break;
					case "1B":
						runner0.transform.LookAt(base2.transform);
						//Debug.Log("2塁に向かって走る");
						break;
					case "2B":
						runner0.transform.LookAt(base3.transform);
						//Debug.Log("3塁に向かって走る");
						break;
					case "3B":
						runner0.transform.LookAt(Homebase.transform);
						//Debug.Log("本塁に向かって走る");
						break;
				}
			}else if(runner0intention == "return"){//帰塁
				switch(runner0treadbase){
					case "1B":
						runner0.transform.LookAt(base1.transform);
						Debug.Log("1塁に戻る");
						break;
					case "2B":
						runner0.transform.LookAt(base2.transform);
						Debug.Log("2塁に戻る");
						break;
					case "3B":
						runner0.transform.LookAt(base3.transform);
						break;
				}
			}
			Vector3 velocity = runner0.transform.rotation * new Vector3(0, 0 ,runner0speed);//進む
			runner0.transform.position += velocity * Time.deltaTime;//進む
			}
	}
	void runnersets(){
		switch(runner3treadbase){
			case "Home":
				runnerposition[2] = 0;
				break;
			case "3B":
				runnerposition[2] = 1;
				break;
		}
		switch(runner2treadbase){
			case "Home":
				runnerposition[1] = 0;
				break;
			case "3B":
				runnerposition[1] = 0;
				runnerposition[2] = 1;
				break;
			case "2B":
				runnerposition[1] = 1;
				break;
		}
		switch(runner1treadbase){
			case "Home":
				runnerposition[1] = 0;
				break;
			case "3B":
				runnerposition[0] = 0;
				runnerposition[2] = 1;
				break;
			case "2B":
				runnerposition[0] = 0;
				runnerposition[1] = 1;
				break;
			case "1B":
				runnerposition[0] = 1;
				break;
		}
		switch(runner0treadbase){
			case "Home":
				break;
			case "3B":
				runnerposition[2] = 1;
				break;
			case "2B":
				runnerposition[1] = 1;
				break;
			case "1B":
				runnerposition[0] = 1;
				break;
			case "Batterbox":
				//OUTにする
				break;
		}

		if(runnerposition[0] == 1){
			runner1.transform.position = new Vector3( 370, 15, 50);
		}else{
			runner1.transform.position = new Vector3( 370, -100, 50);//地面の下に隠す
		}
		if(runnerposition[1] == 1){
			runner2.transform.position = new Vector3( 0, 15, 430);
		}else{
			runner2.transform.position = new Vector3( 0, -100, 430);//地面の下に隠す
		}
		if(runnerposition[2] == 1){
			runner3.transform.position = new Vector3( -370, 15, 35);
		}else{
			runner3.transform.position = new Vector3( -370, -100, 35);//地面の下に隠す
		}
		runner0.transform.position = new Vector3( 30, 15, -340);//元の位置に戻す
	}
	void Basestate(){
		if(runnerposition[0] == 1){
			runner1treadbase = "1B";
			runner1intention = "toNext";
		}else{
			runner1intention = "stop";
		}if(runnerposition[1] == 1){
			runner1treadbase = "2B";
			runner2intention = "toNext";
		}else{
			runner2intention = "stop";
		}
		if(runnerposition[2] == 1){
			runner1treadbase = "3B";
			runner3intention = "toNext";
		}else{
			runner3intention = "stop";
		}
	}
	void Run(){
		if(runner1intention != "stop"){
			if(runner1intention == "toNext"){//進塁
				switch(runner1treadbase){
					case "1B":
						runner1.transform.LookAt(base2.transform);
						break;
					case "2B":
						runner1.transform.LookAt(base3.transform);
						break;
					case "3B":
						runner1.transform.LookAt(Homebase.transform);
						break;
				}
			}else if(runner1intention == "return"){//帰塁
				switch(runner1treadbase){
					case "1B":
						runner1.transform.LookAt(base1.transform);
						break;
					case "2B":
						runner1.transform.LookAt(base2.transform);
						break;
					case "3B":
						runner1.transform.LookAt(base3.transform);
						break;
				}
			}
				Vector3 velocity = runner1.transform.rotation * new Vector3(0, 0 ,runner1speed);//進む
				runner1.transform.position += velocity * Time.deltaTime;//進む
		}
		if(runner2intention != "stop"){
			if(runner2intention == "toNext"){
				switch(runner2treadbase){
					case "2B":
						runner2.transform.LookAt(base3.transform);
						break;
					case "3B":
						runner2.transform.LookAt(Homebase.transform);
						break;
				}
			}else if(runner2intention == "return"){
				switch(runner2treadbase){
					case "2B":
						runner2.transform.LookAt(base2.transform);
						break;
					case "3B":
						runner2.transform.LookAt(base3.transform);
						break;
				}
			}
				Vector3 velocity = runner2.transform.rotation * new Vector3(0, 0 ,runner2speed);//進む
				runner2.transform.position += velocity * Time.deltaTime;//進む
		}
		if(runner3intention != "stop"){
			if(runner3intention == "toNext"){
				runner3.transform.LookAt(Homebase.transform);
			}else if(runner3intention == "return"){
				runner3.transform.LookAt(base3.transform);
			}
				Vector3 velocity = runner3.transform.rotation * new Vector3(0, 0 ,runner3speed);//進む
				runner3.transform.position += velocity * Time.deltaTime;//進む
		}
	}
	void AdvanceorBack(){
		Debug.Log("進退の判断");
			if(runnerposition[2] == 1){//3塁走者がいる場合
				//戻った方が早いのか進んだ方が早いのかを計算する
				float toNextbaseDis;//走者から次の塁までの距離
				float toRearbaseDis;//走者から前の塁までの距離
				//三平方の定理から 斜辺(距離)*斜辺(距離) = 隣辺(x)*隣辺(x) + 対辺(z)*対辺(z)
				//Math.Abs(x)　は xの絶対値
				GameObject runner3Nextbase = Homebase;//3塁走者の次の塁
				GameObject runner3Rearbase = base3; //3塁走者の前の塁
				toNextbaseDis = Mathf.Abs(runner3Nextbase.transform.position.x - runner3.transform.position.x) * Mathf.Abs(runner3Nextbase.transform.position.x - runner3.transform.position.x) + Mathf.Abs(runner3Nextbase.transform.position.z - runner3.transform.position.z) * Mathf.Abs(runner3Nextbase.transform.position.z - runner3.transform.position.z);
				toRearbaseDis = Mathf.Abs(runner3Rearbase.transform.position.x - runner3.transform.position.x) * Mathf.Abs(runner3Rearbase.transform.position.x - runner3.transform.position.x) + Mathf.Abs(runner3Rearbase.transform.position.z - runner3.transform.position.z) * Mathf.Abs(runner3Rearbase.transform.position.z - runner3.transform.position.z);
				if(toNextbaseDis <= toRearbaseDis){//次の塁への距離が前の塁への距離が少なければ
					runner3intention = "toNext";
				}else{
					runner3intention = "return";
				}
			}

			if(runnerposition[1] == 1){//2塁走者がいる場合
				if(runner3intention == "toNext"){//元3塁走者が進む場合
					//戻った方が早いのか進んだ方が早いのかを計算する
					float toNextbaseDis;//走者から次の塁までの距離
					float toRearbaseDis;//走者から前の塁までの距離
					switch(runner2treadbase){
						case "2B":
							runner2Nextbase = base3;
							runner2Rearbase = base2;
							break;
						case "3B":
							runner2Nextbase =	Homebase;
							runner2Rearbase = base3;
							break;
					}
					//三平方の定理から 斜辺(距離)*斜辺(距離) = 隣辺(x)*隣辺(x) + 対辺(y)*対辺(y)
				toNextbaseDis = Mathf.Abs(runner2Nextbase.transform.position.x - runner2.transform.position.x) * Mathf.Abs(runner2Nextbase.transform.position.x - runner2.transform.position.x) + Mathf.Abs(runner2Nextbase.transform.position.z - runner2.transform.position.z) * Mathf.Abs(runner2Nextbase.transform.position.z - runner2.transform.position.z);
				toRearbaseDis = Mathf.Abs(runner2Rearbase.transform.position.x - runner2.transform.position.x) * Mathf.Abs(runner2Rearbase.transform.position.x - runner2.transform.position.x) + Mathf.Abs(runner2Rearbase.transform.position.z - runner2.transform.position.z) * Mathf.Abs(runner2Rearbase.transform.position.z - runner2.transform.position.z);
					if(toNextbaseDis <= toRearbaseDis){//次の塁への距離が前の塁への距離が少なければ
						runner2intention = "toNext";
					}else{
						runner2intention = "return";
					}
				}else{
					runner2intention = "return";
				}
			}
			if(runnerposition[0] == 1){//1塁走者がいる場合
				if(runner2intention == "toNext"){//元2塁走者が進む場合
					//戻った方が早いのか進んだ方が早いのかを計算する
					float toNextbaseDis;//走者から次の塁までの距離
					float toRearbaseDis;//走者から前の塁までの距離
					switch(runner1treadbase){
						case "1B":
							runner1Nextbase = base2;
							runner1Rearbase = base1;
							break;
						case "2B":
							runner1Nextbase = base3;
							runner1Rearbase = base2;
							break;
						case "3B":
							runner1Nextbase =	Homebase;
							runner1Rearbase = base3;
							break;
					}
					//三平方の定理から 斜辺(距離)*斜辺(距離) = 隣辺(x)*隣辺(x) + 対辺(y)*対辺(y)
					toNextbaseDis = Mathf.Abs(runner1Nextbase.transform.position.x - runner1.transform.position.x) * Mathf.Abs(runner1Nextbase.transform.position.x - runner1.transform.position.x) + Mathf.Abs(runner1Nextbase.transform.position.z - runner1.transform.position.z) * Mathf.Abs(runner1Nextbase.transform.position.z - runner1.transform.position.z);
					toRearbaseDis = Mathf.Abs(runner1Rearbase.transform.position.x - runner1.transform.position.x) * Mathf.Abs(runner1Rearbase.transform.position.x - runner1.transform.position.x) + Mathf.Abs(runner1Rearbase.transform.position.z - runner1.transform.position.z) * Mathf.Abs(runner1Rearbase.transform.position.z - runner1.transform.position.z);
					if(toNextbaseDis <= toRearbaseDis){//次の塁への距離が前の塁への距離が少なければ
						runner1intention = "toNext";
					}else{
						runner1intention = "return";
					}
				}else{
					runner1intention = "return";
				}
			}
			if(runner1intention == "toNext"){//元1塁走者が進む場合
				//戻った方が早いのか進んだ方が早いのかを計算する
				float toNextbaseDis;//走者から次の塁までの距離
				float toRearbaseDis;//走者から前の塁までの距離
				switch(runner1treadbase){
					case "1B":
						runner0Nextbase = base2;
						runner0Rearbase = base1;
						break;
					case "2B":
						runner0Nextbase = base3;
						runner0Rearbase = base2;
						break;
					case "3B":
						runner0Nextbase =	Homebase;
						runner0Rearbase = base3;
						break;
				}
				//三平方の定理から 斜辺(距離)*斜辺(距離) = 隣辺(x)*隣辺(x) + 対辺(y)*対辺(y)
				toNextbaseDis = Mathf.Abs(runner0Nextbase.transform.position.x - runner0.transform.position.x) * Mathf.Abs(runner0Nextbase.transform.position.x - runner0.transform.position.x) + Mathf.Abs(runner0Nextbase.transform.position.z - runner0.transform.position.z) * Mathf.Abs(runner0Nextbase.transform.position.z - runner0.transform.position.z);
				toRearbaseDis = Mathf.Abs(runner0Rearbase.transform.position.x - runner0.transform.position.x) * Mathf.Abs(runner0Rearbase.transform.position.x - runner0.transform.position.x) + Mathf.Abs(runner0Rearbase.transform.position.z - runner0.transform.position.z) * Mathf.Abs(runner0Rearbase.transform.position.z - runner0.transform.position.z);
				if(toNextbaseDis <= toRearbaseDis){//次の塁への距離が前の塁への距離が少なければ
					runner0intention = "toNext";
				}else{
					runner0intention = "return";
				}
			}else{
				runner0intention = "return";
			}
	}
}