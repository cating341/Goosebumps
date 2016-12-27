using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameSceneAinmation : MonoBehaviour {

    public Animator PanelAni;
    public Animator TextAni;
	// Use this for initialization
	void Start () {
        Invoke("StartCameraMovement", 5.0f);
	}
	
	// Update is called once per frame
	void Update () { 
	}

    public void StartCameraMovement() {
        Camera.main.GetComponent<CameraController>().CameraMovement();
        Invoke("StartTextFadeIn", 8.0f);
    }

    public void StartTextFadeIn()
    {
        PanelAni.SetBool("start", true);
        TextAni.SetBool("start", true);
    }
}
