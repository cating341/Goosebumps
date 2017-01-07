using UnityEngine;
using System.Collections; 
public class UnDestroy : MonoBehaviour {

    UnDestroy ins;

    void Awake()
    { 
    }

	// Use this for initialization
	void Start () {
        GameObject.Find("SceneManager").GetComponent<MySceneManager>().addToSceneList(this.gameObject);
        //print(gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
