using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemove : MonoBehaviour {

float speed;
public Vector3 ballvelocity;//ボールの加速度
int ballchange;//変化球の種類

	// Use this for initialization
	void Start (){
		speed = 500.0f;

	}

	void Update() {
		switch(ballchange){
			case 1://ストレート
				this.GetComponent<Rigidbody>().velocity = transform.forward * 0.999f;
				break;
			case 2://スライダー
				this.GetComponent<Rigidbody>().velocity = transform.forward * 0.995f;
				this.GetComponent<Rigidbody>().velocity = -transform.right * 1.2f;
				//rigidbody.AddForce(Vector3.left * (slider), ForceMode.Force);
				break;
			case 3://フォーク
				this.GetComponent<Rigidbody>().velocity = transform.forward * 0.99f;//ボールの空気抵抗による加速度の変化
				this.GetComponent<Rigidbody>().velocity = -transform.up * 1.2f;
				break;
			case 4://カーブ
				this.GetComponent<Rigidbody>().velocity = transform.forward * 0.99f;//ボールの空気抵抗による加速度の変化
				this.GetComponent<Rigidbody>().velocity = -transform.right * 1.2f;
				break;
			}

		if(Input.GetKey("d")){//球種選択
				ballchange += 1;
			if(ballchange > 3){
				ballchange = 1;
			}
		}if(Input.GetKey("n")){
				Rigidbody rigidbody = GetComponent<Rigidbody>();
				rigidbody.useGravity = true;//rigitcomponentを使うようにする。
				rigidbody.velocity = Vector3.zero;//加速度をリセット
				rigidbody.angularVelocity = Vector3.zero;
				this.transform.position = new Vector3 (6900, 45, 2000);

				switch(ballchange){
					case 1://ストレート
						this.GetComponent<Rigidbody>().velocity = transform.forward * (speed);
						break;

					case 2://スライダー
						this.GetComponent<Rigidbody>().velocity = transform.forward * (speed/1.2f);
						this.GetComponent<Rigidbody>().velocity = transform.right * 15f;
						break;

					case 3://フォーク
						this.GetComponent<Rigidbody>().velocity = transform.forward * (speed/1.3f);
						break;
					case 4://カーブ
						this.GetComponent<Rigidbody>().velocity = transform.forward * (speed/1.3f);
						this.GetComponent<Rigidbody>().velocity = transform.up * 15f;
						this.GetComponent<Rigidbody>().velocity = transform.right * 20f;
						break;
				}

		}if(Input.GetKey("k")){
			Debug.Log ("速度ベクトル: " + this.GetComponent<Rigidbody>().velocity);
		}
	}
}