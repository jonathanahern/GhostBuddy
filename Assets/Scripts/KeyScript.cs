using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {

	public bool playerOneCanCollect = false;
	public bool playerTwoCanCollect = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		
	
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.CompareTag ("Player One") && playerOneCanCollect == true) {

			other.gameObject.GetComponent<GhostMovementScript> ().CmdAddKey ();
			Destroy (gameObject);
			return;
		}

		else if (other.gameObject.CompareTag ("Player Two") && playerTwoCanCollect == true) {

			other.gameObject.GetComponent<GhostMovementScript> ().CmdAddKey ();
			Destroy (gameObject);
			return;
		}

		else if (other.gameObject.CompareTag ("Player One") && playerOneCanCollect == false) {

			other.gameObject.GetComponent<GhostMovementScript> ().CmdAddHit ();
			other.gameObject.GetComponent<GhostMovementScript> ().CmdAddHit ();
			return;
		}

		else if (other.gameObject.CompareTag ("Player Two") && playerTwoCanCollect == false) {

			other.gameObject.GetComponent<GhostMovementScript> ().CmdAddHit ();
			other.gameObject.GetComponent<GhostMovementScript> ().CmdAddHit ();
			return;
		}
		else if (other.gameObject.name == "Ghost(Clone)") {

			Destroy (gameObject);

			return;
		}
	}

}
