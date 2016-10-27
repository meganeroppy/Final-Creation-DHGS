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

	/// <summary>
	/// 一度でも変化したか？
	/// </summary>
	bool swapOnce = false;

	/// <summary>
	/// 繰り返し変化するか？
	/// </summary>
	public bool reversible = false;

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

	void Start()
	{
		// 有効になっている構造があったらそれのインデックスをカレントにする
		for(int i=0 ; i < parts.Length ;i++)
		{
			if( parts[i].activeInHierarchy )
			{
				currentStructure = i;
				break;
			}
		}

	}


	/// <summary>
	/// 構造を変化させる
	/// </summary>
	public void SwapStructure()
	{
		if( swapOnce && !reversible )
		{
			Debug.Log("二度目はない");
			return;
		}

		swapOnce = true;

		// 構造インデックスを更新
		currentStructure = currentStructure >= parts.Length-1 ? 0 : currentStructure+1;

		// 表示している構造を変更
		for( int i=0 ; i < parts.Length ; i++ ){
			parts[i].SetActive( i == currentStructure );
		}
	}

}


