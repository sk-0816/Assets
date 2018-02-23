using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//3Dのカウントを管理するファイル

public class strikezone : MonoBehaviour {
	public int strikecount = 0;//ストライクカウント
	public int ballcount = 0;//ボールカウント
	public int outcount = 0;//アウトカウント
	public string judgetiming = "nopitch";//投げたか投げてないか
	public string judge = "";//ストライクかボール判定

	public GameObject game;//game.cs

	public GameObject strike1;
	public GameObject strike2;

	public GameObject ball1;
	public GameObject ball2;
	public GameObject ball3;

	public GameObject out1;
	public GameObject out2;

	void OnTriggerEnter(Collider collision){//collisionは衝突したオブジェクト情報が入る引数
		if(collision.gameObject.name == "ball"){
			judge = "strike";
		}if(collision.gameObject.name == "meetBat"){
			judge = "strike";
		}//バットに当ててもアウトになってしまうのでバットに当たったかの変数かを取得したい
	}
	// Use this for initialization
	void Start () {
		int strikecount = 0;//ストライクカウント
		int ballcount = 0;//ボールカウント
		int outcount = 0;//アウトカウント
	}
	// Update is called once per frame

	void Update () {
		if(judgetiming != "nopitch"){//投げているかいないか
				if(judge == "strike"){
					strikecount+=1;
				}if(judge == "ball"){
					ballcount+=1;
				}if(judge == "faul"){
					if(strikecount == 2){//ファウルで三振にならないようにする。
					}else{
						strikecount += 1;
					}
				}
				　Invoke("count", 0.0f);
		}
	}
	void count (){
		switch(strikecount){//ストライクカウント
			case	1:
				strike1.GetComponent<Renderer>().material.color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				break;
			case	2:
				strike2.GetComponent<Renderer>().material.color = new Color(1.0f, 0.92f, 0.016f, 1.0f);
				break;
			case	3:
				ballcount=0;
				strikecount=0;
				outcount+=1;
				game.GetComponent<game>().batterchange = true;
				break;
		}switch(ballcount){//ボールカウント
			case 1:
				ball1.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
				break;
			case 2:
				ball2.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
				break;
			case 3:
				ball3.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
				break;
			case 4:
				ballcount=0;
				strikecount=0;
				break;
		}switch(outcount){//アウトカウント
			case	1:
				out1.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				break;
			case	2:
				out2.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
				break;
			case	3:
				ballcount=0;
				strikecount=0;
				break;
		}if(strikecount==0){
			strike1.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			strike2.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

		}if(ballcount==0){
			ball1.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			ball2.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			ball3.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

		}if(outcount==0){
			out1.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			out2.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		}
		judgetiming = "nopitch";
		judge = "";
	}
}