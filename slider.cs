using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←必要

public class slider: MonoBehaviour {
//投手の投げる時のパワーゲージのようなもの。

  Slider _slider;
	public GameObject ballmove;
	public GameObject game;//game.cs
	public GameObject sliders;//gameobjectのslider

	int chance = 1;//ゲージを2回以上いじれないようにする。
	void Start () {
		//スライダーを取得する
		_slider = sliders.GetComponent<Slider>();
	}

	float pitchtim = 0.0f;//ゲージの状態(sliderのvalue値)
	void Update () {
		if(game.GetComponent<game> ().mode == "pitching"){
			// 上昇
			if(ballmove.GetComponent<pitchball> ().ballstate == "nohit"){
				pitchtim = 0.0f;
				ballmove.GetComponent<pitchball> ().speed = 500.0f;
				chance = 1;
			}
			if(chance == 1){
				if(Input.GetKey("m")){
					pitchtim += 0.01f;
					ballmove.GetComponent<pitchball> ().speed += 10f;
				}
			}
			if(Input.GetKeyUp("q")){
				chance += 1;
			}

			if(pitchtim > 1.0f) {
			//最大を超えたら0に戻す。
				pitchtim = 0.0f;
				ballmove.GetComponent<pitchball> ().speed = 500.0f;
			}

			//ゲージに値を設定
			_slider.value = pitchtim;
		}
	}
}