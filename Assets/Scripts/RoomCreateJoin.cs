using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreateJoin : MonoBehaviour {

	public string levelName;
	public GameObject loadScreen;
	public GameObject levelMap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateRoomButton (){


		if (levelName == "") {
		
			return;

		}

		GameObject.FindGameObjectWithTag ("Room List").GetComponent<JoinGame> ().JoinOrCreateRoom (levelName);
		loadScreen.SetActive (true);
		levelMap.SetActive (false);
	}

}
