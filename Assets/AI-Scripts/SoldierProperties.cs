using UnityEngine;
using System.Collections;

public class SoldierProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject player;
	private BasicProperties basicProperties;
	private float sceneWide = 10.8f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		agent = GetComponent<NavMeshAgent> ();
		basicProperties = GetComponent<BasicProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		basicProperties.NavigateMonster (GetTargetPosition());
		GameObject attraction = basicProperties.GetAttractionPeek ();
		Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> ()
			, basicProperties.CheckDisability() || (attraction && attraction.GetComponent<AIInformation> ().floor == GetComponent<AIInformation> ().floor));
	}

	private Vector3 GetTargetPosition() {
		GameObject attraction = basicProperties.GetAttractionPeek ();
		if (attraction && attraction.GetComponent<AIInformation> ().floor == GetComponent<AIInformation> ().floor) {
			return attraction.transform.position;
		}
		if (Vector3.Distance (new Vector3 (sceneWide, transform.position.y, transform.position.z), transform.position) < 0.3) {
			sceneWide = sceneWide * -1;
		}
		return new Vector3(sceneWide, transform.position.y, transform.position.z);
	}

	public void TurnWay() {
		sceneWide = sceneWide * -1;
	}
}
