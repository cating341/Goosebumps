using UnityEngine;
using System.Collections; 
public class PlayerUnDestroy : MonoBehaviour {

    public static PlayerUnDestroy ins;

    void Awake()
    {

        if (ins == null)
        {
            ins = this;
            GameObject.DontDestroyOnLoad(gameObject); 
        }
        else if (ins != this)
            Destroy(gameObject);
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
