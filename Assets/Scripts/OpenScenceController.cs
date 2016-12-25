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
        Application.LoadLevel(GameObject.Find("SceneManager").GetComponent<MySceneManager>().TRANS0);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
