using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LeaderBoard {
	public List<playerInfo> leaderboard;
	public int myRate;
}

[System.Serializable]
public struct playerInfo {
	public string name;
	public int score;
}