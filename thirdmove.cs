using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdmove : MonoBehaviour {

	public GameObject ball;//pitchball.cs
	public GameObject deffencemove;//deffencemove.cs
	public GameObject third;//third
	public GameObject thirdbase;//三塁
	public bool cover3B;//塁に向かう

	public float thirdspeed;
	public int throwingtiming;//送球までの時間
	int x;//カウント
	public GameObject balllocation;//守備選手が向かうべきところ落下地点
	// Use this for initialization
	void Start () {
		thirdspeed = 100f;
		throwingtiming = 100;
		cover3B　= false;
	}

	// Update is called once per frame
	void Update (){
		if(ball.GetComponent<pitchball>().ballstate == "hit"){//バットに当たったら守備をする
			if(deffencemove.GetComponent<deffencemove>().thirdchase == true){
				third.transform.LookAt(balllocation.transform);//ball.transform
				Vector3 velocity = third.transform.rotation * new Vector3(0, 0 ,thirdspeed);//進む
				third.transform.position += velocity * Time.deltaTime;//進む
				cover3B = false;//塁に向かわなくなる
			}else if(deffencemove.GetComponent<deffencemove>().state3B == false){
				//ボールを追わなくカバーでまだ三塁に着いないとき
				cover3B = true;//塁に向かう
			}
		}
		if(deffencemove.GetComponent<deffencemove>().throwing == "3Bhold"){
			Debug.Log("3Bhold");
		}if(cover3B == true){
			Cover3B();
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "base3"){
			cover3B = false;
			deffencemove.GetComponent<deffencemove>().state3B = true;
			Debug.Log("三塁に着いた");
			third.transform.position = new Vector3( -375, 45, 45);//塁に着く
			third.transform.rotation = Quaternion.Euler(0, -220, 0);
		}
	}

	void OnTriggerStay(Collider collider){
		if(collider.gameObject.name == "base3"){
			if(deffencemove.GetComponent<deffencemove>().holder == third){
				if(deffencemove.GetComponent<deffencemove>().base3judge == "safe"){
					deffencemove.GetComponent<deffencemove>().base3judge = "out";
				}
			}
		}
	}

	void Cover3B(){
		third.transform.LookAt(thirdbase.transform);
		Vector3 velocity = third.transform.rotation * new Vector3(0, 0 ,thirdspeed);//進む
		third.transform.position += velocity * Time.deltaTime;//進む
	}
	/*
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "ball"){
			ball.GetComponent<pitchball>().ballstate = "catch";
			deffencemove.GetComponent<deffencemove>().thirdchase = false;
			deffencemove.GetComponent<deffencemove>().throwing = "3Bhold";
			Debug.Log("三塁手が取った");
		}else{
			if(collider.gameObject.name == "ball"){//バウンドしてからcatchしたら各塁に送球する
				deffencemove.GetComponent<deffencemove>().throwing = "3Bhold";
				deffencemove.GetComponent<deffencemove>().thirdchase = false;
			}
		}
	}*/
}