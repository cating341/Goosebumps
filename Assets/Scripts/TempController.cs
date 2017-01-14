using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TempController : MonoBehaviour {

    public RectTransform tempBar;
    public Image tempBackground;
    public Sprite[] tempBackgroundSprite;
    public GameObject hot, cold;

    public float maxTemp = 88.0f;
    public float speed = 2;
    private const float minTemp = 2.0f;
    private float middleTemp ;

    private float timer = 0;
    private float currentTemp ;
    private int currentState; // 3:hot 2:normal 1:cold

    AudioSource se;

    // Use this for initialization
    void Start () {
        currentState = 2;
        tempBackground.sprite = tempBackgroundSprite[1];
        middleTemp = (maxTemp + minTemp) / 2;
        currentTemp = middleTemp;
        tempBar.sizeDelta = new Vector2(currentTemp, tempBar.sizeDelta.y);
        hot = GameObject.FindWithTag("hot");
        cold = GameObject.FindWithTag("cold");

        hot.GetComponent<ParticleSystem>().Stop();
        cold.GetComponent<ParticleSystem>().Stop();

        se = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            if (currentState == 3 && currentTemp <= maxTemp) currentTemp += speed;
            else if (currentState == 1 && currentTemp >= minTemp) currentTemp -= speed;
            else if (currentState == 2) {
                currentTemp = (currentTemp > middleTemp) ? currentTemp - speed : currentTemp;
                currentTemp = (currentTemp < middleTemp) ? currentTemp + speed : currentTemp;
            }
            timer = 0;
            tempBar.sizeDelta = new Vector2(currentTemp, tempBar.sizeDelta.y);
        }
        

        
	}

    void StopRing()
    {
        hot.GetComponent<ParticleSystem>().Stop();
        cold.GetComponent<ParticleSystem>().Stop();
        se.Stop();
    }

    public void IncreaseTemp() {
        currentState = (currentState == 3) ? 3 : currentState + 1;
        tempBackground.sprite = tempBackgroundSprite[currentState-1];
        StopRing();
        hot.GetComponent<ParticleSystem>().Play();
        se.Play();
        Invoke("StopRing", 1.5f);
    }

    public void DecreaseTemp()
    {
        currentState = (currentState == 1) ? 1 : currentState - 1;
        tempBackground.sprite = tempBackgroundSprite[currentState - 1];
        StopRing();
        cold.GetComponent<ParticleSystem>().Play();
        se.Play();
        Invoke("StopRing", 1.5f);
    }
     
    public float GetTemp()
    {
        return currentTemp;
    }
}
