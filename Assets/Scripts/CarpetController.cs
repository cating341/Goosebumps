﻿using UnityEngine;
using System.Collections;

public class CarpetController : MonoBehaviour {

    public GameObject Temp;
    public float HEAT_TEMP = 65.0f;
    public AudioClip fireEffect;

    private TempController tempController;
    private Animator anim; 

    int heatCount = 0;
    float timer;
    AudioSource audioSource;

	private GameObject killedMonster;
	// Use this for initialization
	void Start () {
        tempController = Temp.GetComponent<TempController>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1.0f)
        { 
            if (tempController.GetTemp() > HEAT_TEMP)
            {
                anim.SetBool("fire", true);
                audioSource.PlayOneShot(fireEffect, 0.5f);
                heatCount++;
            }
            else
            {
                audioSource.Stop();
                anim.SetBool("fire", false);
            }
            timer = 0;
        }

        if (heatCount > 10)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerStay(Collider other)
    {
		if (other.gameObject.tag == "Monster" && anim.GetBool("fire") && !this.killedMonster)
        {
			GameObject.Find("GameHandle").GetComponent<GameController>().AddPoint(150);
			GameObject.Find ("GameHandle").GetComponent<GameController> ().ReBornMonster (other.gameObject);
			this.killedMonster = other.gameObject;
			this.killedMonster.GetComponent<BasicProperties> ().NewDisability ("carpet", true);
			this.killedMonster.GetComponentInChildren<Animator> ().SetBool ("dead", true);


        }
    }
}
