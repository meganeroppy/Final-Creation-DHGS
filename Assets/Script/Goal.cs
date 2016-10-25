using UnityEngine;
using System.Collections;

/// <summary>
/// 触れるとレベルクリア
/// </summary>
public class Goal : MonoBehaviour {

	/// <summary>
	/// プレイヤー
	/// </summary>
	public GameObject player;

	void OnTriggerEnter(Collider col)
	{
		if( col.tag.Equals("Player"))
		{
			Debug.LogError("ゴール");
		}
	}
}
