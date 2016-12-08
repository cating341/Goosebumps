using UnityEngine;
using System.Collections;

public class TempController : MonoBehaviour {

    public RectTransform tempBar;

    private const float maxTemp = 75.0f;
    private const float minTemp = 1.0f;
    private const float middleTemp = 40.0f;

    private float timer = 0;
    private float currentTemp = middleTemp;
    private int currentState; // 3:hot 2:normal 1:cold

	// Use this for initialization
	void Start () {
        currentState = 2;
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            if (currentState == 3 && currentTemp <= maxTemp) currentTemp ++;
            else if (currentState == 1 && currentTemp >= minTemp) currentTemp --;
            else if (currentState == 2) {
                currentTemp = (currentTemp > middleTemp) ? currentTemp - 1 : currentTemp;
                currentTemp = (currentTemp < middleTemp) ? currentTemp + 1 : currentTemp ;
            }
            timer = 0;
            tempBar.sizeDelta = new Vector2(currentTemp*2, tempBar.sizeDelta.y);
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
