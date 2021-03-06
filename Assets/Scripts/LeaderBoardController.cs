﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardController : MonoBehaviour {
	public string url = "http://ballonhouse.com:3000/goosebumps/leaderboard";
	GameController gameController;
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameHandle") != null) {
			gameController = GameObject.Find ("GameHandle").GetComponent<GameController> ();
		} else {
			Debug.Log ("Can't find GameHandle");
		}
	}

	public IEnumerator GetLeaderBoard(int chapter, int difficulty, OnComplete cb) {
		WWW www = new WWW(url + "/" + chapter.ToString() + "/" + difficulty.ToString());
		yield return www;
		if (www.error == null) {
			Debug.Log (www.text);
			LeaderBoard lb = JsonUtility.FromJson<LeaderBoard> (www.text);
			cb (lb);
		} else {
			Debug.Log("ERROR: " + www.error);
		}
	}

	public IEnumerator SendScore (int score, string name, int level, int difficulty, OnComplete callback) {
		WWWForm data = new WWWForm ();
		data.AddField ("score", score);
		data.AddField ("name", name);
		data.AddField ("level", level);
		data.AddField ("difficulty", difficulty);
		WWW wwwPost = new WWW(url, data);
		yield return wwwPost;
		if (wwwPost.error == null) {
//			Debug.Log (wwwPost.text);
			LeaderBoard lb = JsonUtility.FromJson<LeaderBoard> (wwwPost.text);
//			foreach (playerInfo playerInfo in lb.leaderboard) {
//				Debug.Log (playerInfo.name + ": " + playerInfo.score);
//			}
			callback (lb);
			Debug.Log ("My rate: " + lb.myRate);
		} else {
			Debug.Log("ERROR: " + wwwPost.error);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}

public delegate void OnComplete(LeaderBoard lb);
