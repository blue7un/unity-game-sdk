using UnityEngine;
using System.Collections;

public class GlobalGameVariables : Object {
	
	public string gameInfo;
	public string gameUserID;
	public string gameServerID;
	
	// Use this for initialization
	private static readonly GlobalGameVariables instance = new GlobalGameVariables();
	
	private GlobalGameVariables(){}
	
	public static GlobalGameVariables Instance
	{
		get 
		{
			return instance; 
		}
	}
}
