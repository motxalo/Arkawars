using UnityEngine;
using System.Collections;

public class enemyNormalMovement : MonoBehaviour {

	public Transform objetive;
	private Vector3 dir;
	private float speed = .2f;
	// Use this for initialization
	void Start () {
		objetive = GameObject.Find("Planet").transform;
		dir = (objetive.position - transform.position).normalized;
		transform.LookAt(objetive.position);
		enemyController.AddEnemy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += dir*speed*Time.deltaTime;
	}

	public void DieYouBastard(int playerId){
		enemyController.KillEnemy(gameObject,playerId);
		//Destroy(gameObject);
		Invoke("Killed",.1f);
	}


	private void Killed(){
		Destroy(gameObject);
	}
}
