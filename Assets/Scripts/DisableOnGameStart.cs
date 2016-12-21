using UnityEngine;
using System.Collections;

public class DisableOnGameStart : MonoBehaviour {
	private MySceneManager sm;
	// Use this for initialization
	void Start () {
		if (GetComponent<ItemOnObject> ().item.itemName == "Ladder") {
			sm = GameObject.Find ("SceneManager").GetComponent<MySceneManager> ();
		} else {
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (sm.GameSceneIsGame()) {
			Destroy (gameObject);
		}
	}
}
