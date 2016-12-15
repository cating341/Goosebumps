using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//disabilities


public class BasicProperties : MonoBehaviour {

	private string ground1name = "Floor";
	private string ground2name = "Floor (1)";
	private string ground3name = "Floor (2)";
	private string ladder1name = "LadderClimbable1";
	private string ladder2name = "LadderClimbable2";

	private List<GameObject> ground;
	private Dictionary<string, bool> disabledList;
	private int floor;
	public int Floor {
		get {
			return this.floor;
		}
		set {
			this.floor = value;
		}
	}

	// Use this for initialization
	void Start () {
		disabledList = new Dictionary<string, bool> ();
		ground = new List<GameObject> ();
		ground.Add (GameObject.Find ("Floor"));
		ground.Add (GameObject.Find ("Floor (1)"));
		ground.Add (GameObject.Find ("Floor (2)"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewDisability(string key, bool value) {
		disabledList[key] = value;
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
