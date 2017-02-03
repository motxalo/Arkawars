using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class virtualButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

	public int value = 0;

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
