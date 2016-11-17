using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TurnSwitcher : NetworkBehaviour {

	[SyncVar]
	public int playerTurn = 0;

	// Use this for initialization
	public override void OnStartServer () {
	
	}


}
