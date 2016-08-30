using UnityEngine;
using System.Collections;

public class scorerUpdater : MonoBehaviour {

	public UnityEngine.UI.Text points;
	public UnityEngine.UI.Text lifes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		points.text = enemyController.points.ToString("000000");
	}
}
