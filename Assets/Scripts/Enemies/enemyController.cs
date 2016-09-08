using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class enemyController  {

	private static List<GameObject> enemyPool;
	public static int points = -1;

	public static void AddEnemy(GameObject _enemy){
		if(points <0 ) 
			Init();
		enemyPool.Add(_enemy);
	}

	public static void KillEnemy (GameObject _enemy, int killer ){
		points++;
		enemyPool.Remove(_enemy);

	}

	static void Init(){
		enemyPool = new List<GameObject>();
		points = 0;
	}
}
