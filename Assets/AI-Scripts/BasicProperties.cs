using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//disabilities


public class BasicProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private List<GameObject> ground;
	private Dictionary<string, bool> disabledList;
	private Queue<GameObject> attractions;
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
		agent = GetComponent<NavMeshAgent> ();
		disabledList = new Dictionary<string, bool> ();
		attractions = new Queue<GameObject> ();
		ground = new List<GameObject> ();
		ground.Add (GameObject.Find ("Floor"));
		ground.Add (GameObject.Find ("Floor (1)"));
		ground.Add (GameObject.Find ("Floor (2)"));
	}
	
	// Update is called once per frame
	void Update () {
		CheckClimb ();
	}

	public void NavigateMonster(Vector3 tar) {
		if (CheckDisability()) {
			agent.SetDestination (agent.transform.position);
		} else {
			agent.SetDestination (tar);
		}
		GetComponent<Animator> ().SetFloat ("speed", agent.velocity.sqrMagnitude);
	}

	public void NewDisability(string key, bool value) {
		disabledList[key] = value;
		if (key == "carpet") {
			Invoke ("DestroyMe", 4.0f);
		}
	}

	private void DestroyMe() {
		Destroy (gameObject);
	}

	public void NewAttraction(GameObject attr)
	{
		attractions.Enqueue(attr);
	}

	public GameObject GetAttractionPeek(){
		while (attractions.Count > 0) {
			if (CheckDequeue (attractions.Peek ())) {
				attractions.Dequeue ();
			} else {
				return attractions.Peek ();
			}
		}
		return null;
	}

	private bool CheckDequeue(GameObject peek) {
		if (peek.tag == "Phone")
		{
			if (!peek.GetComponent<Animator>().GetBool("ring"))
			{
				return true;
			}
		}
		else if(peek.name == "TV")
		{
			if (!peek.GetComponent<Animator>().GetBool("on"))
			{
				return true;
			}
		}
		else if(peek.name == "Alarm clock")
		{
			if (!peek.GetComponent<Animator>().GetBool("ring"))
			{
				return true;
			}
		}
		return false;
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

	private void IgnoreGround(bool ignore){
		for (int i = 0; i < ground.Count; i++) {
			Physics.IgnoreCollision (GetComponent<Collider>(), ground [i].GetComponent<Collider>(), ignore);
		}
	}

	private void CheckClimb() {
		OffMeshLinkData ladder = agent.currentOffMeshLinkData;
		if (ladder.activated) {
			GetComponent<Rigidbody> ().useGravity = false;
			transform.eulerAngles = new Vector3(0, 0, 0);
			float upDown = ladder.endPos.y > ladder.startPos.y ? 1f : -1f;
			transform.position = new Vector3 (ladder.endPos.x, transform.position.y + 0.1f * upDown, transform.position.z);
			IgnoreGround (true);
			if (Mathf.Abs (transform.position.y - ladder.startPos.y) > Mathf.Abs (ladder.endPos.y - ladder.startPos.y)) {
				agent.CompleteOffMeshLink ();
				GetComponent<Rigidbody> ().useGravity = true;
				IgnoreGround (false);
			}
		}
	}

	public bool CheckDisability() {
		foreach (KeyValuePair<string, bool> item in disabledList) {
			if (item.Value) {
				return true;
			}
		}
		return false;
	}

	public Queue<GameObject> GetAttractions(){
		return attractions;
	}
}
