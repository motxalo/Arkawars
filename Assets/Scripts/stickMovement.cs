using UnityEngine;
using System.Collections;

public class stickMovement : MonoBehaviour {

	public GameObject target;
	public float vel=2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow))
			rotate (1);
		else if (Input.GetKey (KeyCode.RightArrow))
			rotate (-1);
		
	}

	void rotate(int dir){
		transform.RotateAround (target.transform.position, Vector3.forward, vel*dir*Time.deltaTime);		
	}
}
