using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TouchHandler : MonoBehaviour {
	public LayerMask touchMask;
	bool isDown = false;
	private Vector2 lastTouch;
	private GameObject touchedObject;
	public GameObject shootIcon;
	
	
	
	
	//public GameObject particles;
	// Use this for initialization
	//public delegate void OnTouchBeganEvent(TouchInfo TouchInfo);
	//public event OnTouchBeganEvent OnTouchBegan;
	// Update is called once per frame
	void Awake()
	{
		Input.multiTouchEnabled = true;
		
		touchMask = LayerMask.GetMask("UI");
		//touchMask.
	}
	
	void MouseTest(LayerMask layer,Vector3 inputPos)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (inputPos);//Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero,15,layer);

		Vector2 touchPos = pos;
		GameObject tobject = null;
		if(hit)
			tobject = hit.collider.gameObject;
		TouchInfo TouchInfo = new TouchInfo(pos,tobject);
		TouchInfo.deltaPosition = (Vector3)(touchPos-lastTouch);
		if(Input.GetMouseButtonDown(0))
		{
			if(hit)
			{
				touchedObject = hit.collider.gameObject;//recipient.gameObject;
				TouchInfo.oriObject = touchedObject;
				TouchInfo.touchObject.SendMessage("OnTouchBegan",TouchInfo,SendMessageOptions.DontRequireReceiver);
				isDown = true;
				oriMouseDownObj = touchedObject;//save the mouse down gobj;
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(touchedObject){
				TouchInfo.oriObject = oriMouseDownObj;
				oriMouseDownObj.SendMessage("OnTouchEnded",TouchInfo,SendMessageOptions.DontRequireReceiver);
				touchedObject = null;
				isDown = false;
			}
		}
		else
		{
			if(touchedObject)
			{
				
				if(lastTouch.Equals(touchPos))
				{TouchInfo.oriObject = oriMouseDownObj;
					oriMouseDownObj.SendMessage("OnTouchStay",TouchInfo,SendMessageOptions.DontRequireReceiver);}
				else
				{TouchInfo.oriObject = oriMouseDownObj;
					oriMouseDownObj.SendMessage("OnTouchMoved",TouchInfo,SendMessageOptions.DontRequireReceiver);}
			}
			if(hit)
			{
				hit.collider.SendMessage("OnTouchHover",TouchInfo,SendMessageOptions.DontRequireReceiver);
			}
		}
		
		lastTouch = touchPos;
	}
	GameObject testRayHit(Vector2 position)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (position);//Input.mousePosition);
		Collider2D[] hits = Physics2D.OverlapPointAll(pos,touchMask);
		//RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero,15,touchMask);
		//Vector2 touchPos = pos;
		//GameObject tobject = null;
		if(hits!=null)
		{
			//tobject = hit.collider.gameObject;
			return hits[0].gameObject;
		}
		return null;
	}
	TouchInfo testTouchHitObject(Touch touch)
	{
		Touch oriTouch = touch;
		for(int i=0;i<touchBegans.Count;i++)
		{
			if(touchBegans[i].fingerId==touch.fingerId)
			{
				oriTouch = touchBegans[i];
				break;
			}
		}

		GameObject tobject= testRayHit(oriTouch.position);

		if(tobject)
			return new TouchInfo(touch,oriTouch,testRayHit(touch.position),tobject);
		else
		 	return null;
	}
	GameObject oriMouseDownObj;
	List<Touch> touchBegans = new List<Touch>();
	GameObject lastTouchObj;
	void Update () 
	{
		
		//	Camera.main.transform.Translate(new Vector3(Input.GetAxis("Mouse X"),0));
		
		//#if UNITY_EDITOR
		if(Input.GetMouseButton(0)||Input.GetMouseButtonDown(0)||Input.GetMouseButtonUp(0))
		{

			MouseTest(touchMask,Input.mousePosition);
		}
		//#endif
		
		Touch[] mytouches = Input.touches;
		lastTouchObj = null;
		for(int i=0;i<Input.touchCount;i++)
		{
			
			Touch touch = mytouches[i];

			
			//TouchTest(0,touch.position);
			
			TouchInfo TouchInfo = testTouchHitObject(touch);
			
			if(lastTouchObj ==TouchInfo.touchObject)
				continue;
			lastTouchObj = TouchInfo.touchObject;
			
			if(TouchInfo!=null)
			{
				switch(touch.phase)
				{
					case TouchPhase.Began:
							TouchInfo.touchObject.SendMessage("OnTouchBegan",TouchInfo,SendMessageOptions.DontRequireReceiver);
							touchBegans.Add(touch);
							//Debug.Log(TouchInfo.touchObject);
							//Debug.Log(touch.fingerId);
						
					break;
					case TouchPhase.Moved:
						
							TouchInfo.oriObject.SendMessage("OnTouchMoved",TouchInfo,SendMessageOptions.DontRequireReceiver);
					break;
					case TouchPhase.Stationary:
							TouchInfo.oriObject.SendMessage("OnTouchStay",TouchInfo,SendMessageOptions.DontRequireReceiver);
						//	Debug.Log("station");
						//	Debug.Log(TouchInfo.touchObject);
						//	Debug.Log(touch.fingerId);
						
					break;
					case TouchPhase.Ended:
							TouchInfo.oriObject.SendMessage("OnTouchEnded",TouchInfo,SendMessageOptions.DontRequireReceiver);
						//	Debug.Log(touch.fingerId);
							touchBegans.Remove(TouchInfo.oriTouch);
					break;
				}
			}
		}
			
			
		

		
	}
	
	
}
