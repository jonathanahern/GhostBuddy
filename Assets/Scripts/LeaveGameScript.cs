using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class LeaveGameScript : MonoBehaviour {

	private NetworkManager networkManager;

	void Start (){

		networkManager = NetworkManager.singleton;

	}


	public void LeaveRoom ()
	{
		MatchInfo matchInfo = networkManager.matchInfo;
		networkManager.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
		networkManager.StopHost ();
	}

}
