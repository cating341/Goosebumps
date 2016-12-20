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
		basicProperties.NavigateMonster (GetTargetPosition());
	}
	
	// Update is called once per frame
	void Update () {
	}

	private Vector3 GetTargetPosition() {
		if (Vector3.Distance (new Vector3 (sceneWide, transform.position.y, transform.position.z), transform.position) < 0.5) {
			sceneWide = sceneWide * -1;
		}
		return new Vector3(sceneWide, transform.position.y, transform.position.z);
	}
}
