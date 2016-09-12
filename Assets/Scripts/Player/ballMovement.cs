﻿using UnityEngine;
using System.Collections;

public class ballMovement : MonoBehaviour {

	public int playerId = 0;

	public Transform gravityCenter;
	public float gravity = 1f;


	public LayerMask collisionMask;
	public string tagMask;
	public float speed=2f;
	public float speedModifier =0f;
	public  Vector3 dir ;
	Rigidbody rb;

	public GameObject hitEffect;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		dir = transform.forward;
		dir.z = 0f;
		dir.Normalize();

	}

	private Vector3 gravityVector;
	void UpdateGravity(){
		gravityVector = (gravityCenter.position - transform.position ).normalized;
		gravityVector.z = 0f;
	}

	void FixedUpdate(){
		speedModifier = Mathf.Clamp(speedModifier-Time.deltaTime, 0f, 5f);
		UpdateGravity();
		rb.MovePosition(transform.position + (dir + gravityVector*gravity).normalized*(speed+speedModifier)* Time.deltaTime);
	}

	void OnCollisionEnter (Collision col)
	{
		switch ( col.collider.tag ){
		case "Enemy":
			GameObject tGo = Instantiate(hitEffect) as GameObject;
			tGo.GetComponent<EllipsoidParticleEmitter>().worldVelocity = ( gravityCenter.position - transform.position).normalized;
			tGo.transform.position = transform.position;
			tGo.GetComponent<EllipsoidParticleEmitter>().Emit();
			col.collider.gameObject.SendMessage("DieYouBastard",playerId,SendMessageOptions.DontRequireReceiver);
			break;
		case "Player":
			if(col.collider.GetComponent<stickMovement>().Bumping()){
				SetDirNormal(col.contacts[0].normal);
				speedModifier = 3f;
				return;
			}
			break;
		}
		SetNewDir(col.contacts[0].normal, col.collider.tag == "Player" );
	}

	void SetDirNormal(Vector3 newNormal){
		newNormal.z = 0f;
		newNormal.Normalize();
		dir = newNormal;
	}

	void SetNewDir(Vector3 newNormal, bool isPlayer){
		newNormal.z = 0f;
		newNormal.Normalize();
		dir = Vector3.Reflect(dir,newNormal);
		return;
		if ( isPlayer )
			dir = newNormal;
		else
			dir = Quaternion.Euler(0, 0,  180 + Vector3.Angle(-1 * dir,newNormal))*dir;
	}

	// Update is called once per frame
	void Update () {

		// TE LO QUITO TODO DESDE AQUI
		return;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		/*movimiento*/
		transform.Translate (Vector3.forward * Time.deltaTime * speed);

		/*colision*/
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.forward, Color.green);

		if (Physics.Raycast (ray, out hit, Time.deltaTime * speed + .1f, collisionMask)) {
			Vector3 reflectDir = Vector3.Reflect (ray.direction, hit.normal);
			float rot = -1*( 90 - Mathf.Atan2 (reflectDir.x, reflectDir.y) * Mathf.Rad2Deg);
			transform.eulerAngles = new Vector3 (rot, 90, 0);

			if (hit.transform.tag == tagMask) {
				Destroy (hit.transform.gameObject);
			}
		}
	}



}