using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour {
	public GameObject[] prefab;
	public float[] distance;
	public float[] intervals;
	GameObject[] grounds;
	// Use this for initialization
	void Start () {
		grounds = GameObject.FindGameObjectsWithTag ("Ground");
		for (int i = 0; i < prefab.Length; i++) {
			StartCoroutine(randomSpawn (i));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator randomSpawn(int index) {
		
		Vector3 pos;

		int groundIndex = Random.Range (0, grounds.Length);
		float minX = grounds [groundIndex].GetComponent<Collider> ().bounds.min.x;
		float maxX = grounds [groundIndex].GetComponent<Collider> ().bounds.max.x;
		float maxY = grounds [groundIndex].GetComponent<Collider> ().bounds.max.y;
		pos = new Vector3 (Random.Range (minX, maxY), maxY + 0.2f, -4.0f);
	
		if (isOverlapInDistance (pos, 5.0f, index)) {
			yield return new WaitForSeconds (0.5f);
			yield return StartCoroutine (randomSpawn (index));
		} else {
			Instantiate (prefab[index], pos, Quaternion.identity);
			yield return new WaitForSeconds (intervals [index]);
			yield return StartCoroutine (randomSpawn (index));
		}
	}

	bool isOverlapInDistance(Vector3 pos, float dis, int index) {
		Collider[] cols = Physics.OverlapSphere (pos, dis);
		foreach (Collider col in cols) {
			if (col.tag == prefab[index].tag) {
				return true;
			}
		}
		return false;
	}
}
