using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 2;

	private string roomName;

	private NetworkManager networkManager;
	AudioSource source;

	void Awake()
	{
		Screen.SetResolution(480,720,false);
	}

	void Start (){
	
		source = GetComponent<AudioSource> ();
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) {
		
			networkManager.StartMatchMaker ();

		}
	
	}

	public void SetRoomName (string _name)
	{

		roomName = _name;

	}

	public void CreateRoom ()
	{

		if (roomName != "" && roomName != null) 
		{

			Debug.Log ("Creating Room: " + roomName + " with room for " + roomSize + " players.");
			networkManager.matchMaker.CreateMatch (roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);


		}

	}

}
