using UnityEngine;
using System.Collections;
[System.SerializableAttribute]
public class StringfyProperty {

	public object getProperty(string name)
	{
		return GetType().GetField(name).GetValue(this);
	}
	public void setProperty(string name,object value)
	{
		GetType().GetField(name).SetValue(this,value);
	}

	public T getValue<T>(string name)
	{
		return (T)getProperty(name);
	}

	public int getValue(string name)
	{
		return (int)getProperty(name);
	}
	public void setValue(string name,int value)
	{
		setProperty(name,value);
	}
	public int addValue(string name,int d)
	{
		int v = getValue(name)+d;
		setValue(name,v);
		return v;
	}
}
