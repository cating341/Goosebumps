﻿using UnityEngine;
using System.Collections;

public class RefrigeratorController : MonoBehaviour {

    public int maxHealth = 3;
    int health ;
    Animator animation;
	private GameObject hittingThis;
	private bool attacking;
	// Use this for initialization
	void Start () {
        health = maxHealth;
        animation = GetComponent<Animator>();
		this.attacking = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage() {
        health--;
        animation.SetBool("hurt", true);
        Invoke("StopAni", 0.5f);
    }

    void StopAni() {
        animation.SetBool("hurt", false); 
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (other.gameObject.tag == "Monster" && !attacking && GameObject.Find("SceneManager").GetComponent<MySceneManager>().GameSceneIsGame())
        {
			attacking = true;
			hittingThis = other.gameObject;
			hittingThis.GetComponent<BasicProperties> ().NewDisability ("refrig", true);
			hittingThis.GetComponentInChildren<Animator> ().SetBool ("attack", true);
			Damage ();
        }
    }

	private void Damage(){
		TakeDamage ();
		Invoke ("Damage", 2f);
	}

	void OnDestroy() {
        if (this.hittingThis)
        {
			hittingThis.GetComponentInChildren<Animator>().SetBool("attack", false);
			hittingThis.GetComponent<BasicProperties> ().NewDisability ("refrig", false);
        }
	}
}
