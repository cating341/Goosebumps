using UnityEngine;
using System.Collections;

public class TempInitial : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Canvas mycanvas = GetComponent<Canvas>();
        mycanvas.renderMode = RenderMode.ScreenSpaceCamera;
        mycanvas.worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
