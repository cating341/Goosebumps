using UnityEngine;
using System.Collections;

public class ChandelierController : MonoBehaviour {

    private const int MAXHITTIMES = 10;
    int hitting = 0;
    bool enable = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (enable && hitting > MAXHITTIMES)
        {
            gameObject.transform.position -= new Vector3(0, 2.8f, 0);
            enable = false;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit~ " + hitting);
        if (enable && other.gameObject.tag == "Player")
        {
            hitting++;
        }
    }
}
