using UnityEngine;
using System.Collections;

public class ballMovement : MonoBehaviour {

	public LayerMask collisionMask;
	public string tagMask;
	public float speed=2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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
