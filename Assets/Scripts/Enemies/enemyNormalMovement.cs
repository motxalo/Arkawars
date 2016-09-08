using UnityEngine;
using System.Collections;

public class enemyNormalMovement : MonoBehaviour {

    string[] powerupTypes;
	public Transform objetive;
	private Vector3 dir;
	private float speed = .2f;
	// Use this for initialization
	void Start () {
        powerupTypes = new string[] {"A"};
		objetive = GameObject.Find("Planet").transform;
		dir = (objetive.position - transform.position).normalized;
		transform.LookAt(objetive.position);
		enemyController.AddEnemy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += dir*speed*Time.deltaTime;
		// DEBUG
		if ( Vector3.Distance(transform.position, objetive.position) < 1f ){
			Application.LoadLevel(Application.loadedLevel);
			Debug.Log( " GAME OVER ");
		}
	}

	public void DieYouBastard(int playerId){
		enemyController.KillEnemy(gameObject,playerId);
        //Destroy(gameObject);

        //Instantiate(Resources.Load("PowerupsSet/Powerup_" + powerupTypes[Random.Range(0,powerupTypes.Length-1)]) as GameObject,transform.position,transform.rotation);
        Invoke("Killed",.1f);
	}


	private void Killed(){
		Destroy(gameObject);
	}
}
