using UnityEngine;
using System.Collections;

public class PotionController : MonoBehaviour {
	public float HEAT_TEMP = 70.0f;
	Animator animation;
	// Use this for initialization
	void Start () {
		animation = gameObject.GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("TempHandle").GetComponent<TempController>().GetTemp() > HEAT_TEMP && !animation.GetBool("explode"))
		{
			animation.SetBool("explode", true);  
			Invoke ("DestroyObject", 2.8f);
		}
	}

	void DestroyObject(){

		Destroy (gameObject);
	}
}
