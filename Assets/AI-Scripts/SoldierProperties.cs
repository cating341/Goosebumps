using UnityEngine;
using System.Collections;

public class SoldierProperties : MonoBehaviour {

	private NavMeshAgent agent;
	private GameObject player;
	private BasicProperties basicProperties;
	private float leftBound;
	private float rightBound;
    private float yBound;
    private float zBound;
	public float detectPlayerDist = 5f;
	private float heading;
	private bool detect;
	public float detectSpeed = 5f;
	public float normalSpeed = 3.5f;
    public float fixedZ = -3.5f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
        agent = GetComponentInChildren<NavMeshAgent>();
		basicProperties = GetComponent<BasicProperties> ();
//		heading = rightBound;
		detect = false;
	}
	
	// Update is called once per frame
	void Update () {
        print(agent.speed);
        print(GetTargetPosition());
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
//			print (heading);
			TurnWay ();
		}
		if (Mathf.Abs (transform.position.x - player.transform.position.x) < detectPlayerDist
		    && Mathf.Abs (transform.position.y - player.transform.position.y) < 3f 
			&& ((heading == rightBound && player.transform.position.x > transform.position.x)
				|| (heading == leftBound && player.transform.position.x < transform.position.x))) {
			if (!detect) {
				if (GetComponentInChildren<Animator> ()) {
					GetComponentInChildren<Animator> ().SetBool ("isRun", true);
				}
				transform.FindChild ("Canvas").gameObject.SetActive (true);
				agent.speed = detectSpeed;
			}
			detect = true;
		} else {
			if (detect) {
				if (GetComponentInChildren<Animator> ()) {
					GetComponentInChildren<Animator> ().SetBool ("isRun", false);
				}
				transform.FindChild ("Canvas").gameObject.SetActive (false);
				agent.speed = normalSpeed;


			}
			detect = false;
		}
//        print("z: " + transform.position.z);
		return new Vector3(heading, yBound, zBound);
	}

	public void TurnWay(){
		if (heading == leftBound)
			heading = rightBound;
		else 
			heading = leftBound;
	}

    public void InitiateValues(float leftBound, float rightBound, float yBound, float zBound)
    {
        this.leftBound = leftBound;
        this.rightBound = rightBound;
        this.yBound = yBound;
        this.zBound = zBound;
        heading = rightBound;
    }
}
