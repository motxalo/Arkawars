using UnityEngine;
using System.Collections;

public class stickMovement : MonoBehaviour {

	public GameObject target;
	public float vel		 = 2f;
	public float rotateSpeed = 2f;
	public float maxRotation = 45;
	private float rotation = 0;
    public VirtualLeftJoystick leftJoystick;
    public VirtualRightJoystick rightJoystick;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (leftJoystick.Horizontal()>0)
            Rotate(-1);
        else if (leftJoystick.Horizontal() < 0)
            Rotate(1);

        if (rightJoystick.Horizontal() > 0)
            RotatePlayer(-1f);
        else if (rightJoystick.Horizontal() < 0)
            RotatePlayer(1f);

<<<<<<< HEAD

        if (aButton.ReturnValue() == 1)
            BumpPlayer(-1);
        else if (bButton.ReturnValue() == 1)
            BumpPlayer(1);
        else
            BumpPlayer(0);
        //FIN TACTIL
=======
        /*if (Input.GetKey (KeyCode.LeftArrow))
            Rotate (1);
        else if (Input.GetKey (KeyCode.RightArrow))
			Rotate (-1);
>>>>>>> parent of c2b3453... controles tactiles mejorados

        if (Input.GetKey (KeyCode.UpArrow)){
			RotatePlayer(1f);
		}else if (Input.GetKey (KeyCode.DownArrow)){
			RotatePlayer(-1f);
		}*/
		
	}

<<<<<<< HEAD

    void RotatePlayer( float amount ){
=======
	void RotatePlayer( float amount ){
>>>>>>> parent of c2b3453... controles tactiles mejorados
		float oldrotation = rotation;
		rotation = Mathf.Clamp(rotation + amount*rotateSpeed*Time.deltaTime, -1f*maxRotation,maxRotation);

		transform.Rotate(new Vector3(0f,0f,rotation - oldrotation));
	}

	void Rotate(int dir){
		transform.RotateAround (target.transform.position, Vector3.forward, vel*dir*Time.deltaTime);		
	}
}
