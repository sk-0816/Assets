using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitconversion : MonoBehaviour {
	//打球がどこへ行ったのか
	public GameObject strikezone;//strikezone
	public GameObject game;//game.cs



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision){
		if(game.GetComponent<game> ().gamestate == "conversion"){
			if(collision.gameObject.name == "fairzone"){
				if(Random.Range(0,10+1) >= 0.5){
					game.GetComponent<game> ().howhit = 3;
				}else if(Random.Range(0,10+1) >= 5){
					game.GetComponent<game> ().howhit = 2;
				}else{
					game.GetComponent<game> ().howhit = 1;
				}
			}if(collision.gameObject.name == "Infieldzone"){
				if(Random.Range(0,100+1)>= 2){
					game.GetComponent<game> ().howhit = 0;
				}else{//エラー
					game.GetComponent<game> ().howhit = 1;
				}
			}if(collision.gameObject.name == "HOMERUNzone"){
				game.GetComponent<game> ().howhit = 4;
			}if(collision.gameObject.name == "Outzone (1B)" || collision.gameObject.name == "Outzone (2B)" || collision.gameObject.name == "Outzone (SS)" || collision.gameObject.name == "Outzone (3B)" || collision.gameObject.name == "Outzone(cen)" || collision.gameObject.name == "Outzone (left)" || collision.gameObject.name == "Outzone (right)"){
				game.GetComponent<game> ().howhit = 0;
				strikezone.GetComponent<strikezone> ().outcount += 1;
			}
		}
	}
}
