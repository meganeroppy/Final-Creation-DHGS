using UnityEngine;
using System.Collections;

/// <summary>
/// 触れるとレベルクリア
/// </summary>
public class Goal : MonoBehaviour 
{	
	void OnTriggerEnter(Collider col)
	{
		if( col.tag.Equals("Player"))
		{
			Debug.LogError("ゴール");
		}
	}
}
