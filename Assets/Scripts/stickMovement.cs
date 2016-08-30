using UnityEngine;
using System.Collections;

public class stickMovement : MonoBehaviour {

	public GameObject target;
	public float vel		 = 2f;
	public float rotateSpeed = 2f;
	public float maxRotation = 15;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow))
			Rotate (1);
		else if (Input.GetKey (KeyCode.RightArrow))
			Rotate (-1);
		if (Input.GetKey (KeyCode.UpArrow)){
			RotatePlayer(1f);
		}else if (Input.GetKey (KeyCode.DownArrow)){
			RotatePlayer(-1f);
		}
		
	}

	void RotatePlayer( float amount ){

		transform.Rotate(new Vector3(0f,0f,amount*rotateSpeed*Time.deltaTime));
	}

	void Rotate(int dir){
		transform.RotateAround (target.transform.position, Vector3.forward, vel*dir*Time.deltaTime);		
	}
}
