using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outfieldermove : MonoBehaviour {

	public GameObject ball;//pitchball.cs
	public GameObject balllocation;//ボールのある場所
	public GameObject deffencemove;//deffencemove.cs
	public GameObject center;//中堅
	public GameObject right;//右翼
	public GameObject left;//左翼

	public float rightspeed;
	public float leftspeed;
	public float centerspeed;
	// Use this for initialization
	void Start () {
		centerspeed = 100;
		rightspeed = 100;
		leftspeed = 100;
	}
	
	// Update is called once per frame
	void Update (){
		if(ball.GetComponent<pitchball>().ballstate == "hit"){//バットに当たったら守備をする
			if(deffencemove.GetComponent<deffencemove>().leftchase == true){
				left.transform.LookAt(balllocation.transform);
				Vector3 velocity = left.transform.rotation * new Vector3(0, 0 ,leftspeed);//進む
				left.transform.position += velocity * Time.deltaTime;
			}
			if(deffencemove.GetComponent<deffencemove>().rightchase == true){
				right.transform.LookAt(balllocation.transform);
				Vector3 velocity = right.transform.rotation * new Vector3(0, 0 ,rightspeed);//進む
				right.transform.position += velocity * Time.deltaTime;
			}
			if(deffencemove.GetComponent<deffencemove>().centerchase == true){
				center.transform.LookAt(balllocation.transform);
				Vector3 velocity = center.transform.rotation * new Vector3(0, 0 ,centerspeed);//進む
				center.transform.position += velocity * Time.deltaTime;
			}
			if(deffencemove.GetComponent<deffencemove>().throwing == "lefthold"){
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
			}
			if(deffencemove.GetComponent<deffencemove>().throwing == "righthold"){
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
			}
			if(deffencemove.GetComponent<deffencemove>().throwing == "centerhold"){
					//ボールの加速度　重力を消す
					Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}
}
