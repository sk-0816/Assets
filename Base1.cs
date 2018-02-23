using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base1 : MonoBehaviour {
//1塁の当たり判定
//守備の状態によって走者をstopさせる

public GameObject deffencemove;//deffencemove.cs
public GameObject baserun;//baserun.cs
public GameObject runner0;//走る打者
public GameObject runner1;//もともと1塁にいた走者
public GameObject strikezone;//strikezone.cs

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "Runner1"){
			if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){//すでにthrowingがhold,throwing状態なら
				baserun.GetComponent<baserun>().runner1intention = "stop";//止まる
				runner1.transform.position = new Vector3(370,15,50);

			}if(deffencemove.GetComponent<deffencemove>().throwing == "hit"){//まだ守備がボールを取っていない状態なら
				baserun.GetComponent<baserun>().runner1intention = "toNext";//次の塁を狙う
			}
			baserun.GetComponent<baserun>().runner1treadbase = "1B";

			if(baserun.GetComponent<baserun>().runner1intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().base1judge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().base1judge = "nojudge";
				}
			}
		}
		if(collider.gameObject.name == "Runner0"){
			Debug.Log("1塁踏んだ!");
			if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){//すでにthrowingがhold,throwing状態なら
				baserun.GetComponent<baserun>().runner0intention = "stop";//止まる
				Debug.Log("止まる!");

			}if(deffencemove.GetComponent<deffencemove>().throwing == "hit"){//まだ守備がボールを取っていない状態なら
				baserun.GetComponent<baserun>().runner0intention = "toNext";//次の塁を狙う
				Debug.Log("次の塁を狙う!");
			}
			baserun.GetComponent<baserun>().runner0treadbase = "1B";
			runner0.transform.position = new Vector3(370,15,50);//止める

			if(baserun.GetComponent<baserun>().runner0intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().base1judge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().base1judge = "nojudge";
				}
			}

		}
	}
}
