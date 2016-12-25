using UnityEngine;
using System.Collections;

public class TransitionController : MonoBehaviour {
	GameObject animateCanvas;
	CompositeText ct;
	// Use this for initialization
	void Start () {
		animateCanvas = GameObject.Find ("AnimateCanvas");
		ct = animateCanvas.GetComponent<CompositeText> ();
		Invoke ("animateString", 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void animateString() {
		ct.color = Color.white;
		ct.DOText ("你好, 我是月薪87, 安安安安安安安安");
	}
}
