using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal");
		gameObject.transform.position += new Vector3 (x/100, 0, 0);
		animator.SetFloat ( "speed",x);
	}
}
