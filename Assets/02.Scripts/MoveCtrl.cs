using UnityEngine;
using System.Collections;

public class MoveCtrl : MonoBehaviour {

	// コンポーネントを割り当てる変数を設定
	private Transform tr;
	private CharacterController _controller;

	// キーボードからの入力値の変数を宣言
	private float h = 0.0f;
	private float v = 0.0f;

	// 移動速度と回転速度の変数
	public float movSpeed = 5.0f;
	public float rotSpeed = 50.0f;

	// 移動方向のベクトル変数
	private Vector3 movDir = Vector3.zero;

	// Use this for initialization
	void Start () {
		// Update関数からアクセスするコンポーネントを変数に代入
		tr = GetComponent<Transform> ();
		_controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// キーボードからの入力値を受け取る
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		// マウスの左右移動値で回転
		tr.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);

		// ベクトルの加算演算を利用してあらかじめ移動方向を計算する
		movDir = (tr.forward * v) + (tr.right * h);
		// 重力の影響を受けて下に落ちるようにy値を変更
		movDir.y -= 20f * Time.deltaTime;

		// プレイヤーを移動させる
		_controller.Move (movDir * movSpeed * Time.deltaTime);
	}
}
