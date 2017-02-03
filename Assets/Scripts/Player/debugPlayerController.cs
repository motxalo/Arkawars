using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugPlayerController : MonoBehaviour {
	playerController tempController;

	// Use this for initialization
	void Start () {
		tempController = GetComponent<playerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//DEBUG
		if (Input.GetKey (KeyCode.LeftArrow)) {
			tempController.MoveLeft ();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			tempController.MoveRight ();
		} else {
			tempController.Deccelerate ();
		}
	}
}
