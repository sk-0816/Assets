using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runner : MonoBehaviour {
	//走者のアニメーション管理

	//runner0 Animator script

	public GameObject Runner1;//runner1
	public GameObject	Runner2;//runner2
	public GameObject Runner3;//runner3

	public Animator animator0;//runner0
	public Animator animator1;//runner1
	public Animator animator2;//runner2
	public Animator animator3;//runner3

	public GameObject baserun;//baserun.cs


	void Start () {
		animator0 = GetComponent<Animator>();
		animator1 = Runner1.GetComponent<Animator>();
		animator2 = Runner2.GetComponent<Animator>();
		animator3 = Runner3.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(baserun.GetComponent<baserun>().runner0intention != "stop"){
			animator0.SetBool("run", true);
		}else{
			animator0.SetBool("run", false);
		}

		if(baserun.GetComponent<baserun>().runner1intention != "stop"){
			animator1.SetBool("run", true);
		}else{
			animator1.SetBool("run", false);
		}

		if(baserun.GetComponent<baserun>().runner2intention != "stop"){
			animator2.SetBool("run", true);
		}else{
			animator2.SetBool("run", false);
		}

		if(baserun.GetComponent<baserun>().runner3intention != "stop"){
			animator3.SetBool("run", true);
		}else{
			animator3.SetBool("run", false);
		}
	}
}
