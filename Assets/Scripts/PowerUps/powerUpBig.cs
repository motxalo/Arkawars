using UnityEngine;
using System.Collections;

public class powerUpBig : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(transform.localScale.x*2, transform.localScale.y, transform.localScale.z);
		Invoke ("Destroy", 5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Destroy() {
		Debug.Log ("se acabo");
		transform.localScale = new Vector3(transform.localScale.x/2, transform.localScale.y, transform.localScale.z);
	}
}
