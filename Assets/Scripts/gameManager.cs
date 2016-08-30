using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	public string launchSecuence = "";
	public float frecuency = 3f;

	int actualEnemy = 0;

	// Use this for initialization
	void Start () {
		Invoke("LaunchEnemySet",.1f);
	}



	// Update is called once per frame
	void LaunchEnemySet () {
		Instantiate ( Resources.Load("EnemySet/EnemySet_"+launchSecuence[actualEnemy]) as GameObject);
		actualEnemy = (actualEnemy+1)%launchSecuence.Length;
		Invoke("LaunchEnemySet",frecuency);
	}
}
