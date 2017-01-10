using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//disabilities


public class BasicProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject[] grounds;
	private Dictionary<string, bool> disabledList;
	private Queue<GameObject> attractions;
	private float ICE_TEMP = 13.0f;

	private bool iceBreaking;
	private bool iceBroken;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		disabledList = new Dictionary<string, bool> ();
		attractions = new Queue<GameObject> ();
		grounds = GameObject.FindGameObjectsWithTag ("Ground");
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("SoldierMonster"), true);
		iceBreaking = false;
		iceBroken = true;
	}
	
	// Update is called once per frame
	void Update () {
		CheckClimb ();
	}

	public void NavigateMonster(Vector3 tar) {
		if (CheckDisability()) {
			agent.Stop ();
//			agent.SetDestination (agent.transform.position);
		} else {
			agent.Resume ();
			agent.SetDestination (tar);
		}
		GetComponentInChildren<Animator> ().SetFloat ("speed", agent.velocity.sqrMagnitude);
	}

	public void NewDisability(string key, bool value) {
		if (key == "carpet") {
			Invoke ("DestroyMe", 4.0f);
		} else if (key == "water") {
			if ((!disabledList.ContainsKey(key) || !disabledList [key]) && value) {
				GetComponentInChildren<Animator> ().SetBool ("attack", true);
				Invoke ("BreakBathroomIce", 3f);
			}
//			Invoke("Break")
		}
		disabledList[key] = value;
	}

	private void BreakBathroomIce() {
		if (GameObject.Find ("SceneManager").GetComponent<MySceneManager> ().GetLevel () == 1) {
			Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("Water"));
			Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("Icecube"));
		}
		else if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().GetLevel() == 2)
			Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("KingMonster"), LayerMask.NameToLayer ("Water"));
		disabledList ["water"] = false;
		GetComponentInChildren<Animator> ().SetBool ("attack", false);
	}

	private void DestroyMe() {
		Destroy (gameObject);
	}

	public void NewAttraction(GameObject attr)
	{
		attractions.Enqueue(attr);
	}

	public GameObject GetAttractionPeek(){
		if (attractions != null) {
			while (attractions.Count > 0) {
				if (CheckDequeue (attractions.Peek ())) {
					attractions.Dequeue ();
				} else {
					return attractions.Peek ();
				}
			}
		}
		return null;
	}

	private bool CheckDequeue(GameObject peek) {
		if (peek.tag == "Phone") {
			if (!peek.GetComponentInChildren<Animator> ().GetBool ("ring")) {
				return true;
			}
		} else if (peek.name == "TV") {
			if (!peek.GetComponentInChildren<Animator> ().GetBool ("on")) {
				return true;
			}
		} else if (peek.name == "Alarm clock") {
			if (!peek.GetComponentInChildren<Animator> ().GetBool ("ring")) {
				return true;
			}
		}
		return false;
	}

	private void IgnoreGround(bool ignore){
		foreach (GameObject ground in grounds) {
			Physics.IgnoreCollision (GetComponent<Collider>(), ground.GetComponent<Collider>(), ignore);
		}
	}

	private void CheckClimb() {
		OffMeshLinkData ladder = agent.currentOffMeshLinkData;
		if (ladder.activated) {
//			GetComponent<Rigidbody> ().useGravity = false;
			if (GameObject.Find ("TempHandle").GetComponent<TempController> ().GetTemp () < ICE_TEMP && !iceBreaking) {
				GetComponentInChildren<Animator> ().SetBool ("attack", true);
				iceBreaking = true;
				iceBroken = false;
				Invoke ("BreakIce", 3f);
			} else if (iceBroken) {
				transform.eulerAngles = new Vector3(0, 0, 0);
				float upDown = ladder.endPos.y > ladder.startPos.y ? 1f : -1f;
				transform.position = new Vector3 (ladder.endPos.x, transform.position.y + 0.1f * upDown, transform.position.z);
				IgnoreGround (true);
				if (Mathf.Abs (transform.position.y - ladder.startPos.y) > Mathf.Abs (ladder.endPos.y - ladder.startPos.y)) {
					agent.CompleteOffMeshLink ();
					iceBreaking = false;
					IgnoreGround (false);
				}
			}

		}
	}

	private void BreakIce() {
		GetComponentInChildren<Animator> ().SetBool ("attack", false);
		iceBroken = true;
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

	public float GetRemainingDistance() {
		return agent.remainingDistance;
	}

	public void Pause() {
		agent.Stop ();
	}

	public void Resume() {
		agent.Resume ();
	}
}
