using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JPrefabPool:MonoBehaviour{

	// Use this for initialization
	private Stack<GameObject> pool;
	
	//must have Component of JPoolObject
	public int amount = 10;
	public GameObject Prefab;
	void Awake()
	{
		pool = new Stack<GameObject>();
		
	}
	void Start()
	{
		JPoolManager.instance.AddPrefabPool(Prefab.name,this);
		pool = new Stack<GameObject>();
		for(int i=0;i<amount;i++)
		{
			GameObject go = Instantiate(Prefab) as GameObject;
			go.SetActive(false);
			go.GetComponent<JPoolObject>().pool = this;
			pool.Push(go);
		}
	}
	public GameObject GetObject()
	{
		GameObject go = null;
		if(pool.Count>0)
		{
			go = pool.Pop();
			go.SetActive(true);
		}
		else
		{
			go = Instantiate(Prefab) as GameObject;
			go.GetComponent<JPoolObject>().pool = this;
		}
		return go;
	}
	public void Recycle(GameObject go)
	{
		go.SetActive(false);
		pool.Push(go);
	//	
	}
}
