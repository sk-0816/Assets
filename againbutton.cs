using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class againbutton : MonoBehaviour {
//もう一度のボタン
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void SceneLoad() { // MUST public
		Application.LoadLevel("baseball");//baseballというシーンへ
	}
}
