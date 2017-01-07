using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Basic : MonoBehaviour {


	private Dictionary<string, bool> disabledList;
	private Queue<GameObject> attractions;

	// Use this for initialization
	void Start () {
		disabledList = new Dictionary<string, bool> ();
		attractions = new Queue<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
