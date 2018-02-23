using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meetcursor : MonoBehaviour {
//ミートカーソルを動かす。

public GameObject game;//game.cs
public GameObject core;//core
public GameObject grip;//grip
public GameObject axis;//回転軸()

int udlrbtn = 0;// 上下左右
int z = -105;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update (){//axis.transform.rotation.x
		if(game.GetComponent<game> ().mode == "batting"){
			if(this.transform.position.y < 55){
				if(Input.GetKey("up")){
					this.transform.position += new Vector3(0,0.6f,0);//真ん中から25回
					z += 2;
					grip.transform.rotation = Quaternion.Euler(0, 0, z);
				}
			}
			if(this.transform.position.y > 24){
				if(Input.GetKey("down")){
					this.transform.position += new Vector3(0,-0.6f,0);//真ん中から25回
					z -= 2;
					grip.transform.rotation = Quaternion.Euler(0, 0, z);
				}
			}	
			if(this.transform.position.x > -12){
				if(Input.GetKey("left")){
					this.transform.position += new Vector3(-1f,0,0);//真ん中から20回
				}
			}
			if(this.transform.position.x < 12){
				if(Input.GetKey("right")){
					this.transform.position += new Vector3(1f,0,0);//真ん中から20回
				}
			}
			core.transform.position = this.transform.position;
		}
	}
	void LateUpdate(){
		//core.transform.rotation = Quaternion.Euler(30, 0, 10);
	}
}
