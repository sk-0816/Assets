using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwing : MonoBehaviour {
//変数　throwingがhold状態になってから

	public GameObject first;//一塁手
	public GameObject second;//二塁手
	public GameObject SS;//遊撃手
	public GameObject third;//三塁手
	public GameObject right;//right
	public GameObject center;//center
	public GameObject left;//left
	public GameObject C;//C
	public GameObject deffencemove;//deffencemove.cs
	public GameObject ball;//ball

	public GameObject firstbase;//1B
	public GameObject secondbase;//2B
	public GameObject thirdbase;//3B
	public GameObject homebase;//homebase
 	public GameObject throwingtobase;//投げるべき塁

	public Rigidbody ballrigidbody;//ボールのrigidbody

	int throwload;//送球にかかる負荷
	int x;//増やしていくカウント
	public string DirectionofThrowing; //投げる方向
	public float armpower;//送球の速さ
	public bool catchtiming;//取った瞬間ならtrueになる
	// Use this for initialization
	void Start () {
		throwload = 50;
		armpower = 8f;
		DirectionofThrowing　= "1B";
		catchtiming = false;
		ballrigidbody = ball.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update (){
		switch(deffencemove.GetComponent<deffencemove>().throwing){
			case "1Bhold":
				ballrigidbody.useGravity = false;
				ball.transform.position = new Vector3(first.transform.position.x,first.transform.position.y,first.transform.position.z);
				x+=1;
				Debug.Log("1B持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;
			case "2Bhold":
				ballrigidbody.useGravity = false;
				ball.transform.position = new Vector3(second.transform.position.x,second.transform.position.y,second.transform.position.z);
				x+=1;
				Debug.Log("2B持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;
			case "SShold":
				ballrigidbody.useGravity = false;
				ball.transform.position = new Vector3(SS.transform.position.x,SS.transform.position.y,SS.transform.position.z);
				x+=1;
				Debug.Log("SS持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;
			case "3Bhold":
				ballrigidbody.useGravity = false;
				ball.transform.position = new Vector3(third.transform.position.x,third.transform.position.y,third.transform.position.z);
				x+=1;
				Debug.Log("3B持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;
			case "righthold":
				ball.transform.position = new Vector3(right.transform.position.x,right.transform.position.y,right.transform.position.z);
				x+=1;
				Debug.Log("right持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;
			case "centerhold":
				ballrigidbody.useGravity = false;
				ball.transform.position = new Vector3(center.transform.position.x,center.transform.position.y,center.transform.position.z);
				x+=1;
				Debug.Log("center持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;	
			case "lefthold":
				ballrigidbody.useGravity = false;
				ball.transform.position = new Vector3(left.transform.position.x,left.transform.position.y,left.transform.position.z);
				x+=1;
				Debug.Log("left持ち変える");
				deffencemove.GetComponent<deffencemove>().throwing = "throw";
				break;
			case "Chold":
				break;
		}

		if(catchtiming == true){
			switch(DirectionofThrowing){
				case "1B":
					throwingtobase = firstbase;
					break;
				case "2B":
					throwingtobase = secondbase;
					break;
				case "3B":
					throwingtobase = thirdbase;
					break;
				case "HB":
					throwingtobase = homebase;
					break;
			}
			float disholder = Vector3.Distance(deffencemove.GetComponent<deffencemove>().holder.transform.position,throwingtobase.transform.position);
			float dis1B = Vector3.Distance(first.transform.position,throwingtobase.transform.position);
			float dis2B = Vector3.Distance(second.transform.position,throwingtobase.transform.position);
			float disSS = Vector3.Distance(SS.transform.position,throwingtobase.transform.position);
			float dis3B = Vector3.Distance(third.transform.position,throwingtobase.transform.position);

//これらを昇順にソートし並び替える
//disholder <= それぞれの距離全て ,が成り立てば投げずにRuntoBase()が呼び出される

			if(disholder < 100){//投げるべき塁までの距離が
				RuntoBase();
			}else{
				if(x > 0){
						x+=1;
					if(x > throwload){
						//投げるアニメーションをつけ走らせる。アニメーションの中にイベント関数を入れ,それが起こった時に投げられる。
						deffencemove.GetComponent<deffencemove>().throwing = "throw";
						ThrowtoBase();
						x = 0;
					}
				}
			}
			catchtiming = false;
		}
	}
	void ThrowtoBase(){
		ballrigidbody.useGravity = true;
		switch(DirectionofThrowing){//投げる方向
			case "1B":
				Vector3 Firstbase = new Vector3(firstbase.transform.position.x,firstbase.transform.position.y,firstbase.transform.position.z);
				ball.transform.LookAt(Firstbase);
				Debug.Log("1塁に向かって");
				ballrigidbody.velocity = transform.forward * (armpower * 100f);
				Debug.Log("投げた");
				break;
			case "2B":
				Vector3 Secondbase = new Vector3(secondbase.transform.position.x,secondbase.transform.position.y,secondbase.transform.position.z);
				ball.transform.LookAt(Secondbase);
				ballrigidbody.velocity = transform.forward * (armpower * 100f);
				Debug.Log("投げた");
				break;
			case "3B":
				Vector3 Thirdbase = new Vector3(thirdbase.transform.position.x,thirdbase.transform.position.y,thirdbase.transform.position.z);
				ball.transform.LookAt(Thirdbase);
				ballrigidbody.velocity = transform.forward * (armpower * 100f);
				Debug.Log("投げた");
				break;
			case "Home":
				Vector3 Homebase = new Vector3(homebase.transform.position.x,homebase.transform.position.y,homebase.transform.position.z);
				ball.transform.LookAt(Homebase);
				ballrigidbody.velocity = transform.forward * (armpower * 100f);
				Debug.Log("投げた");
				break;
		}
	}
	void RuntoBase(){

	}
}