using UnityEngine;
using System.Collections;


public class pitch : MonoBehaviour {
//投げるモーション

    private Animator animator;
    public GameObject game;//game.cs
    public GameObject ballmove;//ballmove.cs
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        if(game.GetComponent<game> ().mode == "pitching"){
            if(Input.GetKey("m")) {
                this.transform.rotation = Quaternion.Euler(0, -90, 0);
                
                animator.CrossFade("pitch", 0, 0, 0.1f);
                //animator.SetBool("pitch", true);
            }else{
                animator.SetBool("pitch", false);
                this.transform.position = new Vector3(0,13,20);
            }
        }if(game.GetComponent<game> ().mode == "batting"){
            if(ballmove.GetComponent<pitchball> ().ballstate == "pitching"){
                animator.CrossFade("pitch", 0, 0, 0.1f);
                this.transform.position	= new Vector3(0,13,20);
                ballmove.GetComponent<pitchball>().ballstate = "pitched";

                //animator.SetBool("pitch", true);
            }else{
                animator.SetBool("pitch", false);
            }
        }
    }

    //アニメーションの経過によって呼び出される関数

    void backregularposition(){//アニメーションが終わった後に感知されるアニメーション
        //this.transform.rotation = Quaternion.Euler(0, 65, 0); //これをしないとくるくる回る。
        this.transform.position = new Vector3(0,13,20);
    }
    void ballreleasepoint(){
        ballmove.GetComponent<pitchball> ().ballstate = "release";
        ballmove.GetComponent<pitchball> ().pitching();
        Physics.gravity = new Vector3(0f, -5f, 0f);
        //ballmove.GetComponent<pitchball> ().ballstate = "release";
    }

}