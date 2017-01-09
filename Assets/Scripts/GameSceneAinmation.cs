using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameSceneAinmation : MonoBehaviour {

    public Animator PanelAni;
    public Animator TextAni;

	public GameObject BtnAni1;
	public GameObject BtnAni2;

	bool startBtnShow = false;

	GameObject player;
	// Use this for initialization
	void Start () {
        Invoke("StartCameraMovement", 10.0f);
		player = GameObject.Find ("Player");
		Invoke ("StartPlayerMovement", 1.0f);
		player.GetComponent<Character> ().DisablePlayerMove ();
	}
	
	// Update is called once per frame
	void Update () { 
		if(player.GetComponent<CharacterControl>().enableMove)
			player.GetComponent<Character> ().DisablePlayerMove ();

	}

    public void StartCameraMovement() {
        Camera.main.GetComponent<CameraController>().CameraMovement();
        Invoke("StartPanelFadeIn", 10.0f);
		Invoke("StartTextFadeIn", 12.5f);
    }

	public void StartPanelFadeIn()
    {
        PanelAni.SetBool("start", true);
    }

	public void StartTextFadeIn(){
		TextAni.SetBool("start", true);

	}

	void StartPlayerMovement(){
		player.GetComponentInChildren<Animator> ().SetBool ("startEnding", true);

	}

	public void BackToStartScene(){
		GameObject.Find("SceneManager").GetComponent<MySceneManager>().removeAllFromSceceList();
		Application.LoadLevel (GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().START);
	}

	public void Exit()
	{
		Application.Quit();
	}

}

