using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour {
	public GameObject prefab;
	GameObject[] grounds;
	// Use this for initialization
	void Start () {
		grounds = GameObject.FindGameObjectsWithTag ("Ground");
		randomSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void randomSpawn() {
		
		Vector3 pos;

		int groundIndex = Random.Range (0, grounds.Length);
		float minX = grounds [groundIndex].GetComponent<Collider> ().bounds.min.x;
		float maxX = grounds [groundIndex].GetComponent<Collider> ().bounds.max.x;
		float maxY = grounds [groundIndex].GetComponent<Collider> ().bounds.max.y;
		pos = new Vector3 (Random.Range (minX, maxY), maxY + 0.05f, -4.0f);
		
		if (isOverlapInDistance (pos, 5.0f)) {
			Invoke("randomSpawn", 1.0f);
		} else {
			Instantiate (prefab, pos, Quaternion.identity);
			Invoke("randomSpawn", 2.0f);
		}
	}

	bool isOverlapInDistance(Vector3 pos, float dis) {
		print ("loop");
		Collider[] cols = Physics.OverlapSphere (pos, dis);
		foreach (Collider col in cols) {
			if (col.tag == prefab.tag) {
				return true;
			}
		}
		return false;
	}
}
