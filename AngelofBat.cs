using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelofBat : MonoBehaviour {
//バットの角度(当たり判定)の遷移をするオブジェクト

public GameObject axis;//バットの回転軸(キャラクターの持っているもの)
public float x;
public float g;//重力


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		/* ローカルを使って回転
		if(Input.GetKey("l")){
			x += 2;
			this.transform.localRotation = Quaternion.Euler(x, 0, 100);
		}if(Input.GetKey("m")){
			x -= 2;
			this.transform.localRotation = Quaternion.Euler(x, 0, 100);
		}*/


		//if(){//当たり判定のある時のみ実行axis.transform.rotation;   axis.transform.rotate.x
		//this.transform.rotation = Quaternion.Euler(axis.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
		//}
	}
}
