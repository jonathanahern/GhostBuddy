using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameCountScript : NetworkBehaviour {

	public int keyCount;
	public Text keyCountText;
	public int keysNeeded;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		keyCountText.text = "Keys " + keyCount.ToString () + "/" + keysNeeded.ToString();

	}
}
