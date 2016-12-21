using UnityEngine;
using System.Collections;

public class SoldierProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject player;
	private BasicProperties basicProperties;
	private float sceneWide = 10.1f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		agent = GetComponent<NavMeshAgent> ();
		basicProperties = GetComponent<BasicProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		basicProperties.NavigateMonster (GetTargetPosition());
	}

	private Vector3 GetTargetPosition() {
		GameObject attraction = basicProperties.GetAttractionPeek ();
		if (attraction && attraction.GetComponent<AIInformation> ().Floor == GetComponent<AIInformation> ().Floor) {
			return attraction.transform.position;
		}
		if (Vector3.Distance (new Vector3 (sceneWide, transform.position.y, transform.position.z), transform.position) < 0.3) {
			sceneWide = sceneWide * -1;
		}
		return new Vector3(sceneWide, transform.position.y, transform.position.z);
	}
}
