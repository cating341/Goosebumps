using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ClockController : MonoBehaviour {

    public GameObject ClockCanvas;
    public GameObject ClockVal;
    public GameObject ClockSlider;
    public AudioClip clockAlarm;

    private int timer;
    private bool enable;
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

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && Input.GetKey("z"))
        {
            OpenCanvas();
        }
    }

    void ClockRing() {
        animation.SetBool("ring", true);
        audioSource.PlayOneShot(clockAlarm); 
        Invoke("StopAnimation", 5.0f);
    }

    void StopAnimation() {
        enable = true;
        animation.SetBool("ring", false);
    }
}

