using UnityEngine;
using System.Collections;

/// <summary>
/// 範囲内にいることがトリガーとなる領域
/// </summary>
public class TriggerArea : MonoBehaviour 
{
	/// <summary>
	/// 範囲内か？
	/// </summary>
	bool _inArea = false;
	public bool inArea
	{
		get{
			return _inArea;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag.Equals("Player"))
		{
			if (!_inArea) _inArea = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag.Equals("Player"))
		{
			if (_inArea) _inArea = false;
		}
	}

}
