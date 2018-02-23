using UnityEngine;
using System.Collections;
using UnityEngine.UI;//textを変えるためにある。

public class pitchball : MonoBehaviour {
	//投げたりボールの動きを管理する。
	//ボールの簡単な当たり判定も管理

	public string ballstate = "catch";//ボールの状態を表す。
	//ballstate  
	//"pitching" "hit" ""
	string ballfallingpoint;//ボールがファールゾーンに落ちたかフェアゾーンに落ちたか
	public string ball;//球種が何か
	string judgetiming;//strikezone.csの変数judgetimingを入れる変数

	float changeup = 3f;//遅い系のボールの変化量
	float changeupSharpness = 2f;//曲がり
	float fork = 5f;//下に落ちる系統のボールの変化量
 	float forkSharpness = 2f;//曲がり
 	float shoot = 1f;//投手から見て右に曲がる系統のボールの変化量
 	float shootSharpness = 2f;//曲がり
 	float slider = 3f;//投手から見て左へ変化するボール変化量
	float sliderSharpness = 2f;//曲がり
 	float curve = 8f;//投手から見て斜め左へ変化するボール変化量
	float curveSharpness = 2f;//曲がり
	float sinker = 2f;//投手から見て斜め右に変化するボール
	float sinkerSharpness = 2f;//曲がり

	public float pitchtime; //ランダムな投球間隔
	public float tim = 0f;//増えていく

	int choiceballnumber = 4;//球種数
	int judge = 1;
	public int choiceball = 1;//球種変更できる。
	public float speed = 500.0f;//ボールの球速//Random.Range(400, 600+1);
	public int c = 0 ;//遅延をするための変数

	public bool cpuact = false;//cpuの球種、ボールの速さ、投げる場所を決める回数。

	Vector3 pos;//ボールの場所
	public Vector3 targetpos;//標的にかかる重力
	public Vector3 localGravity;//ボールだけの重力

	public GameObject glove;//グローブオブジェクト
	public GameObject target;//標的オブジェクト
	public GameObject BatterCam;

	public GameObject ballname;//ballnameというオブジェクト
	private GameObject PitchingCam;
	public GameObject strikezone;//strikezone.csを取得し、変数を参照するため
	public GameObject game;//game.cs
	public GameObject deffencemove;//deffencemove.cs;
	public GameObject baserun;//baserun.cs

	public Text text;//ballnameのtextが入る変数

	void Start () {
		ballstate = "catch";
	}

	// Update is called once per frame
	void Update(){
		if(game.GetComponent<game> ().mode == "pitching"){
			if(ballstate == "catch"){
				keymanager();
				targetmove();//ターゲットを動かす。
			}
		}
		else if(game.GetComponent<game> ().mode == "batting"){//cpuが投げる側だった場合
			if(cpuact == false){//投球間隔が決まる
				pitchtime = Random.Range(200f, 300f + 1.0f);
				cpuact = true;
			}
			if(cpuact == true){//cpuactがtrueになったら投球間隔を数え始める
				if(ballstate == "catch"){//ballstateが"catch"の場合のみ投げるモーションが始まる
					tim += 1;//timが1ずつ増加
						//pitching();
				}
				if(tim > pitchtime){//投球間隔をこなしたら
					//乱数で球速決め
					speed = 500f;//最低球速
					speed = Random.Range(speed, (speed + 100f) + 1.0f);
					//乱数で投げる場所決め
					target.transform.position = new Vector3(0 , 35 ,-345);
					targetpos = target.transform.position;
					targetpos.y = Random.Range(20.0f, 60.0f);//60 ~ 20
					targetpos.x = Random.Range(-10.0f, 11.0f);
					target.transform.position = new Vector3 (targetpos.x, targetpos.y, targetpos.z);

					//乱数で変化球決め
					choiceball = 1;//Random.Range(1, choiceballnumber + 1);

					fork = 5f;
					slider = 3f;
					shoot = 1f;
					curve = 8f;
					ballstate = "pitching";
					tim = 0f;
					this.GetComponent<TrailRenderer>().enabled = false;//軌道を一時的になくす
				}
			}
		}

		if(ballstate == "release"){
			//投げてからの変化をつける。
			if(ball == "ストレート"){

			}if(ball == "フォーク"){
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.useGravity = false;

					//physicgravityをいじりたいPhysics.gravity = new Vector3(0f, fork, 0f);
					localGravity = new Vector3(0f, -fork, fork/100);
					rigidbody.AddForce(localGravity, ForceMode.Force);//重力を変えて力をボールに加える
					fork += 1.5f;
			}if(ball == "シュート"){
					Rigidbody rigidbody = GetComponent<Rigidbody>();
					localGravity = new Vector3(-shoot, 0f, 0f);
					rigidbody.AddForce(localGravity, ForceMode.Force);//重力を変えて力をボールに加える
					shoot += 0.5f;
			}if(ball == "スライダー"){
					Rigidbody rigidbody = GetComponent<Rigidbody>();
					localGravity = new Vector3(slider, 0f, 0f);
					rigidbody.AddForce(localGravity, ForceMode.Force);//重力を変えて力をボールに加える
					slider += 1.2f;
			}
		}
	}

