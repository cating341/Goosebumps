using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JPoolManager : MonoBehaviour {

	// Use this for initialization
	
	//private List<GameObject> pool;
	
	//private List<JPrefabPool> PrefabPools;
	private Dictionary<string,JPrefabPool> Pools = new Dictionary<string, JPrefabPool>();
	public static JPoolManager instance{get;private set;}
	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			throw new UnityException("double singleton");
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}
	public
	
	void Start () {
		//foreach(JPrefabPool pp in PrefabPools)
		//{
			
		//}
	}
	public void AddPrefabPool(string objName,JPrefabPool pool)
	{
		Pools.Add(objName,pool);
	}
	public GameObject GetObject(string name)
	{
		JPrefabPool jpp = null;
		if(Pools.TryGetValue(name,out jpp))
		{
			return jpp.GetObject();
		} 
		else
		{
			//throw new UnityException(name+" does not exist");
			return null;
		}
	}
	
	public void RecycleObject(GameObject go,float delay)
	{
		go.GetComponent<JPoolObject>().DelayRecycle(delay);
	}
	public void RecycleObject(GameObject go)
	{
		JPoolObject pobj =go.GetComponent<JPoolObject>();
		if(pobj)
		pobj.Recycle();
		else
		Destroy(go);
	}
}
