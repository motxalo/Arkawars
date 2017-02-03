using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	public enum blockAlignment { LEFT, RIGHT, UP, DOWN}
	public blockAlignment align;
	// Use this for initialization
	void Start () {
		actualDisplacement = 0f;
		actualRotation = 0f;
		rotateSpeed = 0f;
		initialPosition = transform.localPosition;
		initialRotation = transform.localRotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		
		UpdateMovement ();
	}

	void UpdateMovement(){
		// Displacement
		movementSpeed = Mathf.Clamp (movementSpeed, -1 * movementMaxSpeed, movementMaxSpeed);
		actualDisplacement = Mathf.Clamp (actualDisplacement + movementSpeed*Time.deltaTime , -movementMargin, movementMargin);

		//transform.localPosition = initialPosition + new Vector3 (actualDisplacement, 0f, 0f);

		// Rotation

		rotateSpeed = Mathf.Clamp (rotateSpeed, -1 * rotateSpeedMax, rotateSpeedMax);
		actualRotation = Mathf.Clamp (actualRotation + rotateSpeed*Time.deltaTime , -rotateMargin, rotateMargin);

		//transform.localRotation = Quaternion.Euler(initialRotation + new Vector3(0f,0f,actualRotation));
		switch (align) {
		case blockAlignment.DOWN:
			transform.localPosition = initialPosition + new Vector3 (actualDisplacement, 0f, 0f);
			transform.localRotation = Quaternion.Euler (initialRotation + new Vector3 (0f, 0f, actualRotation));
			break;
		case blockAlignment.UP:
			transform.localPosition = initialPosition - new Vector3 (actualDisplacement, 0f, 0f);
			transform.localRotation = Quaternion.Euler (initialRotation + new Vector3 (0f, 0f, actualRotation));
			break;
		case blockAlignment.LEFT:
			transform.localPosition = initialPosition + new Vector3 (0f, actualDisplacement, 0f);
			transform.localRotation = Quaternion.Euler (initialRotation - new Vector3 (0f, 0f, actualRotation));
			break;
		case blockAlignment.RIGHT:
			transform.localPosition = initialPosition + new Vector3 (0f, actualDisplacement, 0f);
			transform.localRotation = Quaternion.Euler (initialRotation + new Vector3 (0f, 0f, actualRotation));
			break;
		}
	}

	private Vector3 initialPosition; // x axis
	public float movementMargin = 5f;
	public float actualDisplacement = 0f;
	public float movementSpeed = 0f;
	public float movementMaxSpeed = 2f;
	public float movementAcceleration = 1f;

	public void MoveLeft (){
		float modifier = (movementSpeed > 0f) ? 3f : 1f;
		movementSpeed -= movementAcceleration * Time.deltaTime * modifier;
		RotateLeft ();
	}

	public void MoveRight(){
		float modifier = (movementSpeed < 0f) ? 3f : 1f;
		movementSpeed += movementAcceleration * Time.deltaTime * modifier;
		RotateRight ();
	}

	public float decceleration = 10f;

	public void Deccelerate(){
		if (movementSpeed > 0f)
			movementSpeed = Mathf.Clamp (movementSpeed - decceleration * Time.deltaTime, 0f, movementMaxSpeed);
		else if (movementSpeed < 0f)
			movementSpeed = Mathf.Clamp (movementSpeed + decceleration * Time.deltaTime, -movementMaxSpeed, 0f );
		RotateBack ();
	}	

	public Vector3 initialRotation ; //Z axis
	public float rotateMargin = 45f;
	public float actualRotation = 0f;
	public float rotateSpeedMax = 10f;
	public float rotateAcceleration = 5f;
	public float rotateSpeed = 0f;

	void RotateLeft(){
		float modifier = (rotateSpeed < 0f) ? 40f : 1f;
		rotateSpeed += rotateAcceleration * Time.deltaTime * modifier;
	}

	void RotateRight(){
		float modifier = (rotateSpeed > 0f) ? 40f : 1f;
		rotateSpeed -= rotateAcceleration * Time.deltaTime * modifier;
	}

	public float rotateBacAcceleration = 100f;

	void RotateBack(){
		if (actualRotation > 0f)
			rotateSpeed = Mathf.Clamp (rotateSpeed - rotateBacAcceleration * Time.deltaTime, -rotateSpeedMax, 0f ); 
		else if (actualRotation < 0f)
			rotateSpeed = Mathf.Clamp (rotateSpeed + rotateBacAcceleration * Time.deltaTime,0f, rotateSpeedMax);
	}
}
