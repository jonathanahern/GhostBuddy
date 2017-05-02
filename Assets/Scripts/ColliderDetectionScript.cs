using UnityEngine;
using System.Collections;

public class ColliderDetectionScript : MonoBehaviour {

	private GameObject windmill;
	private WindmillScript windmillScript;
	private TurnManagerScript turnMan;

	GameObject ghostOne;
	GameObject ghostTwo;

	// Use this for initialization
	void Start () {

		windmill = gameObject.transform.parent.gameObject;
		windmillScript = windmill.GetComponent<WindmillScript> ();
		Invoke ("FindTurnMan", 3.0f);
	
	}

	void FindTurnMan(){
	
		turnMan = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ();

		while (turnMan == null) {
			turnMan = GameObject.FindGameObjectWithTag ("Game Manager Local").GetComponent<TurnManagerScript> ();
			Debug.Log ("FINDING NOTHING");
		}
	
	}


	void OnTriggerEnter(Collider other) {

		if (turnMan.preview == true) {
			return;
		}
		
		if (other.gameObject.tag == "Player") {

			Debug.Log ("Entered" + other.gameObject.name);


			if (windmillScript.ghostOne == other.gameObject || windmillScript.ghostTwo == other.gameObject) {
				return;
			}

			if (windmillScript.ghostOne == null) {
				windmillScript.ghostOne = other.gameObject;
				ghostOne = other.gameObject;
				ghostOne.GetComponent<GhostScript> ().inWindMill = true;
				ghostOne.GetComponent<GhostScript> ().myWindMill = windmill;
			} else {
				windmillScript.ghostTwo = other.gameObject;
				ghostTwo = other.gameObject;
				ghostTwo.GetComponent<GhostScript> ().inWindMill = true;
				ghostTwo.GetComponent<GhostScript> ().myWindMill = windmill;

			}
		}
	}

	void OnTriggerExit(Collider other) {
		//Debug.Log (other.gameObject.tag);

		if (turnMan.preview == true) {
			return;
		}

		if (other.gameObject.tag == "Player") {
			if (other.gameObject == windmillScript.ghostOne) {
				windmillScript.ghostOne = null;
				other.gameObject.transform.parent = null;
				other.gameObject.GetComponent<GhostScript> ().inWindMill = false;
				other.gameObject.GetComponent<GhostScript> ().myWindMill = null;
				ghostOne = null;
			} else {
				windmillScript.ghostTwo = null;
				other.gameObject.transform.parent = null;
				other.gameObject.GetComponent<GhostScript> ().inWindMill = false;
				other.gameObject.GetComponent<GhostScript> ().myWindMill = null;
				ghostTwo = null;
			}
		}
	}
}
