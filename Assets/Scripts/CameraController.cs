using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public GameObject[] SplineOriPos;
	private float[] gamescene1 = {-4.58f, 3.53f, -0.32f, 3.3f};
	private float[] gamescene2 = {-7.17f, 5.49f, -0.32f, 6.0f};
    private float[] gamescene3 = { -12.25001f, 12.160069f, 3.0f, 10.0f };
	float[] thisGameScece;

    bool movementEnable = false;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
		//thisGameScece = gamescene1;
        //Invoke("CameraMovement", 5.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (thisGameScece != gamescene2 && Camera.main.orthographicSize <= 8.0f && movementEnable)
        {
            Camera.main.orthographicSize += 0.01f;
            //Debug.Log(gameObject.transform.position);
            thisGameScece = new float[] { -3.4f, -3.4f, 6.67354f, 6.67354f };
        }
        else 
        {
            if (player.transform.position.x <= thisGameScece[0])
                gameObject.transform.position = new Vector3(thisGameScece[0], gameObject.transform.position.y, -15.97f);
            else if (player.transform.position.x >= thisGameScece[1])
                gameObject.transform.position = new Vector3(thisGameScece[1], gameObject.transform.position.y, -15.97f);
            else
                gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, -15.97f);

            if (player.transform.position.y <= thisGameScece[2])
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, thisGameScece[2], -15.97f);
            else if (player.transform.position.y >= thisGameScece[3])
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, thisGameScece[3], -15.97f);
            else
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, -15.97f);
        }
        
	}

    public void CameraMovement() {
        gameObject.GetComponentInChildren<SplineController>().SplineRoot = GameObject.Find("Spline Root");
        movementEnable = true;
        for (int i = 0; i < SplineOriPos.Length;i ++ )
            SplineOriPos[i].gameObject.transform.position = gameObject.transform.position; 

        gameObject.GetComponentInChildren<SplineController>().enabled = true;
    }

	public void undateCameraParameters(string i){
		Debug.Log (i.ToString () == GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW2);
        if (i == GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW1 || i == GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE1)
			thisGameScece = gamescene1;
        else if (i == GameObject.Find("SceneManager").GetComponent<MySceneManager>().PREVIEW2 || i == GameObject.Find("SceneManager").GetComponent<MySceneManager>().GAMESCENE2)
			thisGameScece = gamescene2;
        else  
            thisGameScece = gamescene3;
	}
}
