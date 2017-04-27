using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	List<GameObject> roomList = new List <GameObject> ();

	[SerializeField]
	private Text status;

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Transform roomListParent;

	private NetworkManager networkManager;
	private string currentLevelName;

	// Use this for initialization
	void Start () {
	
		networkManager = NetworkManager.singleton;

		if (networkManager.matchMaker == null)
		{
		
			networkManager.StartMatchMaker ();
		
		}

		RefreshRoomList ();

	}

	public void RefreshRoomList ()
	{
		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading...";
	
	}

	public void OnMatchList (bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
	
	
		status.text = "";

		if (!success || matchList == null) {
		
			status.text = "Couldn't get room list.";
			return;
		
		}

		ClearRoomList ();

		foreach (MatchInfoSnapshot match in matchList) {
			
			GameObject _roomListItemGO = Instantiate (roomListItemPrefab);
			_roomListItemGO.transform.SetParent (roomListParent);
			_roomListItemGO.transform.localScale = Vector3.one;

			RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem> ();
			if (_roomListItem != null) {
			
			
				_roomListItem.Setup (match, JoinRoom);
			
			}


			roomList.Add (_roomListItemGO);
		}
	
		if (roomList.Count == 0) {
		
			status.text = "empty";
		
		}

	}

	void ClearRoomList(){
	
		for (int i = 0; i < roomList.Count; i++) {
		
			Destroy (roomList [i]);
		
		}

		roomList.Clear ();
	
	}

	public void JoinRoom (MatchInfoSnapshot _match){
	
		Debug.Log ("Joining " + _match.name);
		networkManager.matchMaker.JoinMatch (_match.networkId, "", "", "", 0, 0,  networkManager.OnMatchJoined);
		ClearRoomList ();
		status.text = "Joining...";
	
	}

	public void JoinOrCreateRoom (string levelName){

		ClearRoomList ();
		networkManager.matchMaker.ListMatches (0, 20, levelName, true, 0, 0, OnMatchList);
		currentLevelName = levelName;
		Invoke ("NextStepJoinOrCreate", 2.0f);


	}

	void NextStepJoinOrCreate () {
		
		int childCount = roomListParent.childCount;

		if (childCount < 1) {

			networkManager.matchMaker.CreateMatch (currentLevelName, 2, true, "", "", "", 0, 0, networkManager.OnMatchCreate);

		} else {

			roomListParent.GetChild (0).GetComponent<RoomListItem> ().JoinRoom ();

		}

	}
}
