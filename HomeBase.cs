using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour {
//本塁の当たり判定
//点を加算

public GameObject game;//game.cs
public GameObject baserun;//baserun.cs
public GameObject strikezone;//strikezone.cs
public GameObject deffencemove;//deffencemove.cs
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider){//走者が本塁に当たる = 点を入れる
		if(collider.gameObject.name == "Runner3"){
			if(game.GetComponent<game>().mode == "pitching"){//cpu側が攻め
				game.GetComponent<game>().cpuscore += 1;
			}else if(game.GetComponent<game>().mode == "batting"){//プレイヤー側が攻撃
				game.GetComponent<game>().myscore += 1;
			}
			baserun.GetComponent<baserun>().runner3treadbase = "Home";
			if(baserun.GetComponent<baserun>().runner3intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().homebasejudge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().homebasejudge = "nojudge";
				}
			}
		}if(collider.gameObject.name == "Runner2"){
			if(game.GetComponent<game>().mode == "pitching"){//cpu側が攻め
				game.GetComponent<game>().cpuscore += 1;
			}else if(game.GetComponent<game>().mode == "batting"){//プレイヤー側が攻撃
				game.GetComponent<game>().myscore += 1;
			}
			baserun.GetComponent<baserun>().runner2treadbase = "Home";
			if(baserun.GetComponent<baserun>().runner2intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().homebasejudge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().homebasejudge = "nojudge";
				}
			}
		}if(collider.gameObject.name == "Runner1"){
			if(game.GetComponent<game>().mode == "pitching"){//cpu側が攻め
				game.GetComponent<game>().cpuscore += 1;
			}else if(game.GetComponent<game>().mode == "batting"){//プレイヤー側が攻撃
				game.GetComponent<game>().myscore += 1;
			}
			baserun.GetComponent<baserun>().runner1treadbase = "Home";
			if(baserun.GetComponent<baserun>().runner1intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().homebasejudge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().homebasejudge = "nojudge";
				}
			}
		}
		if(collider.gameObject.name == "Runner0"){
			if(game.GetComponent<game>().mode == "pitching"){//cpu側が攻め
				game.GetComponent<game>().cpuscore += 1;
			}else if(game.GetComponent<game>().mode == "batting"){//プレイヤー側が攻撃
				game.GetComponent<game>().myscore += 1;
			}
			baserun.GetComponent<baserun>().runner0treadbase = "Home";
			if(baserun.GetComponent<baserun>().runner0intention == "stop"){
				if(deffencemove.GetComponent<deffencemove>().homebasejudge == "out"){
					strikezone.GetComponent<strikezone>().outcount += 1;
					deffencemove.GetComponent<deffencemove>().homebasejudge = "nojudge";
				}
			}
		}
	}
}
