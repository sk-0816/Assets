using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreboard : MonoBehaviour {
	//得点板の管理・換算

	public GameObject a1;
	public GameObject b1;
	public GameObject a2;
	public GameObject b2;
	public GameObject a3;
	public GameObject b3;
	public GameObject score;
	public GameObject hit;//
	public GameObject game;//game.cs
	public int hits;//ヒットの数
	public string inningpoint;//この回攻撃で入った点数

	// Use this for initialization
	void Start () {
		inningpoint = "0";
	}

	// Update is called once per frame
	void Update () {

		switch(game.GetComponent<game> ().inning){
			case 1:
				if(game.GetComponent<game> ().omoteura == "表"){
				//	a1.GetComponent<Text> ().text = inningpoint; //inningpoint.ToString();
				//	score.GetComponent<Text> ().text = game.GetComponent<game>().myscore.ToString();
				}if(game.GetComponent<game> ().omoteura == "裏"){
				//	b1.GetComponent<Text> ().text = inningpoint;
				//	score.GetComponent<Text> ().text = (game.GetComponent<game>().cpuscore).ToString();
				}
				break;
			case 2:
				if(game.GetComponent<game> ().omoteura == "表"){
				//	a2.GetComponent<Text> ().text = inningpoint;
				//	score.GetComponent<Text> ().text = (game.GetComponent<game>().myscore).ToString();
				}if(game.GetComponent<game> ().omoteura == "裏"){
				//	b2.GetComponent<Text> ().text = inningpoint;
				//	score.GetComponent<Text> ().text = (game.GetComponent<game>().cpuscore).ToString();
				}
				break;
			case 3:
				if(game.GetComponent<game>().omoteura == "表"){
					a3.GetComponent<Text>().text = inningpoint;
					score.GetComponent<Text>().text = (game.GetComponent<game>().myscore).ToString();

				}if(game.GetComponent<game>().omoteura == "裏"){
					b3.GetComponent<Text>().text = inningpoint;
					score.GetComponent<Text>().text = (game.GetComponent<game>().cpuscore).ToString();
				}
				break;
		}
	}
}