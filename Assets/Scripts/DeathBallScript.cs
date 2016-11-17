using UnityEngine;
using System.Collections;

public class DeathBallScript : MonoBehaviour {

	private int hitCount;
	private bool alreadyTriggered = false;

	// Use this for initialization
	void Start () {
	

	
	}

	void OnTriggerEnter(Collider other){

		Debug.Log (other.gameObject.tag);

		if (other.gameObject.CompareTag ("Player One") || other.gameObject.CompareTag ("Player Two")) {
			if (alreadyTriggered == false) {
				other.gameObject.GetComponent<GhostMovementScript> ().CmdAddHit ();
				alreadyTriggered = true;
			}
		}
	}
}
