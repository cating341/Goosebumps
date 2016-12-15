using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject player;
	private BasicProperties basicProperties;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		basicProperties = GetComponent<BasicProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (player.transform.position);
		CheckClimb ();
	}

	private void CheckClimb() {
		OffMeshLinkData ladder = agent.currentOffMeshLinkData;
		if (ladder.activated) {
			GetComponent<Rigidbody> ().useGravity = false;
			float upDown = ladder.endPos.y > ladder.startPos.y ? 1f : -1f;
			transform.position += new Vector3 (0f, 0.1f * upDown, 0f);
			basicProperties.IgnoreGround (true);
//			print (ladder.endPos.y - ladder.startPos.y);
		}
	}

	private int CheckUpDown() {
		if (basicProperties.Floor > player.GetComponent<AIInformation> ().Floor) {
			return -1;
		} else if (basicProperties.Floor < player.GetComponent<AIInformation> ().Floor) {
			return 1;
		} else {
			return 0;
		}
	}
}
