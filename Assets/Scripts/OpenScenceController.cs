using UnityEngine;
using System.Collections;

public class OpenScenceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PreviewScene-chap2", UnityEngine.SceneManagement.LoadSceneMode.Single);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
