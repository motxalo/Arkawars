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
    public virtualButton aButton;
    public virtualButton bButton;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //TACTIL
        if (leftJoystick.Horizontal()>0)
            Rotate(-1);
        else if (leftJoystick.Horizontal() < 0)
            Rotate(1);

        if (rightJoystick.Horizontal() > 0)
            RotatePlayer(-1f);
        else if (rightJoystick.Horizontal() < 0)
            RotatePlayer(1f);

        if (aButton.ReturnValue() == 1)
            BumpPlayer(-1);
        else if (bButton.ReturnValue() == 1)
            BumpPlayer(1);
        else if (aButton.ReturnValue() == 0 || bButton.ReturnValue() == 0)
            BumpPlayer(0);
        //FIN TACTIL

        //TECLADO
        if (Input.GetKey(KeyCode.LeftArrow))
            Rotate(1);
        else if (Input.GetKey(KeyCode.RightArrow))
            Rotate(-1);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotatePlayer(1f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotatePlayer(-1f);
        }

        if (Input.GetKeyDown(KeyCode.Q)){
            BumpPlayer(-1);
        }else if (Input.GetKeyDown(KeyCode.E)){
            BumpPlayer(1);
        }

        if(Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E)){
            BumpPlayer(0);
        }
        //FIN TECLADO


    }

    void BumpPlayer(int dir)
    {
        float oldrotation = rotation;

        if (dir == 0)
            rotation = Mathf.Clamp(rotation + dir * rotateSpeed * 1000 * Time.deltaTime, -1f * 0f, 0f);
        else
            rotation = Mathf.Clamp(rotation + dir * rotateSpeed * 1000 * Time.deltaTime, -1f * maxRotation, maxRotation);

        transform.Rotate(new Vector3(0f, 0f, rotation - oldrotation));
    }

    void RotatePlayer( float amount ){
		float oldrotation = rotation;
		rotation = Mathf.Clamp(rotation + amount*rotateSpeed*Time.deltaTime, -1f*maxRotation,maxRotation);

		transform.Rotate(new Vector3(0f,0f,rotation - oldrotation));
	}

	void Rotate(int dir){
		transform.RotateAround (target.transform.position, Vector3.forward, vel*dir*Time.deltaTime);		
	}
}
