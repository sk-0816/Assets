using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batmove : MonoBehaviour {
//キャラクターの持つ視覚的なバットの角度を変える。
//軌道を見る
	float l = -93f;

	public float x = 0f;
	public float y = 0f;
	public float z = 0f;


	public GameObject grip;
	public GameObject hand;//手
	public GameObject UpperArm;//腕
	public GameObject meetcursor;//バットをボールに充てるための目安のカーソル
	public GameObject game;//game.cs
	public GameObject Bate1;//Bate1
	public GameObject Bate;//Bate

	private float timeleft;

	// Use this for initialization
	void Start(){

	}
	void Update () {
		if(game.GetComponent<game> ().mode == "batting"){
			if(z <= 50f){
				if(Input.GetKey("up")){
					//x -= 1f;
					z += 2.0f;
					//y -= 0.1f;
				}
			}
			if(z >= -50f){
				if(Input.GetKey("down")){
					//x += 1f;
					z -= 2.0f;
					//y += 0.1f;
				}
			}
		}
	}
	// Update is called once per frame
	void LateUpdate (){//バットの軌道見る

				//だいたい1秒ごとに処理を行う
				if(Input.GetKey("p")){
					timeleft -= Time.deltaTime;
					if(timeleft <= 0.0){
						timeleft = 0.05f;

							//ここに処理
							Instantiate(Bate);
							Bate.transform.position = Bate1.transform.position;
							Bate.transform.rotation = Bate1.transform.rotation;
					}
				}

		//hand.transform.localRotation = Quaternion.Euler(hand.transform.rotation.x, hand.transform.rotation.y + y, hand.transform.rotation.z + z);//localEulerAngles (x = 手首自体が回る,y = 手首の真横の動き,z = 手首の縦の動き
		//hand.transform.rotation = Quaternion.Euler(x, UpperArm.transform.rotation.y, z);//localEulerAngles (縦回り手が回る(x),横回り(y),0)
		grip.transform.localRotation = Quaternion.Euler(0f, 0f, 100f + z);//ローカル座標を固定しバットが回らないようにする。＆バットの入る角度調整(90,0,100)

	}
}