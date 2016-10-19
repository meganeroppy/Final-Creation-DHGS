using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// 変化する構造
/// </summary>
public class VariableStructure : MonoBehaviour {

	/// <summary>
	/// 構造候補
	/// </summary>
	public GameObject[] parts;

	// 現在有効になっている構造インデックス
	int currentStructure = 0;

	/// <summary>
	/// プレイヤーが範囲内にいる時だけ判定を行う 存在しない時は常に判定を行う
	/// </summary>
	[SerializeField]
	TriggerArea triggerArea;

	public bool inArea
	{
		get{
			if( triggerArea == null )
			{
				return true;
			}
			return triggerArea.inArea;
		}
	}
	/// <summary>
	/// 構造を変化させる
	/// </summary>
	public void SwapStructure()
	{
		// 構造インデックスを更新
		currentStructure = currentStructure >= parts.Length-1 ? 0 : currentStructure+1;

		// 表示している構造を変更
		for( int i=0 ; i < parts.Length ; i++ ){
			parts[i].SetActive( i == currentStructure );
		}
	}

	void OnTriggerStay(Collider col)
	{
		if( col.tag.Equals("Player") )
		{
			Debug.LogWarning("範囲内");
		}
	}
}


