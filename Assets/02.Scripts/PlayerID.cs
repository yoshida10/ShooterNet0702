using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// NetworkBehaviour...isLocalPlayerとかの変数を所持する
public class PlayerID : NetworkBehaviour {

	// SyncVar：「Command」で変更後、全クライアントへ変更結果を送信
	[SyncVar]
	private string PlayerUniqueIdentity;
	// 9・10のような記述をするとPlayerUnique~はSyncVar変数とか言うらしい

	private NetworkInstanceId	playerNetID;
	private Transform myTransform;

	public override void OnStartLocalPlayer()
	{
		GetNetIdentity ();
		SetIdentity ();
	}

	void Awake()
	{
		myTransform = GetComponent<Transform> ();
	}

	void Update()
	{
		if(myTransform.name==""||myTransform.name == "Player(Clone)")
		{
			SetIdentity();
		}
	}

	[Client]
	void GetNetIdentity()
	{
		// NetworkIdenittyNetID取得
		playerNetID = GetComponent<NetworkIdentity> ().netId;
		// 名前を付けるメソッド実行
		CmdTellServerMyIdentity (MakeUniqueIdentity ());
	}

	void SetIdentity()
	{
		// 自分以外のPlayerオブジェクトの場合
		if (!isLocalPlayer) {
			// 今付いている名前のまま
			myTransform.name=PlayerUniqueIdentity;
		}else{
			// 自分自身の場合、MakeUniqueIdentityメソッドで名前を取得
			myTransform.name=MakeUniqueIdentity();
		}
	}

	string MakeUniqueIdentity()
	{
		// Player + NetIDで名前を付ける
		string uniqueName = "Player" + playerNetID.ToString ();
		return uniqueName;
	}

	// Command:  SyncVar変数を変更し、変更結果を全クライアントへ送る
	[Command]
	void CmdTellServerMyIdentity(string name)
	{
		PlayerUniqueIdentity = name;
	}
}