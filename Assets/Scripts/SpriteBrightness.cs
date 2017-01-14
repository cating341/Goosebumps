using UnityEngine;
using System.Collections; 
public class SpriteBrightness : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player"))
        {
            float y = player.transform.position.y - gameObject.transform.position.y;
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 4.0f && Mathf.Abs(y) < 3.5f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log("white");
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                Debug.Log("black");
            }
        }
    }
}
