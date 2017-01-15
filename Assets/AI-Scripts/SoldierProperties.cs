using UnityEngine;
using System.Collections;

public class SoldierProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject player;
	private BasicProperties basicProperties;
	public float leftBound;
	public float rightBound;
	public float detectPlayerDist = 5f;
	private float heading;
	private bool detect;
	public float detectSpeed = 5f;
	public float normalSpeed = 3.5f;
    public float fixedZ = -3.5f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		agent = GetComponent<NavMeshAgent> ();
		basicProperties = GetComponent<BasicProperties> ();
		heading = rightBound;
		detect = false;
	}
	
	// Update is called once per frame
	void Update () {
		basicProperties.NavigateMonster (GetTargetPosition());
		GameObject attraction = basicProperties.GetAttractionPeek ();
		Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> ()
			, basicProperties.CheckDisability() || (attraction && attraction.name != "Player" && attraction.GetComponent<AIInformation> ().floor == GetComponent<AIInformation> ().floor) || player.GetComponent<Character>().isInvincible);
	}

	private Vector3 GetTargetPosition() {
		GameObject attraction = basicProperties.GetAttractionPeek ();
		if (attraction && attraction.name != "Player" && attraction.GetComponent<AIInformation> ().floor == GetComponent<AIInformation> ().floor) {
			return attraction.transform.position;
		}
		if (Vector3.Distance (new Vector3 (heading, transform.position.y, transform.position.z), transform.position) < 0.3) {
			TurnWay ();
		}
		if (Mathf.Abs (transform.position.x - player.transform.position.x) < detectPlayerDist
		    && Mathf.Abs (transform.position.y - player.transform.position.y) < 3f 
			&& ((heading == rightBound && player.transform.position.x > transform.position.x)
				|| (heading == leftBound && player.transform.position.x < transform.position.x))) {
			if (!detect) {
				transform.FindChild ("Canvas").gameObject.SetActive (true);
				agent.speed = detectSpeed;
			}
			detect = true;
		} else {
			if (detect) {
				transform.FindChild ("Canvas").gameObject.SetActive (false);
				agent.speed = normalSpeed;

			}
			detect = false;
		}
        print("z: " + transform.position.z);
		return new Vector3(heading, transform.position.y, fixedZ);
	}

	public void TurnWay(){
		if (heading == leftBound)
			heading = rightBound;
		else 
			heading = leftBound;
	}
}
