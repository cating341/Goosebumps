using UnityEngine;
using System.Collections;

public class TransitionController : MonoBehaviour {
	GameObject animateCanvas;
	CompositeText ct;

    public int thisScene = 1; //0: gamescene0, 1: gamescene1
	// Use this for initialization
	void Start () {
		animateCanvas = GameObject.Find ("AnimateCanvas");
		ct = animateCanvas.GetComponent<CompositeText> ();
		ct.color = Color.white;
		//Invoke ("animateString", 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (ct.isCompleted == false) {
				animateString ();
                gameObject.GetComponent<AudioSource>().Play();
			} else {
				// TBD:
				StartGame();

			}
		}

        if(ct.isCompleted)
            gameObject.GetComponent<AudioSource>().Stop();
	}

	void animateString() {
        if (thisScene == 0)
            ct.DOText("Bump在街道上遇到了魔女, \n魔女欣賞bump可以控制溫度的能力, \n將他的家人變成殭屍, \n藉此逼迫bump成為他的手下, \n為了躲避變成怪物的家人, \nbump逃回了家中.....");
        else if (thisScene == 1)
            ct.DOText("Bump成功將家人關在房子內. \n為了解救被變為殭屍的家人們, \nBump決定前往魔法森林尋找魔女, \n與魔女決一死戰....");
	}

	public void StartGame()
	{
        if (thisScene == 0)
            Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW1); 
        else if (thisScene == 1)
            Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW2); 
	}
}
