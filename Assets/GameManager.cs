using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject character;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        print(GameObject.Find("Player").transform.position);
	}
}