	void targetmove(){//ターゲットを動かす
		targetpos = target.transform.position;
		//ボールを投げる時の目標を動かす。
		if(Input.GetKey(KeyCode.UpArrow)){
			if(targetpos.y<54.0f){
				targetpos.y += 0.5f;
			}
			target.transform.position = new Vector3 (targetpos.x, targetpos.y, targetpos.z);
		}if(Input.GetKey(KeyCode.DownArrow)){
			if(targetpos.y>14.0f){
				targetpos.y -= 0.5f;
			}
			target.transform.position = new Vector3 (targetpos.x, targetpos.y, targetpos.z);
		}if(Input.GetKey(KeyCode.LeftArrow)){
			if(targetpos.x<14.0f){
				targetpos.x += 0.5f;
			}
			target.transform.position = new Vector3 (targetpos.x, targetpos.y, targetpos.z);
		}if(Input.GetKey(KeyCode.RightArrow)){
			if(targetpos.x>-14.0f){
					targetpos.x -= 0.5f;
			}
			target.transform.position = new Vector3 (targetpos.x, targetpos.y, targetpos.z);
		}
	}
	void keymanager(){//キー管理　投げる・ボールを選ぶ
		if(Input.GetKeyUp("q")){
			if(choiceball < choiceballnumber){
				choiceball += 1;
			}else{
				choiceball = 1;
			}
		}
			switch(choiceball){
				case 1:
					ball = "ストレート";
					break;
				case 2:
					ball = "フォーク";
					break;
				case 3:
					ball = "シュート";
					break;
				case 4:
					ball = "スライダー";
					break;
			}
				//textを変更通りにする
				text = ballname.GetComponent<Text>();
				text.text = ball;

		if(Input.GetKeyUp("m")){//ボールを要求
			fork = 0.005f;
			slider = 0.003f;
			shoot = 0.001f;
			this.GetComponent<TrailRenderer>().enabled = false;//軌道を一時的になくす
		}
	}
	public void pitching(){//投球(CPU、プレイヤーの)
		if(ballstate == "release"){//遅延でアニメーションとボールの動きを合わせる
			Rigidbody rigidbody = GetComponent<Rigidbody>();
			rigidbody.useGravity = true;//rigitcomponentを使うようにする。
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			pos = transform.position;

			switch(choiceball){
				case 1://ストレート
					ball = "ストレート";
					transform.position = new Vector3(pos.x=-14, pos.y=55, pos.z=0);
					transform.LookAt(target.transform);
					rigidbody.velocity = transform.forward * (speed);//加速度を設定
					this.GetComponent<TrailRenderer>().enabled=true;//軌道をつける
					break;
				case 2://フォーク
					ball = "フォーク";
					transform.position = new Vector3(pos.x=-14, pos.y=55, pos.z=0);
					transform.LookAt(target.transform);
					rigidbody.velocity = transform.forward * (speed/1.2f);
					this.GetComponent<TrailRenderer>().enabled=true;
					break;
				case 3://シュート
					ball = "シュート";
					transform.position = new Vector3(pos.x=-14, pos.y=55, pos.z=0);
					transform.LookAt(target.transform);
					rigidbody.velocity = transform.forward * (speed/1.1f);
					this.GetComponent<TrailRenderer>().enabled=true;
					break;
				case 4://スライダー
					ball = "スライダー";
					transform.position = new Vector3(pos.x=-14, pos.y=55, pos.z=0);
					transform.LookAt(target.transform);
					rigidbody.velocity = transform.forward * (speed/1.3f);
					this.GetComponent<TrailRenderer>().enabled=true;
					break;
			}
			cpuact = false;//とりあえず
			tim = 0;
		}
	}
	//collisionは衝突したオブジェクト情報が入る引数で判断
	void OnCollisionEnter(Collision collision){
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		if(collision.gameObject.name == "catcher"){

			//ボールの勢いを消す。
				pos = transform.position;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			//ボールを止めグローブも止めキャッチ状態にする。

				// 必要 glove.transform.position = new Vector3 (pos.x, pos.y, -425);//グローブがボールを追う
				this.transform.position = new Vector3(pos.x, pos.y,-415);
				rigidbody.useGravity = false;//rigitcomponentを使わないにする。
				ballstate = "catch";
			//キャッチを確認し判定を許す
					strikezone.GetComponent<strikezone> ().judgetiming = "pitched";//strikezoneの変数を取得
					//cpuact = false;必要キャッチーができたら
			}
			if(collision.gameObject.name == "bigwall"||collision.gameObject.name == "backnet"){
				strikezone.GetComponent<strikezone> ().judge = "faul";//この場合ランなーは打席に戻る。
			}if(collision.gameObject.name == "corezone"||collision.gameObject.name == "meetBat"||collision.gameObject.name == "meetzone"){
				ballstate = "hit";
				deffencemove.GetComponent<deffencemove>().runmode = true;
				deffencemove.GetComponent<deffencemove>().throwing = "hit";
				judgetiming = "pitched";
				Physics.gravity = new Vector3(0f, -60f, 0f);//当たった瞬間に重力増加


				strikezone.GetComponent<strikezone> ().judge = "faul";
			}
			if(ballstate == "hit"){//打球判定

				if(deffencemove.GetComponent<deffencemove>().BoundNum == 0){
					if(collision.gameObject.name == "gardensand"){
						deffencemove.GetComponent<deffencemove>().BoundNum += 1;
						deffencemove.GetComponent<deffencemove>().ballDropPoint = "faul";
						deffencemove.GetComponent<deffencemove>().runmode = false;
					}if(collision.gameObject.name =="fairzone"){
						deffencemove.GetComponent<deffencemove>().BoundNum += 1;
						deffencemove.GetComponent<deffencemove>().ballDropPoint = "fair";
					}
				}else if(deffencemove.GetComponent<deffencemove>().BoundNum > 0){
					if(collision.gameObject.name == "gardensand"||collision.gameObject.name == "sandofhomebase"||collision.gameObject.name =="fairzone"){
						deffencemove.GetComponent<deffencemove>().BoundNum += 1;
					}
				}
			}
	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "ballzone"){
			if(strikezone.GetComponent<strikezone> ().judge != "faul"){
				strikezone.GetComponent<strikezone> ().judge = "ball";//ボールゾーンにいったらボール。
			}
		}
		if(deffencemove.GetComponent<deffencemove>().BoundNum == 0){//守備時の当たり判定
			if(collider.gameObject.name == "catcharea_1"){//ノーバウンドでcatchしたらアウト判定にする
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().firstchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "1Bhold";
					Debug.Log("1Bhold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_2"){
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().secondchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "2Bhold";
					Debug.Log("2Bhold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_3"){
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().thirdchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "3Bhold";
					Debug.Log("3Bhold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "gcatcharea_SS"){
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().SSchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "SShold";
					Debug.Log("SShold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_left_right"){
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().rightchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "righthold";
					Debug.Log("righthold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_left_cen"){
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().centerchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "centerhold";
					Debug.Log("centerhold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_left"){
					ballstate = "catch";
					baserun.GetComponent<baserun>().runner0treadbase = "fly";
					deffencemove.GetComponent<deffencemove>().leftchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "lefthold";
					Debug.Log("lefthold");
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
			}else if(deffencemove.GetComponent<deffencemove>().ballDropPoint == "fair"){//バウンドが一回以上でかつフェアゾーンに落ちた場合
				if(collider.gameObject.name == "catcharea_1"){//バウンドしてからcatchしたら各塁に送球する
					deffencemove.GetComponent<deffencemove>().firstchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "1Bhold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_2"){
					deffencemove.GetComponent<deffencemove>().secondchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "2Bhold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_3"){
					deffencemove.GetComponent<deffencemove>().thirdchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "3Bhold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_SS"){
					deffencemove.GetComponent<deffencemove>().SSchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "SShold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_right"){
					deffencemove.GetComponent<deffencemove>().rightchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "righthold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_cen"){
					deffencemove.GetComponent<deffencemove>().centerchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "cenhold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
				if(collider.gameObject.name == "catcharea_left"){
					deffencemove.GetComponent<deffencemove>().leftchase = false;
					deffencemove.GetComponent<deffencemove>().throwing = "lefthold";
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = this.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
			}
	}
}