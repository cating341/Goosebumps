using UnityEngine;
using System.Collections;

public class PreviewGameController : MonoBehaviour {

	GameObject player;
	public GameObject kingMonster;
	public GameObject soldierMonster;
	private bool instantiatedMonster;
	MySceneManager sceneManager;
	// Use this for initialization
	void Start () {
		sceneManager = GameObject.Find ("SceneManager").GetComponent<MySceneManager> ();
        player = GameObject.Find("Player");
		instantiatedMonster = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Application.isLoadingLevel && !instantiatedMonster) {
			instantiatedMonster = true;
			InstantiateMonsters ();
		}



	}

	private void InstantiateMonsters() {
		GameObject obj = (GameObject) Instantiate (kingMonster);
//		Invoke ("InstantiateKingMonster", kingMonsterAppear);
		SoldierPosition[] soldier1Pos = new SoldierPosition[] {
			new SoldierPosition(new Vector3(-7.262046f, -3.47f, -3.735428f), new Vector2(-10.8f, 10.8f)),
			new SoldierPosition(new Vector3(-0.23f, 0.21f, -3.735428f), new Vector2(-10.8f, 10.8f)),
			new SoldierPosition(new Vector3(5.01f, 4.63f, -3.96f), new Vector2(-10.8f, 10.8f))
		};
		SoldierPosition[] soldier2Pos = new SoldierPosition[] {
			new SoldierPosition (new Vector3 (-13.09f, -0.64f, -5.01f), new Vector2 (-13f, 10f)), 
			new SoldierPosition (new Vector3 (-2.56f, 3.68f, -5.01f), new Vector2 (-13f, 13f)),
			new SoldierPosition (new Vector3 (-6.7f, 8.12f, -5.01f), new Vector2 (-13f, 0f))
		};
        int level = sceneManager.GetLevel();
        int difficulty = sceneManager.getDifficulty();
		print (level);
		for (int i = 0; i < difficulty + 1; i++) {
			GameObject newMonster = new GameObject();
			if (level == 1) {
				newMonster = (GameObject)Instantiate (soldierMonster, soldier1Pos [i].instPos, soldierMonster.transform.rotation);
			} else if (level == 2) {
				newMonster = (GameObject)Instantiate (soldierMonster, soldier2Pos [i].instPos, soldierMonster.transform.rotation);
			}
		}
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("KingMonster"));
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("SoldierMonster"));
	}

    public void GameStart()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5.0f);
		player.GetComponent<Character>().EnablePlayerMove();
        if (sceneManager.currentSceneName == sceneManager.PREVIEW1)
            Application.LoadLevel(sceneManager.GAMESCENE1);
        else if (sceneManager.currentSceneName == sceneManager.PREVIEW2)
            Application.LoadLevel(sceneManager.GAMESCENE2);
    }

	// disable monster
	public void disableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().DisablePlayerMove ();
		Invoke ("enableMonster", 5.0f);
	}

	void enableMonster(){
		GameObject.Find ("Player").GetComponent<Character> ().EnablePlayerMove ();
	}
}
