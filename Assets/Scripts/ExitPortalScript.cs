using UnityEngine;
using System.Collections;

public class ExitPortalScript : MonoBehaviour {

	public int playersInside = 0;
	private GameObject player;
	private bool won = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (playersInside == 2 && won == false) {

			player = GameObject.Find ("Ghost(Clone)");
			player.GetComponent<GhostMovementScript> ().RpcWinScreen();
			won = true;
		}
	
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.name == "Ghost(Clone)") {

			playersInside++;

		}
			
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject.name == "Ghost(Clone)") {

			playersInside--;

		}

	}
		
}
