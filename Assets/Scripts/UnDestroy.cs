using UnityEngine;
using System.Collections; 
public class UnDestroy : MonoBehaviour {

    UnDestroy ins;

    void Awake()
    {

        //if (ins == null)
        //{
        //    ins = this;
        //    GameObject.DontDestroyOnLoad(gameObject); 
        //}
        //else if (ins != this)
        //    Destroy(gameObject);
        GameObject.Find("SceneManager").GetComponent<MySceneManager>().addToSceneList(this.gameObject);
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
