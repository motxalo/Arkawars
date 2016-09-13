using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MobileStick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	public Transform center;
	public Transform player;
	public Transform circle;
	private stickMovement stMovement;
	public float distance = 20f;

	public int value = 0;

	void Start(){
		stMovement = player.GetComponent<stickMovement>();
	}

	void Update(){
		circle.position = transform.position + (player.transform.position - center.transform.position).normalized * distance;
		if (Pressed()){
			//Debug.Log( "CLICK POINT : "+new Vector3(Input.mousePosition.x, Input.mousePosition.y,center.position.z) + " CENTER : "+transform.position);
			#if UNITY_EDITOR
				float angle = Vector3.Angle(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0f) - transform.position, Vector3.right);
			#else
				float angle = Vector3.Angle(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y,0) - transform.position, Vector3.right);
			#endif
			if(Input.mousePosition.y > transform.position.y) angle = 360f - angle;
			stMovement.MoveAngle(angle);
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		Debug.Log("tocando");
		value = 1;
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		value = 0;
	}

	public int ReturnValue()
	{
		return value;
	}

	public bool Pressed(){
		return value!=0;
	}
}
