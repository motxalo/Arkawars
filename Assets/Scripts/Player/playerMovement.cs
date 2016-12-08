using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

	public int playerId = 1;
	public GameObject target;
	public float vel		 = 2f;
	public float rotateSpeed = 2f;
	public float maxRotation = 45;
	private float rotation = 0;
    public virtualLeftJoystick leftJoystick;
    public virtualRightJoystick rightJoystick;
    // Use this for initialization
	private float distanceToCenter;
    void Start () {
		distanceToCenter = Mathf.Abs((transform.position - target.transform.position).magnitude);
	}

	public void MoveAngle(float angle){
		if(bumping!=0) return;
		float oldAngle = Vector3.Angle((transform.position - target.transform.position), Vector3.right);
		if( transform.position.y > target.transform.position.y) oldAngle = 360f - oldAngle;
		float newAngle = Mathf.Lerp(oldAngle, angle, rotateSpeed * Time.deltaTime);
		//Debug.Log("MOVING : "+oldAngle+" TO : "+newAngle+" WANTED : "+angle);

		transform.RotateAround (target.transform.position, Vector3.forward, oldAngle - newAngle);		
	}

	// Update is called once per frame
	void Update () {
		float oldAngle = Vector3.Angle((transform.position - target.transform.position), Vector3.right);
		if( transform.position.y > target.transform.position.y) oldAngle  = 360f - oldAngle;
		//Debug.Log("GRADOS AL CENTRO : "+oldAngle);
		if ( bumping ==1){
			transform.position += transform.up*bumpDistance;

		}else if ( bumping == 2){
			transform.position -= transform.up*bumpDistance;
		}
		else{
        	//TACTIL
			/*
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
            	*/
        //FIN TACTIL

        //TECLADO
			if (InputManager.MoveLeft(playerId))
            	Rotate(1);
			else if (InputManager.MoveRight(playerId))
            	Rotate(-1);

        //FIN TECLADO
			if (InputManager.BumpDown (playerId)) {
                //if
                if(transform.FindChild("Player").transform.GetComponent<powerUpGlue>())
                {
                    GameObject.Find("Ball").GetComponent<ballMovement>().speed = 5f;
                    GameObject.Find("Ball").transform.parent = null;
                    GameObject.Find("Ball").GetComponent<ballMovement>().dir= (GameObject.Find("Crossfire").transform.position - GameObject.Find("Player").transform.position).normalized;
                    /*aqui hay que poner la direccion de la bola hacia la mirilla*/
                }
                else
                    StartCoroutine ("BumpPlayer");
			}
		}
    }

    private int bumping = 0;
	public float bumptime = .2f;
	public float bumpDistance =.01f;
	IEnumerator BumpPlayer(){
		Vector3 tpos = transform.position;
		bumping = 1;
		yield return new WaitForSeconds(bumptime/2);
		bumping = 2;
		yield return new WaitForSeconds(bumptime/2);
		bumping = 0;
		transform.position = tpos;
	}
    
	public bool Bumping(){
		return bumping != 0;
	}

	void BumpPlayer(int dir)
    {		
        float oldrotation = rotation;

        if (dir == 0)
            rotation = Mathf.Clamp(rotation + dir * rotateSpeed * 1000 * Time.deltaTime, -1f * 0f, 0f);
        else
            rotation = Mathf.Clamp(rotation + dir * rotateSpeed * 1000 * Time.deltaTime, -1f * maxRotation, maxRotation);

		transform.FindChild("Player").transform.Rotate(new Vector3(0f, 0f, rotation - oldrotation));
    }

    void RotatePlayer( float amount ){
		float oldrotation = rotation;
		rotation = Mathf.Clamp(rotation + amount*rotateSpeed*Time.deltaTime, -1f*maxRotation,maxRotation);

		transform.Rotate(new Vector3(0f,0f,rotation - oldrotation));
	}

	void BackToNormalRotation(){
		float oldrotation = rotation;
		rotation = Mathf.Lerp(rotation,0f,Time.deltaTime*rotateSpeed);
		transform.Rotate(new Vector3(0f,0f,rotation - oldrotation));
		//transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, Vector3.zero, Time.deltaTime*rotateSpeed));
	}

	void Rotate(int dir){
		transform.RotateAround (target.transform.position, Vector3.forward, vel*dir*Time.deltaTime);		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PowerUp") {
			Destroy (other.gameObject);
		}
	}
}
