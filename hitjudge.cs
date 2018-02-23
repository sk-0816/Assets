using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitjudge : MonoBehaviour {
//バットの当たり判定(当たった場所、タイミング)から打球を返す
//内野手の動き始めも表す

public GameObject Bat;// バット全体
public GameObject meet;//ミートゾーン
public GameObject core;//芯
public GameObject axis;//バットの回転軸
public GameObject Caxis;//バットの芯 (キャラクターが持っている方の)

public GameObject batteranim;//batteranimation.cs
public GameObject deffencemove;//deffencemove.cs
public GameObject MainCam;//メインカメラ
public GameObject BatterCam;//batterカメラ
public GameObject PitchingCam;//pitchカメラ

public GameObject second;//second
public GameObject SS;//SS

public float swingpower;//スイングの強さ
public GameObject ball;//ボール
public float power_bounceback;//跳ね返る加速度
Rigidbody rigidbody;//ボールのrigidbody
float ballspeed;//来たボールのスピード
float power; //飛ばす力
bool meettim;
public string	Batattribute;//ボールがどこに当たったか
public string swingtiming;//スイングのタイミング

//跳ね返る角度　横
//跳ね返る角度　高さ

bool delay;//遅延
int delaycount;//遅延時間

public Vector3 collisionpos;//バット接触した時点でのボールの場所
public Vector3 bouncedpos;//跳ね返り離れた時点でのボールの場所

//跳ね返るボールの強さ = 当たった時のタイミング * 当たったバットの場所 * power
//飛んでいく角度 = collisionposからbouncedposまでで移動した方向

	// Use this for initialization
	void Start (){
		Batattribute = "nohit";
		swingpower = 0f;
		power = 10f;
		Debug.Log("開始");
	}

	// Update is called once per frame
	void Update () {
		if(delay == true){
			delaycount += 1;
		}
		if(delaycount >= 5){
			addpower();
			delaycount = 0;
			delay = false;
		}
		switch(swingtiming){

			case "toolate":
				swingpower = 50f;
				axis.transform.localRotation = Quaternion.Euler(-Caxis.transform.rotation.x * 150f, 0, 106.5f);//正しい
				//axis.transform.localRotation = Quaternion.Euler(0, 0, 106.5f);
				break;
			case "late":
				swingpower = 100f;
				batteranim.GetComponent<batteranimation>().x += 1f;
 				axis.transform.localRotation = Quaternion.Euler(-Caxis.transform.rotation.x * 150f, 0, 106.5f);//正しい
				//axis.transform.localRotation = Quaternion.Euler(-50f, 0, 106.5f);
				break;
			case "best":
				swingpower = 150f;
				batteranim.GetComponent<batteranimation>().x = 0f; 
				axis.transform.localRotation = Quaternion.Euler(-Caxis.transform.rotation.x * 150f, 0, 106.5f);//正しい
				//axis.transform.localRotation = Quaternion.Euler(0f, 0, 106.5f);
				break;
			case "fast":
				swingpower = 100f;
				batteranim.GetComponent<batteranimation>().x += 1f; 
				axis.transform.localRotation = Quaternion.Euler(-Caxis.transform.rotation.x * 150f, 0, 106.5f);//正しい
				//axis.transform.localRotation = Quaternion.Euler(50f, 0, 106.5f);
				break;
			case "toofast":
				swingpower = 50f;
				batteranim.GetComponent<batteranimation>().x += 0.5f; 
				axis.transform.localRotation = Quaternion.Euler(-Caxis.transform.rotation.x * 150f, 0, 106.5f);//正しい
				//axis.transform.localRotation = Quaternion.Euler(100f, 0, 106.5f);
				break;
			case "swingaway"://空振り
				swingpower = 0f;
				batteranim.GetComponent<batteranimation>().x = 0f; 
				//axis.transform.localRotation = Quaternion.Euler(-360f, 0, 106.5f);
				break;
		}
		if(swingpower <= 0){//タイミングが遅いと当たり判定をなくす
			Bat.GetComponent<MeshCollider>().isTrigger = true;//バット全体
			meet.GetComponent<MeshCollider>().isTrigger = true;//ミートゾーン
			core.GetComponent<MeshCollider>().isTrigger = true;//芯
		}else{
			Bat.GetComponent<MeshCollider>().isTrigger = false;//バット全体
			meet.GetComponent<MeshCollider>().isTrigger = false;//ミートゾーン
			core.GetComponent<MeshCollider>().isTrigger = false;//芯
		}
	}
	void OnCollisionEnter(Collision collision){//衝突時バットのどこと当たったかを検知
		if(collision.gameObject.name == "meetBat"){//バットに当たる
			if(Batattribute == "corezone"||Batattribute == "meetzone"){//すでに他と触れている場合

			}else{
				Batattribute = "meetBat";
				Debug.Log("meetBatに当たる!");
			}
		}if(collision.gameObject.name == "meetzone"){//ミートゾーンに当たる
			if(Batattribute == "corezone"||Batattribute == "meetBat"){//すでに他と触れている場合

			}else{
				Batattribute = "meetzone";
				Debug.Log("meetzoneに当たる!");
			}


		}if(collision.gameObject.name == "corezone"){//芯に当たる
			if(Batattribute == "meetzone"||Batattribute == "meetBat"){//すでに他と触れている場合

			}else{
				Batattribute = "corezone";
				Debug.Log("corezone に当たる");
			}
		}
	}

		//オブジェクトが離れた時
		void OnCollisionExit(Collision collision){//オブジェクトが離れたらボールが跳ね返る。
			//Rigidbody rigidbody = GetComponent<Rigidbody>();//rigidbodyを取得
			//rigidbody.useGravity = true;//使えるようにする。

			if(collision.gameObject.name == "corezone"){//芯から離れた場合
				if(Batattribute == "corezone"){
					power_bounceback = 0;
					collisionpos = this.transform.position;
					bouncebackconversion();
					delay = true;
					Debug.Log("coreから離れた!");

				}

			}if(collision.gameObject.name == "meetzone"){//ミートゾーンから離れた場合
				if(Batattribute == "meetzone"){
					power_bounceback = 0;
					collisionpos = this.transform.position;
					bouncebackconversion();
					delay = true;
					Debug.Log("meetzoneから離れた!");

				}

			}if(collision.gameObject.name == "meetBat"){//バットから離れた場合
				if(Batattribute == "meetBat"){
					power_bounceback = 0;
					collisionpos = this.transform.position;
					bouncebackconversion();
					delay = true;
					Debug.Log("meetBatから離れた!");
				}
			}
		}

		void OnTriggerExit(Collider collider){
			//ボールがどの方向に向かったかで内野手一人追わせるようにする
			if(collider.gameObject.name == "chase1B"){
				if(deffencemove.GetComponent<deffencemove>().secondchase == false || deffencemove.GetComponent<deffencemove>().SSchase == false || deffencemove.GetComponent<deffencemove>().thirdchase == false){
					deffencemove.GetComponent<deffencemove>().firstchase = true;
					Debug.Log("一塁手追い始める");
				}
			}if(collider.gameObject.name == "chase2BorSS"){
				Vector3 ballpos = this.transform.position;
				Vector3 pos2B = second.transform.position;
				Vector3 posSS = SS.transform.position;
				if(Mathf.Abs(pos2B.x -= ballpos.x) <= Mathf.Abs(posSS.x -= ballpos.x)){//ボールがどっち側に側に寄っているか
					if(deffencemove.GetComponent<deffencemove>().firstchase == false || deffencemove.GetComponent<deffencemove>().SSchase == false || deffencemove.GetComponent<deffencemove>().thirdchase == false){
						deffencemove.GetComponent<deffencemove>().secondchase = true;
						Debug.Log("二塁手追い始める");
					}
				}else{
					if(deffencemove.GetComponent<deffencemove>().firstchase == false || deffencemove.GetComponent<deffencemove>().secondchase == false || deffencemove.GetComponent<deffencemove>().thirdchase == false){
						deffencemove.GetComponent<deffencemove>().SSchase = true;
						Debug.Log("遊撃手追い始める");
					}
				}
			}if(collider.gameObject.name == "chase3B"){
				if(deffencemove.GetComponent<deffencemove>().firstchase == false || deffencemove.GetComponent<deffencemove>().secondchase == false || deffencemove.GetComponent<deffencemove>().SSchase == false){
					deffencemove.GetComponent<deffencemove>().thirdchase = true;
					Debug.Log("三塁手追い始める");
				}
			}
		}


		void addpower(){
			Debug.Log("addpowerが呼ばれた!");
			Rigidbody rigidbody = GetComponent<Rigidbody>();//rigidbodyを取得
			rigidbody.useGravity = true;//rigidbodyの使用

			//角度調整
			transform.LookAt(collisionpos);

			//力を加える
			rigidbody.velocity = -transform.forward * (power_bounceback);

		}

		void bouncebackconversion(){
			Debug.Log("bouncebackconversionが呼ばれた!");
			//ボールが跳ね返る力、角度を計算
			switch(Batattribute){//バットのどこに当たっている状態か
				case "corezone":
					power *= 1.0f;
						Debug.Log("coreに当たっていた!");
					break;
				case "meetzone":
					power *= 0.7f;
						Debug.Log("meetに当たっていた!");
					break;
				case "meetBat":
					power *= 0.5f;
						Debug.Log("meetBatに当たっていた!");
					break;
			}
			power_bounceback = (power * swingpower);//power_bouncebackの範囲 0 ~ 750
		}
}