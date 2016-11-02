using UnityEngine;
using System.Collections;

public class powerUpGlue : MonoBehaviour {

    void Start()
    {
        transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
        Invoke("Remove", 15f);
    }

    void Update()
    {

    }

    public void Remove()
    {
        transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        Invoke("Killed", .1f);
    }

    private void Killed()
    {
        Destroy(transform.GetComponent<powerUpGlue>());
		GameObject.Find ("Ball").GetComponent<ballMovement>().speed = 5f;
		GameObject.Find ("Ball").transform.parent = null;
    }
}
