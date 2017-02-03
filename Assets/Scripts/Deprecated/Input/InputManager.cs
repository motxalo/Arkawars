using UnityEngine;
using System.Collections;

public static class InputManager  {

	private static KeyCode player1MoveLeft 		= KeyCode.LeftArrow;
	private static KeyCode player1MoveRight 	= KeyCode.RightArrow;
    private static KeyCode player1Bump          = KeyCode.Space;
    private static KeyCode player2MoveLeft      = KeyCode.A;
    private static KeyCode player2MoveRight     = KeyCode.D;
    private static KeyCode player2Bump          = KeyCode.Q;


    private static virtualButton mobileMoveLeft1;
	private static virtualButton mobileMoveRight1;
    private static virtualButton mobileMoveLeft2;
    private static virtualButton mobileMoveRight2;
    private static virtualButton mobileBump;

	public static void InitMobileInput(){
		mobileMoveLeft1 = GameObject.Find("MoveL1").GetComponent<virtualButton>();
		mobileMoveRight1 = GameObject.Find("MoveR1").GetComponent<virtualButton>();
        mobileMoveLeft2 = GameObject.Find("MoveL2").GetComponent<virtualButton>();
        mobileMoveRight2 = GameObject.Find("MoveR2").GetComponent<virtualButton>();
        //mobileBump = GameObject.Find("Bump").GetComponent<virtualButton>();
        Debug.Log("MOBILE INPUT INITIALIZED");
	}

	public static bool MoveLeft(int playerId){
		switch ( playerId ){
		case 1: return Input.GetKey(player1MoveLeft) || mobileMoveLeft1.Pressed();
        case 2: return Input.GetKey(player2MoveLeft) || mobileMoveLeft2.Pressed();
		case 3: return false;
		case 4: return false;
		}
		return false;
	}

	public static bool MoveRight(int playerId){
		switch ( playerId ){
		case 1: return Input.GetKey(player1MoveRight) || mobileMoveRight1.Pressed();
		case 2: return Input.GetKey(player2MoveRight) || mobileMoveRight2.Pressed();
        case 3: return false;
		case 4: return false;
		}
		return false;
	}

	public static bool BumpDown(int playerId){
		switch ( playerId ){
		case 1: return Input.GetKeyDown(player1Bump) || mobileBump;
		case 2: return Input.GetKeyDown(player2Bump);
		case 3: return false;
		case 4: return false;
		}
		return false;
	}
}
