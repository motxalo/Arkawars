using UnityEngine;
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

	float maxDistToCenter = 8.8f;

	void FixedUpdate(){
		speedModifier = Mathf.Clamp(speedModifier-Time.deltaTime, 0f, 5f);
		UpdateGravity();
		float distanceToCenter = Mathf.Abs((transform.position - gravityCenter.position).magnitude);
		if ( distanceToCenter > maxDistToCenter ){
			transform.position = gravityCenter.position + maxDistToCenter * (transform.position - gravityCenter.position ).normalized;
			dir = gravityVector;
			rb.MovePosition(transform.position + dir *(speed+speedModifier)* Time.deltaTime);
		} else
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
			if(col.collider.GetComponent<powerUpGlue>())
			{
				speed = 0f;
				this.transform.parent = GameObject.Find("PlayerEmpty").transform;
				return;
			}
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

}
