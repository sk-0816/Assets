using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSmove : MonoBehaviour {

	public GameObject ball;//pitchball.cs
	public GameObject deffencemove;//deffencemove.cs
	public GameObject SS;//SS
	public float SSspeed;
	public bool cover2B;//塁に向かう
	public GameObject secondmove;

	public GameObject secondbase;//二塁
	public int throwingtiming;//送球までの時間
	int x;//カウント
	public GameObject balllocation;//守備選手が向かうべきところ落下地点
	// Use this for initialization
	void Start () {
		SSspeed = 100f;
		throwingtiming = 100;
		cover2B　= false;
	}

	// Update is called once per frame
	void Update (){
		if(ball.GetComponent<pitchball>().ballstate == "hit"){//バットに当たったら守備をする
			if(deffencemove.GetComponent<deffencemove>().SSchase == true){
				SS.transform.LookAt(balllocation.transform);//ball.transform
				Vector3 velocity = SS.transform.rotation * new Vector3(0, 0 ,SSspeed);//進む
				SS.transform.position += velocity * Time.deltaTime;//進む
				cover2B = false;//塁に向かわなくなる
			}else if(deffencemove.GetComponent<deffencemove>().state2B == false){
				if(secondmove.GetComponent<secondmove>().cover2B == false){//二塁手が塁につかない時だけ
				//ボールを追わなくカバーでまだ二塁に着いないとき
					cover2B = true;
				}else{
					cover2B = false;
				}
			}
		}

		if(deffencemove.GetComponent<deffencemove>().throwing == "SShold"){
			//ボールの加速度　重力を消す
			Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			Debug.Log("SShold");
		}if(cover2B == true){
				Cover2B();
				Debug.Log("cover遊撃");
			}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "base2"){
			cover2B = false;
			deffencemove.GetComponent<deffencemove>().state2B = true;
			SS.transform.position = new Vector3( 0, 45, 430);//塁に着く
			SS.transform.rotation = Quaternion.Euler(0, -180, 0);
		}
	}

	void OnTriggerStay(Collider collider){
		if(collider.gameObject.name == "base2"){
			if(deffencemove.GetComponent<deffencemove>().holder == SS){
				if(deffencemove.GetComponent<deffencemove>().base2judge == "safe"){
					deffencemove.GetComponent<deffencemove>().base2judge = "out";
				}
			}
		}
	}

	void Cover2B(){
		SS.transform.LookAt(secondbase.transform);
		Vector3 velocity = SS.transform.rotation * new Vector3(0, 0 ,SSspeed);//進む
		SS.transform.position += velocity * Time.deltaTime;//進む
	}
	/*
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "ball"){
			ball.GetComponent<pitchball>().ballstate = "catch";
			deffencemove.GetComponent<deffencemove>().SSchase = false;
			deffencemove.GetComponent<deffencemove>().throwing = "SShold";
			Debug.Log("遊撃手が取った");
		}else{
			if(collider.gameObject.name == "ball"){//バウンドしてからcatchしたら各塁に送球する
				deffencemove.GetComponent<deffencemove>().throwing = "SShold";
				deffencemove.GetComponent<deffencemove>().SSchase = false;
			}
		}
	}*/
}