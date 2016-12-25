using UnityEngine;
using System.Collections;

public class TransitionController : MonoBehaviour {
	GameObject animateCanvas;
	CompositeText ct;
	// Use this for initialization
	void Start () {
		animateCanvas = GameObject.Find ("AnimateCanvas");
		ct = animateCanvas.GetComponent<CompositeText> ();
		ct.color = Color.white;
		Invoke ("animateString", 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (ct.isCompleted == false) {
				print ("Fire");
				animateString ();
			} else {
				// TBD:
				print("Change to next scene");
				StartGame();

			}
		}
	}

	void animateString() {
		
		ct.DOText ("你好, 我是月薪87, 安安安安安安安安");
	}

	public void StartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("PreviewScene-chap2", UnityEngine.SceneManagement.LoadSceneMode.Single);

	}
}
