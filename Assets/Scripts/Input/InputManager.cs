using UnityEngine;
using System.Collections;

public static class InputManager  {

	private static KeyCode player1MoveLeft 		= KeyCode.LeftArrow;
	private static KeyCode player1MoveRight 	= KeyCode.RightArrow;
	private static KeyCode player1RotateRight 	= KeyCode.UpArrow;
	private static KeyCode player1RotateLeft 	= KeyCode.DownArrow;
	private static KeyCode player1Bump 			= KeyCode.Space;


	private static virtualButton mobileMoveLeft;
	private static virtualButton mobileMoveRight;
	private static virtualButton mobileRotateLeft;
	private static virtualButton mobileRotateRight;
	private static virtualButton mobileBumpL;
	private static virtualButton mobileBumpR;

	public static void InitMobileInput(){
		mobileMoveLeft 	= GameObject.Find("MoveL").GetComponent<virtualButton>();
		mobileMoveRight = GameObject.Find("MoveR").GetComponent<virtualButton>();
		mobileRotateLeft 	= GameObject.Find("RotL").GetComponent<virtualButton>();
		mobileRotateRight 	= GameObject.Find("RotR").GetComponent<virtualButton>();
		mobileBumpR = GameObject.Find("BumpR").GetComponent<virtualButton>();
		mobileBumpL = GameObject.Find("BumpL").GetComponent<virtualButton>();
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

	public static bool RotateLeft(int playerId){
		switch ( playerId ){
		case 1 : return Input.GetKey(player1RotateLeft) || mobileRotateLeft.Pressed();
		case 2: return false;
		case 3: return false;
		case 4: return false;
		}
		return false;
	}

	public static bool RotateRight(int playerId){
		switch ( playerId ){
		case 1 : return Input.GetKey(player1RotateRight) || mobileRotateRight.Pressed();
		case 2: return false;
		case 3: return false;
		case 4: return false;
		}
		return false;
	}

	public static bool BumpDown(int playerId){
		switch ( playerId ){
		case 1 : return Input.GetKeyDown(player1Bump) || mobileBumpL.Pressed() || mobileBumpR.Pressed();
		case 2: return false;
		case 3: return false;
		case 4: return false;
		}
		return false;
	}
}
