using UnityEngine;
using System.Collections;

public class UseGear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z")) {
            Debug.Log("hello");
        }
	}
}
