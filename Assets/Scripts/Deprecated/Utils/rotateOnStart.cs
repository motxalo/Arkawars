using UnityEngine;
using System.Collections;

public class rotateOnStart : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Transform planet = GameObject.Find("Planet").transform;
		transform.RotateAround(planet.position,Vector3.forward,Random.Range(0f,360f));
	}

}
