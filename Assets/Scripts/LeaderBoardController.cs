using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardController : MonoBehaviour {
	public string url = "http://ballonhouse.com:3000/goosebumps/leaderboard";

	// Use this for initialization
	void Start () {
		StartCoroutine (SendScore (10, "an"));
	}

	IEnumerator GetLeaderBoard() {
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null) {
			Debug.Log (www.text);		
		} else {
			Debug.Log("ERROR: " + www.error);
		}
	}

	IEnumerator SendScore (int score, string name) {
		WWWForm data = new WWWForm ();
		data.AddField ("score", score);
		data.AddField ("name", name);
		WWW wwwPost = new WWW(url, data);
		yield return wwwPost;
		if (wwwPost.error == null) {
			Debug.Log (wwwPost.text);
			LeaderBoard lb = JsonUtility.FromJson<LeaderBoard> (wwwPost.text);
			foreach (playerInfo playerInfo in lb.leaderboard) {
				Debug.Log (playerInfo.name + ": " + playerInfo.score);
			}
			Debug.Log ("My rate: " + lb.myRate);
		} else {
			Debug.Log("ERROR: " + wwwPost.error);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
