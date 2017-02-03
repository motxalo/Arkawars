using UnityEngine;
using System.Collections;

public class powerUpSmall : MonoBehaviour {

	void Start () {
        LeanTween.scale(gameObject, new Vector3(transform.localScale.x/2, transform.localScale.y, transform.localScale.z), .5f);
		Invoke ("Remove", 5f);
	}

	void Update () {

	}

	public void Remove() {
        LeanTween.scale(gameObject, new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z), .5f);
        Invoke("Killed",.1f);
	}

    public void RemoveNoLean()
    {
        transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
        Invoke("Killed", .1f);
    }


    private void Killed(){
		Destroy(transform.GetComponent<powerUpSmall>());
	}
}
