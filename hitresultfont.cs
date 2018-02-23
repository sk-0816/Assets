using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hitresultfont : MonoBehaviour {
//打球が上がった後の結果を文字で知らせる。
	public GameObject game;//game.cs
	public GameObject hitfont;
	Text text;
	public GameObject MainCam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*今はまだ使えない
		if(MainCam){
			switch(game.GetComponent<game> ().howhit){
				case 0:
					hitfont.GetComponent<Text>	().text = "OUT";
					break;
				case 1:
					hitfont.GetComponent<Text>	().text = "SINGLEHIT";
					break;
				case 2:
					hitfont.GetComponent<Text>	().text = "2BASEHIT";
					break;
				case 3:
					hitfont.GetComponent<Text>	().text = "3BASEHIT";
					break;
				case 4:
					hitfont.GetComponent<Text>	().text = "HOMERUN!";
					break;
			}
		} */
	}
}
