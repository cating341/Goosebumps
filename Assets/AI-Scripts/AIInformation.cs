using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIInformation : MonoBehaviour {
    
	private List<GameObject> ground;
    private int floor;
    public int Floor
    {
        get
        {
            return this.floor;
        }
        set
        {
            this.floor = value;
        }
    }

    // Use this for initialization
    void Start()
    {
		ground = new List<GameObject> ();
		ground.Add (GameObject.Find ("Floor"));
		ground.Add (GameObject.Find ("Floor (1)"));
		ground.Add (GameObject.Find ("Floor (2)"));
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
		{
			for (int i = 0; i < 3; i++)
			{
				if (col.gameObject == ground[i])
				{
					Floor = i + 1;
				}
			}
		}
	}

	public void IgnoreGround(bool ignore){
		for (int i = 0; i < ground.Count; i++) {
			Physics.IgnoreCollision (GetComponent<Collider>(), ground [i].GetComponent<Collider>(), ignore);
		}
	}
}
