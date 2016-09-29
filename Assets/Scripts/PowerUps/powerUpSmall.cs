using UnityEngine;
using System.Collections;

public class powerUpSmall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LeanTween.scale(gameObject, new Vector3(transform.localScale.x/2, transform.localScale.y, transform.localScale.z), .5f);
		Invoke ("Remove", 5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Remove() {
		Debug.Log ("se acabo");
        LeanTween.scale(gameObject, new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z), .5f);
        Invoke("Killed",.1f);
	}

    public void RemoveNoLean()
    {
        Debug.Log("se acabo");
        transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
        Invoke("Killed", .1f);
    }


    private void Killed(){
		Destroy(transform.GetComponent<powerUpSmall>());
	}
}
