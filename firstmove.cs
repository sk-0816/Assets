using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstmove : MonoBehaviour {

	public GameObject ball;//pitchball.cs
	public GameObject deffencemove;//deffencemove.cs
	public GameObject first;//first
	public GameObject firstbase;//一塁
	public float firstspeed;
	public bool cover1B;//塁に向かう

	public int throwingtiming;//送球までの時間
	int x;//カウント
	public GameObject balllocation;//守備選手が向かうべきところ落下地点
	// Use this for initialization
	void Start () {
		firstspeed = 100f;
		throwingtiming = 100;
		cover1B　= false;
	}

	// Update is called once per frame
	void Update (){
		if(ball.GetComponent<pitchball>().ballstate == "hit"){//バットに当たったら守備をする
			if(deffencemove.GetComponent<deffencemove>().firstchase == true){
				//50の高さのままボールを追う
				first.transform.LookAt(balllocation.transform);//ball.transform
				Vector3 velocity = first.transform.rotation * new Vector3(0, 0 ,firstspeed);//進む
				first.transform.position += velocity * Time.deltaTime;//進む
				cover1B = false;//塁に向かわなくなる
			}else if(deffencemove.GetComponent<deffencemove>().state1B == false){
				//ボールを追わなくカバーでまだ一塁に着いないとき
				cover1B = true;
			}
		}
		if(deffencemove.GetComponent<deffencemove>().throwing == "1Bhold"){
			Debug.Log("1Bhold");
		}if(cover1B == true){
			Cover1B();
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "base1"){
			cover1B = false;
			deffencemove.GetComponent<deffencemove>().state1B = true;
			first.transform.position = new Vector3( 375, 45, 45);//塁に着く
			first.transform.rotation = Quaternion.Euler(0, 220, 0);
		}
	}
	void OnTriggerStay(Collider collider){
		if(collider.gameObject.name == "base1"){
			if(deffencemove.GetComponent<deffencemove>().holder == first){//この処理ができるかわからない
				if(deffencemove.GetComponent<deffencemove>().base1judge == "safe"){
					deffencemove.GetComponent<deffencemove>().base1judge = "out";
				}
			}
		}
	}
	void Cover1B(){
		first.transform.LookAt(firstbase.transform);
		Vector3 velocity = first.transform.rotation * new Vector3(0, 0 ,firstspeed);//進む
		first.transform.position += velocity * Time.deltaTime;//進む
	}

	/* 
	void OnTriggerEnter(Collider collider){
		if(deffencemove.GetComponent<deffencemove>().BoundNum == 0){//守備時の当たり判定
			if(collider.gameObject.name == "ball"){
				ball.GetComponent<pitchball>().ballstate = "catch";
				deffencemove.GetComponent<deffencemove>().firstchase = false;
				deffencemove.GetComponent<deffencemove>().throwing = "1Bhold";
				Debug.Log("一塁手が取った");
			}
		}else{
			if(collider.gameObject.name == "ball"){//バウンドしてからcatchしたら各塁に送球する
				deffencemove.GetComponent<deffencemove>().throwing = "1Bhold";
				deffencemove.GetComponent<deffencemove>().firstchase = false;
			}
		}
	}*/
}//　lookatで方向を見る → その方向を取得しYの角度だけ0にする → 進ませる
	//this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0, this.transform.rotation.z);
	//指定の角度にする↑