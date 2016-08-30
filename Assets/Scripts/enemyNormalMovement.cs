using UnityEngine;
using System.Collections;

public class enemyNormalMovement : MonoBehaviour {

	public Transform objetive;
	private Vector3 dir;
	public float speed = .1f;
	// Use this for initialization
	void Start () {
		objetive = GameObject.Find("Planet").transform;
		dir = (objetive.position - transform.position).normalized;
		transform.LookAt(objetive.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += dir*speed*Time.deltaTime;
	}

	public void DieYouBastard(){
		Destroy(gameObject);
	}
}
