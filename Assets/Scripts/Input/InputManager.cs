using UnityEngine;
using System.Collections;

public static class InputManager  {

	private static KeyCode player1MoveLeft 		= KeyCode.LeftArrow;
	private static KeyCode player1MoveRight 	= KeyCode.RightArrow;
	private static KeyCode player1Bump 			= KeyCode.Space;


	private static virtualButton mobileMoveLeft;
	private static virtualButton mobileMoveRight;
	private static virtualButton mobileBump;

	public static void InitMobileInput(){
		mobileMoveLeft = GameObject.Find("MoveL").GetComponent<virtualButton>();
		mobileMoveRight = GameObject.Find("MoveR").GetComponent<virtualButton>();
		//mobileBump = GameObject.Find("Bump").GetComponent<virtualButton>();
		Debug.Log("MOBILE INPUT INITIALIZED");
	}

	public static bool MoveLeft(int playerId){
		switch ( playerId ){
		case 1 : return Input.GetKey(player1MoveLeft) || mobileMoveLeft.Pressed();
		case 2: return false;
		case 3: return false;
		case 4: return false;
		}
		return false;
	}

	public static bool MoveRight(int playerId){
		switch ( playerId ){
		case 1 : return Input.GetKey(player1MoveRight) || mobileMoveRight.Pressed();
		case 2: return false;
		case 3: return false;
		case 4: return false;
		}
		return false;
	}

	public static bool BumpDown(int playerId){
		switch ( playerId ){
		case 1 : return Input.GetKeyDown(player1Bump) || mobileBump;
		case 2: return false;
		case 3: return false;
		case 4: return false;
		}
		return false;
	}
}
