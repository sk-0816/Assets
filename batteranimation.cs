using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteranimation : MonoBehaviour {
//打者の当たり判定

	public Animator animator;
	// Use this for initialization

	public GameObject Bat;//Bat.cs
	public GameObject base1;//1塁ベース
	public GameObject runner1;//1塁にいるランナー
	public GameObject meetcursor;
	public GameObject axis;//バットの回転軸
	public GameObject game;//game.cs
	public GameObject hitjudge;//hitjudge.cs
	public GameObject baserun;//baserun.cs
	public string timing;

	public bool Zbutton;//Zボタンが押せる状態かそうでないか

	public float x;//バットの傾き

	float runspeed = 3.0f;//走る速度
	// Use this for initialization

	void Start(){
		timing = "swingaway";
		animator = GetComponent<Animator>();
		this.transform.position = new Vector3( -36f, 30f, -355f);
		Zbutton = true;
	}
		// Update is called once per frame
	void Update(){
		Vector3 pos = this.transform.position;

		if(Zbutton == true){
			if(Input.GetKey("z")){
				x = -100;
				axis.transform.localRotation = Quaternion.Euler(0, 0, 0.74f);
				animator.CrossFade("swing", 0, 0, 0.74f);//アニメーションを途中から再生
				this.transform.rotation = Quaternion.Euler(0, 100, 0);
				hitjudge.GetComponent<hitjudge>().swingpower = 0;//Zを押していれば永遠にswingが上がってしまうバグを制御
				//this.transform.position	=	new Vector3( pos.x, 15, -345);
			}else{
				animator.SetBool("swing", false);
				this.transform.position	=	new Vector3( pos.x, 15, -345);
				}
			}
		}
	void LateUpdate(){
		if(game.GetComponent<game> ().mode == "batting"){
			if(transform.position.x < -20){//真ん中から20回
				if(Input.GetKey("right")){
					//meetcursor.transform.position += new Vector3( 1f, 0f, 0f);//-20,-25,-40
					this.transform.position += new Vector3 (1, 0, 0);
				}
			}
			if(transform.position.x > -40){//真ん中から20回
				if(Input.GetKey("left")){
					//meetcursor.transform.position -= new Vector3( 1f, 0f, 0f);
					this.transform.position -= new Vector3 (1, 0, 0);
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider){//collisionは衝突したオブジェクト情報が入る引数
		if(collider.gameObject.name == "base1"){
			animator.SetBool("run", false);
			this.transform.position =  new Vector3 (-25, 2, -345);//次の打者が出てくる。
			runner1.transform.position = new Vector3 (390, 40, 45);//runner1が塁に着く。
		}
	}
	void AnimStart(){
		Zbutton = false;
	}
	void AnimEnd(){
		Zbutton = true;
	}
	void swingtimingtoofast(){
		hitjudge.GetComponent<hitjudge>().swingtiming = "toofast";
		//axis.transform.localRotation = Quaternion.Euler(x, 0, 106.5f);
	}
	void swingtimingfast(){
		hitjudge.GetComponent<hitjudge>().swingtiming = "fast";
		//axis.transform.localRotation = Quaternion.Euler(x, 0, 106.5f);
	}
	void swingtimingbest(){
		hitjudge.GetComponent<hitjudge>().swingtiming = "best";
		//axis.transform.localRotation = Quaternion.Euler(x, 0, 106.5f);
	}
	void swingtiminglate(){
		hitjudge.GetComponent<hitjudge>().swingtiming = "late";
		//axis.transform.localRotation = Quaternion.Euler(x, 0, 106.5f);
	}
	void swingtimingtoolate(){
		hitjudge.GetComponent<hitjudge>().swingtiming = "toolate";
		//axis.transform.localRotation = Quaternion.Euler(x, 0, 106.5f);
	}
	void swingtimingswingaway(){
		hitjudge.GetComponent<hitjudge>().swingtiming = "swingaway";
		//axis.transform.localRotation = Quaternion.Euler(x, 0, 106.5);
	}
}