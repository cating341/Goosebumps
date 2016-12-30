using UnityEngine;
using System.Collections;

public class SoldierProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject player;
	private BasicProperties basicProperties;
	public float leftBound;
	public float rightBound;
	private float heading;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		agent = GetComponent<NavMeshAgent> ();
		basicProperties = GetComponent<BasicProperties> ();
		heading = rightBound;
	}
	
	// Update is called once per frame
	void Update () {
		print (transform.position.x);
		basicProperties.NavigateMonster (GetTargetPosition());
		GameObject attraction = basicProperties.GetAttractionPeek ();
		Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> ()
			, basicProperties.CheckDisability() || (attraction && attraction.name != "Player" && attraction.GetComponent<AIInformation> ().floor == GetComponent<AIInformation> ().floor));
	}

	private Vector3 GetTargetPosition() {
		GameObject attraction = basicProperties.GetAttractionPeek ();
		if (attraction && attraction.name != "Player" && attraction.GetComponent<AIInformation> ().floor == GetComponent<AIInformation> ().floor) {
			return attraction.transform.position;
		}
		if (Vector3.Distance (new Vector3 (heading, transform.position.y, transform.position.z), transform.position) < 0.3) {
			if (heading == leftBound)
				heading = rightBound;
			else
				heading = leftBound;
		}
		return new Vector3(heading, transform.position.y, transform.position.z);
	}

	public void TurnWay(){
		if (heading == leftBound)
			heading = rightBound;
		else 
			heading = leftBound;
	}
}
