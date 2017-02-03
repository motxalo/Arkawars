using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugIAController : MonoBehaviour {
	
	playerController controller;
	Transform ball;
	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball").transform;
		controller = GetComponent<playerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (controller.align) {
		case playerController.blockAlignment.DOWN:
			UpdateDown (); break;
		case playerController.blockAlignment.LEFT:
			UpdateLeft (); break;
		case playerController.blockAlignment.UP:
			UpdateUp (); break;
		case playerController.blockAlignment.RIGHT:
			UpdateRight (); break;
		}
	}

	void UpdateLeft(){
		if (ball.transform.position.y > transform.position.y)
			controller.MoveRight ();
		else
			controller.MoveLeft ();
	}

	void UpdateRight(){
		if (ball.transform.position.y < transform.position.y)
			controller.MoveLeft ();
		else
			controller.MoveRight ();
	}

	void UpdateUp(){

		if (ball.transform.position.x > transform.position.x)
			controller.MoveLeft ();
		else
			controller.MoveRight ();
	}

	void UpdateDown(){

		if (ball.transform.position.x < transform.position.x)
			controller.MoveLeft ();
		else
			controller.MoveRight ();
	}
}
