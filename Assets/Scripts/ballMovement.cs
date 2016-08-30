using UnityEngine;
using System.Collections;

public class ballMovement : MonoBehaviour {

	public LayerMask collisionMask;
	public string tagMask;
	public float speed=2f;

	public  Vector3 dir ;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		dir = transform.forward;
		dir.z = 0f;
		dir.Normalize();

	}

	void FixedUpdate(){
		rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
	}

	void OnCollisionEnter (Collision col)
	{
		SetNewDir(col.contacts[0].normal, col.collider.tag == "Player" );
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
