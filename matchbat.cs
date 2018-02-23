using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchbat : MonoBehaviour {
//使用していない

	float y;
	public GameObject game;//game.cs

	// Use this for initialization
	void Start () {
		y = -30f;
	}

	// Update is called once per frame
	void Update () {
		if(game.GetComponent<game> ().mode == "batting"){
			if(y>-90f){
				if (Input.GetKey("k")) {
					y-=5f;
					transform.rotation = Quaternion.Euler(0f, y, 0f);
				}
			}
			if(y<50f){
				if (Input.GetKey ("i")) {
					y+=5f;
					transform.rotation = Quaternion.Euler(0f, y, 0f);
				}
    	}
		}
	}
}
