using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	int status = 0; // 0 : cabecera, 1 : menu, 2 : leaderboard, 3 : niveles 
	bool canTouchThis = false;
	public Vector3 enterPos;
	public Vector3 exitPos;
	public Vector3 centerPos;
	public GameObject[] paneles;

	// Use this for initialization
	void Start () {
		status = 0;
		for (int i = 1; i < paneles.Length; i++){
			paneles[i].transform.position = enterPos;
		}
		Invoke("CanTouchThis",.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!canTouchThis) return;
		switch(status){
		case 0: UpdateMain(); break;
		case 1: UpdateMenu(); break;
		case 2: UpdateLeaderboard(); break;
		case 3: UpdateLevelSelec(); break;
		}
	}

	void UpdateMain(){
		if (Input.GetMouseButtonDown(0)){
			canTouchThis = false;
			status = 1;
			paneles[0].transform.position = centerPos;
			paneles[1].transform.position = enterPos;
			LeanTween.move(paneles[0],exitPos, .5f);
			LeanTween.move(paneles[1],centerPos, .5f);
			Invoke("CanTouchThis",.5f);
		}
	}

	void UpdateMenu(){}

	public void Play(){

		Debug.Log("SELECT LEVEL");
		canTouchThis = false;
		status = 1;
		paneles[1].transform.position = centerPos;
		paneles[3].transform.position = enterPos;
		LeanTween.move(paneles[1],exitPos, .5f);
		LeanTween.move(paneles[3],centerPos, .5f);
		Invoke("CanTouchThis",.5f);
	}

	public void LeaderBoard(){

		Debug.Log("LEADERBOARD");
		canTouchThis = false;
		status = 1;
		paneles[1].transform.position = centerPos;
		paneles[2].transform.position = enterPos;
		LeanTween.move(paneles[1],exitPos, .5f);
		LeanTween.move(paneles[2],centerPos, .5f);
		Invoke("CanTouchThis",.5f);
	}

	void UpdateLeaderboard(){
		if (Input.GetMouseButtonDown(0)){
			canTouchThis = false;
			status = 1;
			paneles[2].transform.position = centerPos;
			paneles[1].transform.position = enterPos;
			LeanTween.move(paneles[2],exitPos, .5f);
			LeanTween.move(paneles[1],centerPos, .5f);
			Invoke("CanTouchThis",.5f);
		}
	}

	void UpdateLevelSelec(){
		Application.LoadLevel(1);
	}

	void CanTouchThis(){
		canTouchThis = true;
	}
}
