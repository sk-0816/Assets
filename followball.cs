using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followball : MonoBehaviour {
	//ボールの落下地点を表すlocallocationの場所合わせと当たり判定をする。

	public GameObject ball;//ball.cs
	public GameObject deffencemove;//deffencemove.cs

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update (){
		if(ball.GetComponent<pitchball>().ballstate == "hit"){//hitしたら追うようになる。
			this.transform.position = new Vector3(ball.transform.position.x ,40 ,ball.transform.position.z);
			//50の高さでボールの落下点を表続ける。
		}
	}
}
