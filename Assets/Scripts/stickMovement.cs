using UnityEngine;
using System.Collections;

public class stickMovement : MonoBehaviour {

	public GameObject target;
	public float vel		 = 2f;
	public float rotateSpeed = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow))
			rotate (1);
		else if (Input.GetKey (KeyCode.RightArrow))
			rotate (-1);
		if (Input.GetKey (KeyCode.UpArrow)){
			transform.Rotate(new Vector3(0f,0f,rotateSpeed*Time.deltaTime));
		}else if (Input.GetKey (KeyCode.DownArrow)){
			transform.Rotate(new Vector3(0f,0f,-1f*rotateSpeed*Time.deltaTime));
		}
		
	}

	void rotate(int dir){
		transform.RotateAround (target.transform.position, Vector3.forward, vel*dir*Time.deltaTime);		
	}
}
