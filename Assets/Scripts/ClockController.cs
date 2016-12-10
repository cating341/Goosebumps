﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ClockController : MonoBehaviour {

    public GameObject ClockCanvas;
    public GameObject ClockVal;
    public GameObject ClockSlider;

    private int timer;
    private bool enable = true;
    private Animator animation;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        ClockCanvas.SetActive(false);
        animation = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        if(ClockCanvas.active)
            ClockVal.GetComponent<Text>().text = ((int)ClockSlider.GetComponent<Slider>().value).ToString();
	}

    public void SettingClockTimer(){
        timer = (int)ClockSlider.GetComponent<Slider>().value;
        enable = false;
        Invoke("ClockRing", timer);
        ClockCanvas.SetActive(false);
    }

    public void OpenCanvas()
    {
        ClockCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        ClockCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && enable)
        {
            OpenCanvas();
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            CloseCanvas();
        }
    }

    void ClockRing() {
        animation.SetBool("ring", true);
        audioSource.Play(); 
        Invoke("StopAnimation", 5.0f);
    }

    void StopAnimation() {
        enable = true;
        audioSource.Stop(); 
        animation.SetBool("ring", false);
    }
}
