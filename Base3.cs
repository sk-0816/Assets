using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base3 : MonoBehaviour {
//3塁の当たり判定
//守備の状態によって走者をstopさせる

public GameObject deffencemove;//deffencemove.cs
public GameObject baserun;//baserun.cs
public GameObject runner0;//走る打者
public GameObject runner1;//もともと1塁にいた走者
public GameObject runner2;//もともと2塁にいた走者
public GameObject runner3;//もともと3塁にいた走者
public GameObject strikezone;//strikezone.cs
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "Runner3"){
			if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){//throwingがすでにhold,throwing状態なら
				baserun.GetComponent<baserun>().runner3intention = "stop";//止まる
				runner3.transform.position = new Vector3(-370,15,35);
		}
			if(deffencemove.GetComponent<deffencemove>().throwing == "hit"){//まだ守備がボールを取っていない状態なら
				baserun.GetComponent<baserun>().runner3intention = "toNext";//次の塁を狙う
			}
			baserun.GetComponent<baserun>().runner3treadbase = "3B";
		
		}if(collider.gameObject.name == "Runner2"){
			if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){//throwingがすでにhold,throwing状態なら
				baserun.GetComponent<baserun>().runner2intention = "stop";//止まる
				runner2.transform.position = new Vector3(-370,15,35);

			}
			if(deffencemove.GetComponent<deffencemove>().throwing == "hit"){//まだ守備がボールを取っていない状態なら
				baserun.GetComponent<baserun>().runner2intention = "toNext";//次の塁を狙う
			}
			baserun.GetComponent<baserun>().runner2treadbase = "3B";

			if(baserun.GetComponent<baserun>().runner2intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().base3judge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().base3judge = "nojudge";
				}
			}
		}
		if(collider.gameObject.name == "Runner1"){
			if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){//throwingがすでにhold,throwing状態なら
				baserun.GetComponent<baserun>().runner1intention = "stop";//止まる
				runner1.transform.position = new Vector3(-370,15,35);

			}
			if(deffencemove.GetComponent<deffencemove>().throwing == "hit"){//まだ守備がボールを取っていない状態なら
				baserun.GetComponent<baserun>().runner1intention = "toNext";//次の塁を狙う
			}
			baserun.GetComponent<baserun>().runner1treadbase = "3B";

			if(baserun.GetComponent<baserun>().runner1intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().base3judge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().base3judge = "nojudge";
				}
			}
		}
		if(collider.gameObject.name == "Runner0"){
			if(deffencemove.GetComponent<deffencemove>().throwing != "hit" && deffencemove.GetComponent<deffencemove>().throwing != "nocatch"){//throwingがすでにhold,throwing状態なら
				baserun.GetComponent<baserun>().runner0intention = "stop";//止まる
				runner0.transform.position = new Vector3(-370,15,35);
			}
			if(deffencemove.GetComponent<deffencemove>().throwing == "hit"){//まだ守備がボールを取っていない状態なら
				baserun.GetComponent<baserun>().runner0intention = "toNext";//次の塁を狙う
			}
			baserun.GetComponent<baserun>().runner0treadbase = "3B";

			if(baserun.GetComponent<baserun>().runner0intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().base3judge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().base3judge = "nojudge";
				}
			}
		}
	}
}
