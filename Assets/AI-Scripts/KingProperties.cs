using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingProperties : MonoBehaviour {

	private GameObject player;
	private BasicProperties basicProperties;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		basicProperties = GetComponent<BasicProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		basicProperties.NavigateMonster (GetTargetPosition());
		Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> ()
			, basicProperties.CheckDisability() || basicProperties.GetAttractions ().Count > 0);
	}

	private Vector3 GetTargetPosition() {
		return basicProperties.GetAttractionPeek () ? basicProperties.GetAttractionPeek ().transform.position : player.transform.position;
	}
}
