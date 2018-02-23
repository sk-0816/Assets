using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondmove : MonoBehaviour {

	public GameObject ball;//pitchball.cs
	public GameObject deffencemove;//deffencemove.cs
	public GameObject second;//second
	public GameObject secondbase;//二塁
	public float secondspeed;
	public bool cover2B;//塁に向かう

	public int throwingtiming;//送球までの時間
	int x;//カウント
	public GameObject balllocation;//守備選手が向かうべきところ落下地点
	// Use this for initialization
	void Start () {
		secondspeed = 100f;
		throwingtiming = 100;
		cover2B　= false;
	}

	// Update is called once per frame
	void Update(){
		if(ball.GetComponent<pitchball>().ballstate == "hit"){//バットに当たったら守備をする
			if(deffencemove.GetComponent<deffencemove>().secondchase == true){
				second.transform.LookAt(balllocation.transform);//ball.transform
				Vector3 velocity = second.transform.rotation * new Vector3(0, 0 ,secondspeed);//進む
				second.transform.position += velocity * Time.deltaTime;//進む
				cover2B = false;//塁に向かわなくなる
			}else if(deffencemove.GetComponent<deffencemove>().state2B == false){
				//ボールを追わなくカバーでまだ二塁に着いないとき
				cover2B = true;
			}
		}
		if(deffencemove.GetComponent<deffencemove>().throwing == "2Bhold"){
			//ボールの加速度　重力を消す
			Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			Debug.Log("2Bhold");
		}if(cover2B == true){
			Cover2B();
			Debug.Log("cover二塁");
		}
	}


	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "base2"){
			cover2B = false;
			deffencemove.GetComponent<deffencemove>().state2B = true;
			second.transform.position = new Vector3( 0, 45, 430);//塁に着く
			second.transform.rotation = Quaternion.Euler(0, -180, 0);
		}
	}

	void OnTriggerStay(Collider collider){
		if(collider.gameObject.name == "base2"){
			if(deffencemove.GetComponent<deffencemove>().holder == second){
				if(deffencemove.GetComponent<deffencemove>().base2judge == "safe"){
					deffencemove.GetComponent<deffencemove>().base2judge = "out";
				}
			}
		}
	}

	void Cover2B(){
		second.transform.LookAt(secondbase.transform);
		Vector3 velocity = second.transform.rotation * new Vector3(0, 0 ,secondspeed);//進む
		second.transform.position += velocity * Time.deltaTime;//進む
	}
	/*
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "ball"){
			ball.GetComponent<pitchball>().ballstate = "catch";
			deffencemove.GetComponent<deffencemove>().secondchase = false;
			deffencemove.GetComponent<deffencemove>().throwing = "2Bhold";
			Debug.Log("二塁手が取った");
		}else{
			if(collider.gameObject.name == "ball"){//バウンドしてからcatchしたら各塁に送球する
				deffencemove.GetComponent<deffencemove>().throwing = "2Bhold";
				deffencemove.GetComponent<deffencemove>().secondchase = false;
			}
		}
	}*/
}