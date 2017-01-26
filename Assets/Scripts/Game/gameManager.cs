using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	public string launchSecuence = "";
	public float frecuency = 3f;

	int actualEnemy = 0;

	void Awake(){
	//InputManager.InitMobileInput();
	}

	// Use this for initialization
	void Start () {
		//Invoke("LaunchEnemySet",.1f);
	}



	// Update is called once per frame
	void LaunchEnemySet () {
		Instantiate ( Resources.Load("EnemySet/EnemySet_"+launchSecuence[actualEnemy]) as GameObject);
		if(actualEnemy >= launchSecuence.Length)
			frecuency *= .8f;
		actualEnemy = (actualEnemy+1)%launchSecuence.Length;
		Invoke("LaunchEnemySet",frecuency);
	}
}
