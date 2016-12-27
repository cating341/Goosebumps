using UnityEngine;
using System.Collections;
using DG.Tweening;

public class WispController : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			col.GetComponent<Character> ().StartWispPossessed (3.0f);
			Destroy (this.gameObject);
		}
	}


}
