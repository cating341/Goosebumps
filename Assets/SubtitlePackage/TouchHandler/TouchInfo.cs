using UnityEngine;
using System.Collections;

public class TouchInfo
{
	public Vector2 touchPos;
	public GameObject touchObject;
	public Touch touch;
	public Touch oriTouch;
	public GameObject oriObject;
	public Vector3 deltaPosition;
	public TouchInfo(Touch touch,Touch oriTouch,GameObject touchObject,GameObject oriObject)
	{
		this.touchPos =Camera.main.ScreenToWorldPoint (touch.position);
		this.oriTouch = oriTouch;
		this.touchObject = touchObject;
		this.touch = touch;
		this.oriObject = oriObject;
		deltaPosition = touch.deltaPosition;
	}
	public TouchInfo(Vector2 pos,GameObject touchObject)
	{
		touchPos = pos;
		this.touchObject = touchObject;
	}
}

