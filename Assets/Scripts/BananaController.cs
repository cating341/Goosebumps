﻿using UnityEngine;
using System.Collections;

public class BananaController : MonoBehaviour {

	private bool stepped;
	private GameObject steppedMonster;
	// Use this for initialization
	void Start () {
		stepped = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Monster")
        {
			if (!stepped) {
				stepped = true;
				steppedMonster = other.gameObject;
				steppedMonster.GetComponent<BasicProperties> ().NewDisability ("faint", true);
				steppedMonster.GetComponent<Animator> ().SetBool ("faint", true);
				Invoke ("WakeUp", 3f);
			}
            Debug.Log("Trigger banana event");
        }
    }

	private void WakeUp() {
		steppedMonster.GetComponent<BasicProperties> ().NewDisability ("faint", false);
		steppedMonster.GetComponent<Animator> ().SetBool ("faint", false);
		Destroy (gameObject);
	}
}