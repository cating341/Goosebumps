using UnityEngine;
using System.Collections;

public class TempController : MonoBehaviour {

    public RectTransform tempBar;

    public float maxTemp = 73.0f;
    public float speed = 2;
    private const float minTemp = 2.0f;
    private float middleTemp ;

    private float timer = 0;
    private float currentTemp ;
    private int currentState; // 3:hot 2:normal 1:cold

	// Use this for initialization
	void Start () {
        currentState = 2;
        middleTemp = (maxTemp + minTemp) / 2;
        currentTemp = middleTemp;
        tempBar.sizeDelta = new Vector2(currentTemp * (120 / middleTemp / 2), tempBar.sizeDelta.y);
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
            tempBar.sizeDelta = new Vector2(currentTemp * (120 / middleTemp / 2), tempBar.sizeDelta.y);
        }
        

        
	}

    public void IncreaseTemp() {
        currentState = (currentState == 3) ? 3 : currentState + 1; 
    }

    public void DecreaseTemp()
    {
        currentState = (currentState == 1) ? 1 : currentState - 1;
    }
     
    public float GetTemp()
    {
        return currentTemp;
    }
}
