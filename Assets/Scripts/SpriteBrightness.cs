using UnityEngine;
using System.Collections; 
public class SpriteBrightness : MonoBehaviour {

    GameObject player;
    MySceneManager sceneManager;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        sceneManager = GameObject.Find("SceneManager").GetComponent<MySceneManager>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (gameObject && player && sceneManager.isNightmare)
        {
            float y = player.transform.position.y - gameObject.transform.position.y;
            if (sceneManager.GetLevel() == 1)
            {
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 4.0f && Mathf.Abs(y) < 3.5f)
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                else
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
            }
            else
            {
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 3.7f && Mathf.Abs(y) < 2.5f)
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                else
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
            }

        }
    }
}
