using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countjage : MonoBehaviour {
//2Dのカウント表示
	public GameObject strikezone;//strikezone.csが入っている。
	int strikecount;
	int ballcount;
	int outcount;

	public GameObject strike2D_1;//2Dカウントの表示
	public GameObject strike2D_2;//2Dカウントの表示

	public GameObject ballcount2D_1;//2Dカウントの表示
	public GameObject ballcount2D_2;//2Dカウントの表示
	public GameObject ballcount2D_3;//2Dカウントの表示

	public GameObject outcount2D_1;//2Dカウントの表示
	public GameObject outcount2D_2;//2Dカウントの表示

	string count = "●";
	string nocount = "";
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(strikezone.GetComponent<strikezone> ().judgetiming != "nopitch"){
			strikecount = strikezone.GetComponent<strikezone> ().strikecount;
			ballcount = strikezone.GetComponent<strikezone> ().ballcount;
			outcount = strikezone.GetComponent<strikezone> ().outcount;

			switch(strikecount){
				case 0:
					strike2D_1.GetComponent<Text>().text = nocount;
					strike2D_2.GetComponent<Text>().text = nocount;
					break;
				case 1:
					strike2D_1.GetComponent<Text>().text = count;
					break;
				case 2:
					strike2D_2.GetComponent<Text>().text = count;
					break;
			}
			switch(ballcount){
				case 0:
					ballcount2D_1.GetComponent<Text>().text = nocount;
					ballcount2D_2.GetComponent<Text>().text = nocount;
					ballcount2D_3.GetComponent<Text>().text = nocount;
					break;
				case 1:
					ballcount2D_1.GetComponent<Text>().text = count;
					break;
				case 2:
					ballcount2D_2.GetComponent<Text>().text = count;
					break;
				case 3:
					ballcount2D_1.GetComponent<Text>().text = count;
					break;
			}
			switch(outcount){
				case 0:
					outcount2D_1.GetComponent<Text>().text = nocount;
					outcount2D_2.GetComponent<Text>().text = nocount;
					break;
				case 1:
					outcount2D_1.GetComponent<Text>().text = count;
					break;
				case 2:
					outcount2D_2.GetComponent<Text>().text = count;
					break;
			}
		}
	}
}
