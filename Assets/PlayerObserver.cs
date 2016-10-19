using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの行動に従ってアクションを実行する
/// </summary>
public class PlayerObserver : MonoBehaviour 
{

	/// <summary>
	/// 変化させる構造
	/// </summary>
	public VariableStructure[] structures;

	/// <summary>
	/// 監視対象
	/// </summary>
	GameObject observee;

	/// <summary>
	/// 現在の条件で既にアクションが実行されたか？
	/// </summary>
	bool executedActionInCurrentCondition = false; 

	/// <summary>
	/// 範囲内と解釈する角度(degree)
	/// </summary>
	float thresholdDegree = 30f;

	void Start()
	{
		observee = GameObject.Find("Player");
	}

	void Update()
	{
		if( CheckCondition() ){
			ExecuteAction();
		}
	}

	/// <summary>
	/// 条件を満たしているか調べる
	/// </summary>
	bool CheckCondition()
	{
		if( observee == null ){
			Debug.LogError("observeeが null");
			return false;
		}
			
		if( CheckIsTargetBackSide() ){
			if( !executedActionInCurrentCondition){
				executedActionInCurrentCondition = true;
				return true;
			}
		}else if(executedActionInCurrentCondition){
			executedActionInCurrentCondition = false;
		}

		return false;
	}

	/// <summary>
	/// アクションを実行
	/// </summary>
	void ExecuteAction()
	{
		foreach( VariableStructure s in structures ){
			s.SwapStructure();
		}
	}


	/// <summary>
	/// p2からp1への角度を求める
	/// </summary>
	/// <returns>2点の角度(Degree)</returns>
	/// <param name="p1">自分の座標</param>
	/// <param name="p2">相手の座標</param>
	public float GetAim(Vector3 p1, Vector3 p2) {
		float dx = p2.x - p1.x;
		float dz = p2.z - p1.z;
		float rad = Mathf.Atan2(dz, dx);

		float num = -(rad * Mathf.Rad2Deg) + 90f;
		if( num < 0 ){
			num += 360f;
		}
		return num;
	}


	/// <summary>
	/// 監視対象が管理している構造を背後に捉えているかどうか
	/// </summary>
	bool CheckIsTargetBackSide()
	{
		// プレイヤーが有効な位置いいなければ判定しない
		if( !structures[0].inArea )
		{
			Debug.Log("プレイヤーが有効範囲にいない");
			return false;
		}

		// 対象の構造に対してのどの方向を向いているか調べる
		Vector3 tPos = structures[0].transform.position;
		Vector3 pPos = observee.transform.position;

		// プレイヤーからみて建物を正面にした時の角度(degree)を求める
		float aim = GetAim(pPos, tPos);
		// Debug.Log("正面の角度 : " + aim.ToString() );

		float back = aim >= 180f ? aim - 180f : aim + 180f;
	//	Debug.Log("背後の角度 : " + back.ToString() );

		// プレイヤーの角度(degree)
		float pRot = observee.transform.rotation.eulerAngles.y;
	//	Debug.Log( "プレイヤーの回転 : " + pRot.ToString() );

		// 正面角度から何度の角度差があるか？
		float rotDiff = Mathf.Abs( back - pRot );
		if( rotDiff >= 180f ) {
			rotDiff = 360f - rotDiff;
		}
	//	Debug.Log( "背後角度から" + rotDiff.ToString() + "離れている" );

		return rotDiff <= thresholdDegree;

		// プレイヤーと建物の距離を求める
		//Vector3 diff = new Vector3(playerPos.x - targetPos.x, 0, playerPos.z - targetPos.z );
		//float distance = diff.magnitude;
		//Debug.Log( "距離 : " + distance.ToString() );
	}
}
